using Galileo.Core.Submissions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Galileo.Core.Logging;
using Galileo.Core.Analytics;

namespace Galileo.Core
{
    /// <summary>
    /// Options for a Hunter Session to turn on/off particular functions when
    /// detecting submissions
    /// </summary>
    [Flags]
    internal enum HunterSessionOptions
    {
        /// <summary>
        /// Provide analytics through the IAnalytics interface
        /// </summary>
        Analytics,
        /// <summary>
        /// Provide logging through the ILog interface
        /// </summary>
        Logging,
        /// <summary>
        /// Update the session based on percentage and description 
        /// of what is the current state
        /// </summary>
        Progress,
        /// <summary>
        /// Perform the submission detection in seperate threads
        /// </summary>
        Threaded,
        /// <summary>
        /// Update the Client UI via OnProgress and OnComplete event updates
        /// Usually used with Progress.
        /// </summary>
        Events,

        /// <summary>
        /// Catch all for a typical setup for the Client
        /// </summary>
        Any = Threaded | Analytics | Logging | Progress | Events,

        /// <summary>
        /// Export read content that is used by the scanner to the working directory
        /// </summary>
        Export,

        /// <summary>
        /// Record times during the submission detection
        /// </summary>
        Timed,

        /// <summary>
        /// Catch all of a typical setup for the Unit Tests
        /// </summary>
        UnitTests = 0
    }

    /// <summary>
    /// Hunter Session is a pass-around-object to represent the state
    /// of a collection of submissions for processing.
    /// </summary>
    internal class HunterSession
    {

        #region Fields

        /// <summary>
        /// The profile related to this session
        /// </summary>
        HunterProfile _profile;

        /// <summary>
        /// The hunter configuration to this session
        /// </summary>
        HunterConfig _config;

        /// <summary>
        /// File path(s) that have been resolved in turned into a list of possible
        /// submission files. Paths are a full path.
        /// </summary>
        List<string> _resolvedFiles;

        /// <summary>
        /// Instance to the log used to debug and relay information to the user
        /// or any listener to the log
        /// </summary>
        ILog _log;

        /// <summary>
        /// Options related to running the HunterSession in relation to library;
        /// i.e. if it is running as a Client, CLI or as a Unit Test.
        /// </summary>
        HunterSessionOptions _options;

        /// <summary>
        /// Worker thread to process submissions on 
        /// </summary>
        Thread _processThread;

        /// <summary>
        /// Cancellation token to cancel the submission thread if started.
        /// </summary>
        CancellationTokenSource _cancellationToken;

        /// <summary>
        /// Overall progress
        /// </summary>
        float _progressPercentage;

        /// <summary>
        /// Number of true submissions to process
        /// </summary>
        int _workUnitsCount;

        /// <summary>
        /// Number of submissions that have been processed
        /// </summary>
        int _workUnitsComplete;

        /// <summary>
        /// Date/Time when the session has started.
        /// </summary>
        DateTime _ran;

        /// <summary>
        /// Is the session currently running?
        /// </summary>
        AtomicBool _isRunning;

        /// <summary>
        /// Is the started session now aborting?
        /// </summary>
        AtomicBool _isAborting;

        /// <summary>
        /// Submissions to process, in process or processed
        /// </summary>
        ConcurrentBag<Submissions.Submission> _submissions;

        /// <summary>
        /// Assign the base submission (assignment) so we can rule out some things
        /// </summary>
        Submissions.Submission _baseSubmission;

        /// <summary>
        /// Google or Null analytics
        /// </summary>
        IAnalytics _analytics;

        /// <summary>
        /// Files, Folders or archives to be inspected for possible submissions
        /// </summary>
        SubmissionCandidates _candidates;

        /// <summary>
        /// Timer used for submission timing.
        /// </summary>
        DateTime _timer;

