using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Galileo.Core;
using I18NPortable;

namespace Galileo.Client
{
    /// <summary>
    /// A client wrapper for <see cref="Hunter"/>'s
    /// </summary>
    public class HuntHandler
    {
        #region Fields

        /// <summary>
        /// A cached copy of the HunterProfile.
        /// </summary>
        HunterProfile _cachedProfile;

        /// <summary>
        /// A reference to the created <see cref="Hunter"/> to handle.
        /// </summary>
        HunterSession _session;

        #endregion

        public static HuntHandler Create(string id, HunterProfile profile)
        {
            return new HuntHandler(id, profile, Settings.DefaultFolder);
        }

        public HuntHandler(string id, HunterProfile profile, string targetPath)
        {
            // Assign parameters
            _cachedProfile = profile;
            ID = id;

            // Default properties
            Log = new ConcurrentBag<string>();

            HunterSessionOptions sessionOptions = 0;

            // Establish default set of options
            sessionOptions |= HunterSessionOptions.Events;
            sessionOptions |= HunterSessionOptions.Logging;
            sessionOptions |= HunterSessionOptions.Progress;
            sessionOptions |= HunterSessionOptions.Threaded;
            sessionOptions |= HunterSessionOptions.Analytics;


            // Create new session
            _session = new HunterSession(profile, ID, targetPath, Settings.DefaultConfig, sessionOptions);

            // Subscribe to events
            _session.OnComplete += OnComplete;
            _session.OnProcess += OnProcess;
            _session.Log.AddLogEventHandler(OnLogEvent);
        }

        ~HuntHandler()
        {
            _session.Log.FlushCache();
        }

        #region Events

        /// <summary>
        /// Action fired when the Hunter completes its process.
        /// </summary>
        public Action<string> OnProcessComplete;

        /// <summary>
        /// Action fired when the Hunter raises a log event.
        /// </summary>
        public Action<string, string> OnProcessLogEvent;

        /// <summary>
        /// Action fired when the Hunter updates the status of the process.
        /// </summary>
        public Action<string> OnProcessUpdate;

        /// <summary>
        /// Action triggered when the submission list changes based on the the working directory changing, or configuration changes.
        /// </summary>
		public Action OnSubmissionListChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets (or sets privately) the unique identifier of the <see cref="HuntHandler" />.
        /// </summary>
        /// <value>The identifying string.</value>
        public string ID { get; private set; }

        /// <summary>
        /// Gets (or sets privately) a value indicating whether a Hunter is processing currently.
        /// </summary>
        /// <value><c>true</c> if is working; otherwise, <c>false</c>.</value>
        public bool IsWorking
        {
            get; private set;
        }

        /// <summary>
        /// Gets (or sets privately) the total progress percentage.
        /// </summary>
        /// <value>The total progress percentage.</value>
        public float ProgressPercentage
        {
            get; private set;
        }

        /// <summary>
        /// A local stored copy of the log generated from the <see cref="Hunter"/>
        /// </summary>
        /// <value>The log.</value>
        public ConcurrentBag<string> Log { get; private set; }


