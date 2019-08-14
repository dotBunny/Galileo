using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.Submissions
{
    /// <summary>
    /// A collection of SubmissionCandidates and SubmissionFilters
    /// </summary>
    class SubmissionCandidates
    {
        #region Fields
        
        /// <summary>
        /// Candidates that have been through the resolved function, but
        /// are not necessarly submissions.
        /// </summary>
        private List<SubmissionCandidate> _processed;

        /// <summary>
        /// Candidates that have to go or going through the resolved function
        /// </summary>
        private List<SubmissionCandidate> _processing;

        /// <summary>
        /// Candidates that next to go in the resolved function
        /// </summary>
        private List<SubmissionCandidate> _nextProcessing;

        /// <summary>
        /// Filters to apply to candidates when they resolve themselves
        /// </summary>
        private List<SubmissionFilters.ISubmissionFilter> _filters;
        
        /// <summary>
        /// Resolve function mutex for thread safetyy
        /// </summary>
        private object _resolveLockObject;

        /// <summary>
        /// Next processing list mutext for thread safety
        /// </summary>
        private object _nextProcessingObject;

        /// <summary>
        /// Are the Candidates being processed?
        /// </summary>
        private bool _isProcessing;

        /// <summary>
        /// Iterator index for when iterating through resolved candidates
        /// </summary>
        private int _resolveIndex;

        #endregion
        
        public SubmissionCandidates()
        {
            _isProcessing = false;
            _resolveIndex = 0;
            _resolveLockObject = new object();
            _nextProcessingObject = new object();
            _processed = new List<SubmissionCandidate>(10);
            _processing = new List<SubmissionCandidate>(10);
            _nextProcessing = new List<SubmissionCandidate>(10);
            _filters = null;
        }
        
        /// <summary>
        /// Are the Candidates being processed?
        /// </summary>
        public bool IsProcessing
        {
            get { return _isProcessing; }
        }

        /// <summary>
        /// Deprecated/NOT USED????
        /// </summary>
        public List<SubmissionCandidate> FilteredCandidates
        {
            get { return _processed;  }
        }

        /// <summary>
        /// Reset the iterator and maybe get the first resolved candidate
        /// that is a submission, or null if there isn't any.
        /// </summary>
        public SubmissionCandidate FirstResolved
        {
            get
            {
                _resolveIndex = 0;
                return NextResolved;
            }
        }

        /// <summary>
        /// Maybe get the next resolved candidate that is a submission,
        /// or null if there isn't any.
        /// </summary>
        public SubmissionCandidate NextResolved
        {
            get
            {
                SubmissionCandidate candidate = null;
                for (int i = _resolveIndex; i < _processed.Count; i++)
                {
                    SubmissionCandidate c = _processed[i];
                    if (c.IsSubmission && !c.IsFiltered)
                    {
                        _resolveIndex = i + 1;
                        candidate = c;
                        break;
                    }
                }
                return candidate;
            }
        }

        /// <summary>
        /// Deprecated ?????
        /// </summary>
        public List<SubmissionCandidate> Submissions1
        {
            get { return _processed;  }
        }

        /// <summary>
        /// Add a SubmissionFilter to the list of filters
        /// </summary>
        /// <param name="filter"></param>
        public void AddFilter(SubmissionFilters.ISubmissionFilter filter)
        {
            if (_filters == null)
            {
                _filters = new List<SubmissionFilters.ISubmissionFilter>(1);
            }
            _filters.Add(filter);
        }

        /// <summary>
        /// Run all the filters through a Candidate and return if it was
        /// filtered or not.
        /// </summary>
        /// <param name="candidate">The candidate to filter against</param>
        /// <returns></returns>
        public bool ApplyFilter(SubmissionCandidate candidate)
        {
            if (_filters == null || _filters.Count == 0)
                return false;

            foreach (var filter in _filters)
            {
                if (filter.Filter(candidate))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Add a candidate for resolving.
        /// If the candidates are currently being resolved, they will
        /// also be resolved in the the 'next batch' of _nextProcessing
        /// </summary>
        /// <param name="path">Path to process</param>
        /// <param name="depth">Current depth</param>
        /// <param name="parent">Parent candidate if any </param>
        public SubmissionCandidate Add(string path, int depth = 0, SubmissionCandidate parent = null)
        {
            if (depth > 20)
            {
                // @TODO
                // Make this more user friendly.
                // YUp. ...
                throw new Exception("Hit maximum folder depth");
            }

            SubmissionCandidate candidate = new SubmissionCandidate(path, depth, parent);

            if (_isProcessing)
            {
                lock (_nextProcessingObject)
                {
                    _nextProcessing.Add(candidate);
                }
            }
            else
            {
                _processing.Add(candidate);
            }

            return candidate;
        }

        /// <summary>
        /// Run through all candidates to be processed and resolve them.
        /// If any candidates are added then they will also be processed as well.
        /// </summary>
        public void Resolve(string workingDirectory)
        {
            // Mutex lock this function.
            lock (_resolveLockObject)
            {
                // We've started processing
                _isProcessing = true;

                // Clear _nextProcessing
                _nextProcessing.Clear();

                // We loop as many times as _nextProcessing is empty.
                while (true)
                {
                    // Go through all candidates for processing and resolve them
                    foreach (var candidate in _processing)
                    {
                        candidate.Resolve(this, workingDirectory);
                    }

                    // Add them to the _processed and then clear _processing
                    _processed.AddRange(_processing);
                    _processing.Clear();

                    // Lock the _nextProcessing mutex
                    lock (_nextProcessingObject)
                    {
                        // Nothing else to process? Then we leave
                        if (_nextProcessing.Count == 0)
                        {
                            break;
                        }

                        // More things to process? 
                        // Then add them to _processing for a another round
                        // and then clear _nextProcessing.
                        _processing.AddRange(_nextProcessing);
                        _nextProcessing.Clear();
                    }
                }

                // And done.
                _isProcessing = false;
            }
        }
    }
}