        /// <summary>
        /// Content Exporter
        /// </summary>
        ContextExporter.IContentExporter _exporter;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.HunterSession"/> class.
        /// </summary>
        /// <param name="profile">The <see cref="T:Galileo.Core.HunterProfile"/> to use for the session.</param>
        /// <param name="HandlerName"><see cref="T:Galileo.Client.HuntHandler"/>'s unique name.</param>
        /// <param name="workingDirectory">The working directory to search for submissions/candidates.</param>
        /// <param name="config">The <see cref="T:Galileo.Core.HunterConfig"/> to use when searching.</param>
        /// <param name="options">A set of options outlining features to use.</param>
        public HunterSession(HunterProfile profile, string HandlerName, string workingDirectory, HunterConfig config, HunterSessionOptions options = HunterSessionOptions.Any)
        {
            Name = HandlerName;

            //// options = options | HunterSessionOptions.Export;

            // Assign references
            _profile = profile;
            _options = options;
            _config = config;

            // Create the Log.
            if (HasOption(HunterSessionOptions.Logging))
            {
                _log = new Log();
            }
            else
            {
                _log = new NullLog();
            }

            // Create the Analytics
            if (HasOption(HunterSessionOptions.Analytics))
            {
                _analytics = new GoogleAnalytics(profile.PackageVersion, true);
            }
            else
            {
                _analytics = new NullAnalytics();
            }
            
            // Setup the class variables to their default states
            _resolvedFiles = new List<string>(1);
            _isAborting = new AtomicBool(false);
            _isRunning = new AtomicBool(false);
            _progressPercentage = 0.0f;
            _workUnitsCount = 0;
            _workUnitsComplete = 0;
            _processThread = null;
            _candidates = new SubmissionCandidates();
            _submissions = new ConcurrentBag<Submission>();

            // Setup working directory
            SetWorkingDirectory(workingDirectory);

            
            if (HasOption(HunterSessionOptions.Export))
            {
                _exporter = new ContextExporter.TextFileContentWriter(System.IO.Path.Combine(workingDirectory, HunterConfig.GalileoDefaultDataFolder));
            }
            else
            {
                _exporter = new ContextExporter.NullContentExporter();
            }
        }

        #region Delegates & Events

        /// <summary>
        /// For when the process has been updated
        /// </summary>
        /// <param name="percentageComplete"></param>
        public delegate void ProcessEventHandler(float percentageComplete);

        /// <summary>
        /// For when the process has been updated in terms of percentage
        /// or a description.
        /// Only called when the Events Option has been enabled
        /// </summary>
        public ProcessEventHandler OnProcess;

        /// <summary>
        /// For when the all submissions have been checked
        /// Only called when the Events Option has been enabled
        /// </summary>
        public Action OnComplete;

        #endregion

        #region Properties

        /// <summary>
        /// The Analytics to be used with this session
        /// Only used when the Analytics Option has been enabled
        /// </summary>
        public IAnalytics Analytics
        {
            get { return _analytics; }
            set { _analytics = value; }
        }

        /// <summary>
        /// The content exporter
        /// Only used when the Export Option has been enabled
        /// </summary>
        public ContextExporter.IContentExporter Exporter
        {
            get { return _exporter; }
        }

        /// <summary>
        /// The Base submission to refer too
        /// </summary>
        public Submissions.Submission BaseSubmission
        {
            get { return _baseSubmission; }
            set { _baseSubmission = value; }
        }

        /// <summary>
        /// Cancellation token when processing
        /// Only used when the Threading Option is enabled
        /// </summary>
        public CancellationTokenSource CancellationToken
        {
            get { return _cancellationToken; }
            set { _cancellationToken = value; }
        }

        /// <summary>
        /// The Configuration used with this session
        /// </summary>
        public HunterConfig Config
        {
            get { return _config; }
        }

        /// <summary>
        /// Does the session have an option enabled
        /// </summary>
        /// <param name="option">The option to check for</param>
        /// <returns></returns>
        public bool HasOption(HunterSessionOptions option)
        {
            return (_options & option) != 0;
        }

        /// <summary>
        /// Is the session currently aborting?
        /// </summary>
        public bool IsAborting
        {
            get { return _isAborting.Value; }
        }