        /// <summary>
        /// The target path to be used when searching for submissions.
        /// </summary>
        public string WorkingDirectory
        {
            get { return _session.WorkingDirectory; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cancel the <see cref="Hunter"/>'s current processing.
        /// </summary>
        public void Cancel()
        {
            Hunter.Cancel(_session);
            IsWorking = false;
        }

        /// <summary>
        /// Generate a unique 6 character identifier.
        /// </summary>
        /// <returns>The identifier.</returns>
        public static string CreateID()
        {
            // Create 6 character identification string for hunter session (logic is here to remove call to core)
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().Substring(0, 6);
        }

        /// <summary>
        /// Gets the sessions current configuration
        /// </summary>
        /// <returns>The <see cref="HunterConfig"/> from session.</returns>
        public HunterConfig GetConfigFromSession()
        {
            return _session.Config;
        }

        /// <summary>
        /// Get the path to the report for the current target folder.
        /// </summary>
        /// <returns>The report's path.</returns>
        public string GetReportPath()
        {
            return HunterConfig.GetReportPath(WorkingDirectory);
        }

        /// <summary>
        /// Does the current target folder have a report in it?
        /// </summary>
        /// <returns><c>true</c>, if it exists, <c>false</c> otherwise.</returns>
        public bool HasReport()
        {
            if (File.Exists(HunterConfig.GetReportPath(WorkingDirectory)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Open the current target folders report, if it exists.
        /// </summary>
        public void OpenReport()
        {
            if (HasReport())
            {
#if __MACOS__
                Platform.Run("open", HunterConfig.GetReportPath(WorkingDirectory), true);
#else
                Platform.Run(HunterConfig.GetReportPath(WorkingDirectory), string.Empty, true);
#endif
            }
        }

        /// <summary>
        /// Pre proces setup work on the target folder
        /// </summary>
        public void PreProcess()
        {
            // Make sure we have our data folder
            if (!Directory.Exists(HunterConfig.GetDataPath(WorkingDirectory)))
            {
                Directory.CreateDirectory(HunterConfig.GetDataPath(WorkingDirectory));
            }

            // We're going to create a list of the files ahead of time for now
            _session.AddPathForSubmission(WorkingDirectory);

            // Add base level submission filters
            _session.AddFilterForSubmission(new Core.Submissions.SubmissionFilters.DefaultSubmissionFilter());

            // User space exclusions
            _session.AddFilterForSubmission(new Core.Submissions.SubmissionFilters.FileExtensionSubmissionFilter(_session.Config.SharedIgnoredFileExtensions));
            _session.AddFilterForSubmission(new Core.Submissions.SubmissionFilters.FileNameSubmissionFilter(_session.Config.SharedIgnoredFiles));
            _session.AddFilterForSubmission(new Core.Submissions.SubmissionFilters.DirectoryNameSubmissionFilter(_session.Config.SharedIgnoredFolders));

            // Create our list of submissions (we'll change this later to have selection)
            _session.CreateSubmissions();
        }



        public void UpdateSessionConfig(HunterConfig config)
        {
            _session.ReplaceConfig(config);

            // This will most likely need to be changed in the future.
            //OnSubmissionListChanged?.Invoke();
        }
        public bool UpdateWorkingDirectory(string newWorkingDirectory)
        {
            return _session.SetWorkingDirectory(newWorkingDirectory);


            //OnSubmissionListChanged?.Invoke();
        }

        /// <summary>
        /// Instruct the <see cref="Hunter"/> to start processing.
        /// </summary>
        public void Process()
        {
            Hunter.Process(_session);

            IsWorking = true;
        }

        /// <summary>
        /// Event fired when the <see cref="Hunter"/> is has completed its processing.
        /// </summary>
        void OnComplete()
        {
            IsWorking = false;

            OnProcessComplete?.Invoke(ID);
        }


        /// <summary>
        /// Event fired when the <see cref="Hunter"/> raises a log event.
        /// </summary>
        /// <param name="line">The log line to be handled.</param>
        void OnLogEvent(string line)
        {
            string stripLine = Markdown.Strip(line);
            Log.Add(stripLine);
            OnProcessLogEvent?.Invoke(ID, stripLine);
        }

        /// <summary>
        /// Event fired when the <see cref="Hunter"/> updates its progress information.
        /// </summary>
        /// <param name="taskPercentComplete">Percentage of current task completed.</param>
        /// <param name="overallPercentComplete">Percentage of current estimate of overall work completed.</param>
        /// <param name="progressText">Current internal status text.</param>
        void OnProcess(float overallPercentComplete)
        {
            ProgressPercentage = overallPercentComplete;
            OnProcessUpdate?.Invoke(ID);
        }

        #endregion
    }
}