        /// <summary>
        /// Is the session currently processing submissions?
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning.Value; }
        }

        /// <summary>
        /// Get the instance to the log (_log)
        /// </summary>
        public ILog Log
        {
            get { return _log; }
        }

        /// <summary>
        /// Session unique identifier used at runtime.
        /// </summary>
        /// <value>The <see cref="HunterSession" />'s name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Get the profile associated with this session
        /// </summary>
        public HunterProfile Profile
        {
            get { return _profile; }
        }

        /// <summary>
        /// The Date and Time the submission checks started running
        /// </summary>
        public DateTime Ran
        {
            get { return _ran; }
        }

        /// <summary>
        /// The number of submissions to be checked
        /// </summary>
        public int SubmissionCount
        {
            get { return _submissions.Count; }
        }

        /// <summary>
        /// An IEnumerable to the submissions
        /// </summary>
        public IEnumerable<Submissions.Submission> Submissions
        {
            get { return _submissions; }
        }

        /// <summary>
        /// The submissions themselves
        /// </summary>
        public ConcurrentBag<Submissions.Submission> SubmissionsBag
        {
            get { return _submissions; }
        }

        /// <summary>
        /// The thread used by the Hunter
        /// Only used when the Threading Option is enabled
        /// </summary>
        public Thread Thread
        {
            get { return _processThread; }
        }

        /// <summary>
        /// The full path to the working directory where all the output,
        /// temporary files and results are written to
        /// </summary>
        public string WorkingDirectory { get; private set; }
            

        /// <summary>
        /// Number of work units to process
        /// </summary>
        public int WorkUnits
        {
            get { return _workUnitsCount; }
            set { _workUnitsCount = value; }
        }

        /// <summary>
        /// Number of work units that have been processed
        /// </summary>
        public int WorkUnitsCompleted
        {
            get { return _workUnitsComplete; }
            set { _workUnitsComplete = value; }
        }

        /// <summary>
        /// The work units that have been processed in a decimal percentage (0-1)
        /// </summary>
        public float WorkUnitsPercent
        {
            get { return (float)_workUnitsComplete / (float)_workUnitsCount; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add an absolute path to search for files, archives or directories
        /// for possible candidate submissions to be processed
        /// </summary>
        /// <param name="path"></param>
        public void AddPathForSubmission(string path)
        {
            _candidates.Add(path);
        }

        /// <summary>
        /// Add a filter to check against and filter out any submission candidates
        /// that be either files, archives or directories.
        /// </summary>
        /// <param name="filter"></param>
        public void AddFilterForSubmission(Submissions.SubmissionFilters.ISubmissionFilter filter)
        {
            _candidates.AddFilter(filter);
        }

        public void AddDefaultSubmissionFilter()
        {
            _candidates.AddFilter(new Submissions.SubmissionFilters.DefaultSubmissionFilter());
        }

        public void AddFileExtensionSubmissionFilter(params string[] options)
        {
            _candidates.AddFilter(new Submissions.SubmissionFilters.FileExtensionSubmissionFilter(options));
        }

        public void AddFileNameSubmissionFilter(params string[] options)
        {
            _candidates.AddFilter(new Submissions.SubmissionFilters.FileNameSubmissionFilter(options));
        }
        
        public void AddDirectoryNameSubmissionFilter(params string[] options)
        {
            _candidates.AddFilter(new Submissions.SubmissionFilters.DirectoryNameSubmissionFilter(options));
        }

        /// <summary>
        /// Increment a work unit as having been completed.
        /// </summary>
        public void CompleteWorkUnit()
        {
            _workUnitsComplete++;

            SetProgress((1f - Hunter.PercentageHoldback) * (float)((float)_workUnitsComplete / (float)_workUnitsCount));
        }

        /// <summary>
        /// Sets the progress percentage of the session, and notifies any subscribers (if enabled).
        /// </summary>
        /// <param name="amount">he 0-1 percentage value of completion of the sessions workload.</param>
        public void SetProgress(float amount)
        {
            if (HasOption(HunterSessionOptions.Progress))
            {
                _progressPercentage = amount;
                InvokeProcess();
            }
        }

        /// <summary>
        /// Abort the current session's workload.
        /// </summary>
        /// <returns>Was the session able to abort?</returns>
        internal bool Abort()
        {
            if (_isAborting.Set(true) == true)
            {
                _isRunning.Set(false);

                // Kill Thread
                if (HasOption(HunterSessionOptions.Threaded) && _processThread != null)
                {
                    _processThread.Abort();
                    _processThread = null;
                }

                _cancellationToken?.Cancel();

                return true;

            }
            return false;
        }

        /// <summary>
        /// Resolves the files, folders and directories based on the working directory, 
        /// populating the <see cref="HunterSession.SubmissionsBag"/> with the filtered submissions.
        /// </summary>
        /// <remarks>An enumerator is available from <see cref="HunterSession.Submissions"/>.</remarks>
        internal void CreateSubmissions()
        {

            //// Do we have a working directory?
            //bool hasWorkingDirectory = !string.IsNullOrWhiteSpace(WorkingDirectory);

            // Resolve any candidates into possible submissions, applying the
            // current filters
            _candidates.Resolve(WorkingDirectory);

            // Maybe get the first candidate
            SubmissionCandidate candidate = _candidates.FirstResolved;

            // Process each valid candidate
            while (candidate != null)
            {
                // Try to create a submission from that candidate
                if (candidate.TryCreateSubmission(this, out Submission submission))
                {
                    // Add it to the submissions bag
                    _submissions.Add(submission);

                    //// If there isn't a working directory/base path, then we assume the first submission to be it.
                    //if (hasWorkingDirectory == false)
                    //{
                    //    hasWorkingDirectory = true;
                    //    WorkingDirectory = candidate.ContainerPath;
                    //}
                }

                // Fetch the next submission for processing, or null if there aren't any left
                candidate = _candidates.NextResolved;
            }
        }

        /// <summary>
        /// Tell the session we're no longer going to be doing anything with it.
        /// </summary>
        internal void EndProcess()
        {
            // Update progress as being completed.
            SetProgress(1);

            // And stop.
            if (_isRunning.Set(false))
            {
                _isAborting.Set(false);
                _processThread = null;
            }
        }

        /// <summary>
        /// Invokes the <see cref="OnProcess"/> event if the option is enabled.
        /// </summary>
        internal void InvokeProcess()
        {
            if (HasOption(HunterSessionOptions.Events) && OnProcess != null)
            {
                OnProcess.Invoke(_progressPercentage);
            }
        }

        /// <summary>
        /// Invokes the <see cref="OnComplete"/> event if the option is enabled.
        /// </summary>
        internal void InvokeComplete()
        {
            if (HasOption(HunterSessionOptions.Events) && OnComplete != null)
            {
                OnComplete.Invoke();
            }
        }

        /// <summary>
        /// Replace the _config with a new one
        /// </summary>
        /// <param name="newConfig">The newer config to replace with</param>
        internal void ReplaceConfig(HunterConfig newConfig)
        {
            _config = newConfig;
        }

        /// <summary>
        /// Reset the <see cref="HunterSession" />'s status state.
        /// </summary>
        internal void Reset()
        {
            _progressPercentage = 0;
            _workUnitsComplete = 0;
            _workUnitsCount = 0;

            _ran = DateTime.Now;

            _isAborting.Set(false);
            _isRunning.Set(false);
        }

        /// <summary>
        /// Sets the working directory, loading data if needed
        /// </summary>
        /// <param name="workingDirectory">Working directory.</param>
        /// <returns><see langword="true"/> if a config was found and loaded.</returns>
        internal bool SetWorkingDirectory(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;

            // Establish folder where the sessions log will be kept
            Log.Setup(HunterConfig.GetLogPath(WorkingDirectory, DateTime.Now.ToString(Galileo.Localization.LocalizationCache.DateLongSafeFormat) + "_" + Name));

            // Check if we have a config to load, loading if we do
            HunterConfig localConfig = HunterConfig.GetConfig(HunterConfig.GetConfigPath(workingDirectory));
            if (localConfig != null)
            {
                ReplaceConfig(localConfig);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tell the session its going to start processing
        /// </summary>
        /// <param name="thread">The target thread.</param>
        internal void Start(Thread thread)
        {
            if (_isRunning.Set(true))
            {
                _isAborting.Set(false);
                _processThread = thread;
            }
        }

        #endregion
    }
}