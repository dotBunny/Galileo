using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Galileo.Core.Checks;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.Submissions
{

    /// <summary>
    /// A single submission representing a single document style file for processing
    /// </summary>
    class Submission
    {

        #region Fields
    
        /// <summary>
        /// Hunter session
        /// </summary>
        private HunterSession _session;

        /// <summary>
        /// Candidate of the submission
        /// </summary>
        private SubmissionCandidate _candidate;

        /// <summary>
        /// File processor for this submission
        /// </summary>
        private IFileProcessor _processor;
        
        /// <summary>
        /// First name of the student
        /// </summary>
        internal string FirstName;
        
        /// <summary>
        /// Last name of the student
        /// </summary>
        internal string LastName;
        
        /// <summary>
        /// University/College/School ID of the student
        /// </summary>
        internal string StudentID;

        /// <summary>
        /// When the file was created
        /// </summary>
        internal DateTime MetaDateCreated;
        
        /// <summary>
        /// When the file was modified
        /// </summary>
        internal DateTime MetaDateModified;
        
        /// <summary>
        /// When the file was last printed
        /// </summary>
        internal DateTime MetaDateLastPrinted;
        
        /// <summary>
        /// What operating system username created the file
        /// </summary>
        internal string MetaUsername;
        
        /// <summary>
        /// What was the name of the person who registered the software that
        /// this file was created in
        /// </summary>
        internal string MetaCreator;
        
        /// <summary>
        /// When the file was last modified by
        /// </summary>
        internal string MetaLastModifiedBy;
        
        /// <summary>
        /// What the content is
        /// </summary>
        internal string Content;
        
        /// <summary>
        /// Length of the content
        /// </summary>
        internal int ContentLength;
        
        /// <summary>
        /// A hash of the content
        /// </summary>
        internal string ContentHash;
        
        /// <summary>
        /// Checks to perform on the content
        /// </summary>
        internal ConcurrentBag<ICheck> _checks;

        /// <summary>
        /// Hash of the GUID
        /// </summary>
        internal string _guidHash;

        /// <summary>
        /// Path of the file relative to the working directory
        /// </summary>
        internal string _relativePath;

        #endregion

        /// <summary>
        /// Submission constructor
        /// </summary>
        /// <param name="session">Current session</param>
        /// <param name="candidate">Candidate to work with</param>
        internal Submission(HunterSession session, SubmissionCandidate candidate)
        {
            // Assign references
            _session = session;
            _candidate = candidate;
            
            // Assign Defaults
            FirstName = string.Empty;
            LastName = string.Empty;
            StudentID = string.Empty;
            ContentLength = 0;
            ContentHash = string.Empty;
            Content = string.Empty;
            MetaLastModifiedBy = string.Empty;
            MetaCreator = string.Empty;
            MetaUsername = string.Empty;
            MetaDateLastPrinted = DateTime.MinValue;
            MetaDateModified = DateTime.MinValue;
            MetaDateCreated = DateTime.MinValue;

            // Create our relative path based on the working directory (just remove it and add a directory character)
            _relativePath = AbsolutePath.Replace(session.WorkingDirectory, string.Empty);

            DetectName();
            
            // Figure out the processor
            _processor = candidate.FileType.CreateProcessor(this);

            // Do a quick check of the meta data filling out some of our details
            if (Processor != null)
            {
                Processor.Process();
            }

            // Check Processor
            if (!Processor.IsProcessed())
            {
                _session.Log.Add("- " + Markdown.Emphasis("Unable to process this submission. Please confirm that it is a supported file type."));
            }

            // Create Checks
            _checks = CheckFactory.CreateChecks(this, Processor.GetCheckTypes());

            // Create simple ID
            _guidHash = Core.Compare.Hash(_candidate.Guid.ToString()).Substring(0, 6);
        }

        #region Properties

        /// <summary>
        /// Configuration associated with 
        /// </summary>
        internal HunterConfig Config
        {
            get { return _session.Config; }
        }

        /// <summary>
        /// Shared Submission Log
        /// </summary>
        internal Galileo.Core.Logging.ILog Logging
        {
            get { return _session.Log; }
        }
        
        /// <summary>
        /// Is the Submission flagged by a check?
        /// </summary>
        internal bool Flagged
        {
            get
            {
                foreach (ICheck check in _checks)
                {
                    if (check.Flagged())
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// The base submission to refer to
        /// </summary>
        public Submission BaseSubmission => _session.BaseSubmission;

        /// <summary>
        /// The FileProcessorType
        /// </summary>
        internal FileProcessorFactory.FileProcessorType Type => _candidate.FileType.GetProcessorType();

        /// <summary>
        /// The File processor checking the submission
        /// </summary>
        internal IFileProcessor Processor => _processor;

        /// <summary>
        /// The GUID
        /// </summary>
        internal Guid GUID => _candidate.Guid;

        /// <summary>
        /// A hash of the GUID
        /// </summary>
        internal string GUIDHash => _guidHash;

        /// <summary>
        /// The time that the file was written to disk
        /// </summary>
        internal DateTime FileDate => _candidate.FileWriteTime;

        /// <summary>
        /// The name of the file without an extension
        /// </summary>
        internal string FileName => _candidate.FileNameWithoutExtension;

        /// <summary>
        /// The name of the file with an extension
        /// </summary>
        internal string FileNameWithExtension => _candidate.FileName;

        /// <summary>
        /// The directory path of the file
        /// </summary>
        internal string ContainerPath => _candidate.ContainerPath;

        /// <summary>
        /// Full Path to file
        /// </summary>
        internal string AbsolutePath => _candidate.ReadPath;

        /// <summary>
        /// Path relative to the working directory
        /// </summary>
        internal string RelativePath => _relativePath;

        /// <summary>
        /// use to flag for similar sized files
        /// </summary>
        internal long FileSize => _candidate.FileSize;

        /// <summary>
        /// Has the submission been processed yet?
        /// </summary>
        internal bool Processed => Processor.IsProcessed();
        
        /// <summary>
        /// The checks working on the submission
        /// </summary>
        internal ConcurrentBag<ICheck> Checks => _checks;
        
        #endregion

        #region Static Methods

        /// <summary>
        /// Is the string all in UPPERCASE?
        /// </summary>
        private static bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Search predicate returns true if a string ends in "saurus".
        /// </summary>
        private static bool IsMonth(String s)
        {
            switch (s.ToLower())
            {
                case "jan":
                case "feb":
                case "mar":
                case "apr":
                case "may":
                case "jun":
                case "jul":
                case "aug":
                case "sep":
                case "sept":
                case "oct":
                case "nov":
                case "dec":
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Is a string a day of the week?
        /// </summary>
        private static bool IsDay(string s)
        {
            switch (s.ToLower())
            {
                case "mon":
                case "tues":
                case "weds":
                case "thurs":
                case "fri":
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Is the string either the two time periods; AM or PM?
        /// </summary>
        private static bool IsTime(string s)
        {
            switch (s.ToLower())
            {
                case "am":
                case "pm":
                    return true;
            }
            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the check or NULL for the given CheckType
        /// </summary>
        internal ICheck GetCheck(CheckFactory.CheckType type)
        {
            foreach (ICheck c in Checks)
            {
                if (c.GetType() == type)
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// Detect the author's name from the file name
        /// </summary>
        void DetectName()
        {
            // Attempt to figure out what the name and student ID of the submission might be

            string workingName = RelativePath;

            // TODO: Remove any sort of assignment thats given in the config
            // TODO: Custom regex?

            // Remove everything after the first "."
            int location = workingName.IndexOf('.');
            if (location > 0)
            {
                workingName = workingName.Substring(0, location);
            }

            // We remove everything that isnt alpha or a space
            Regex rgx = new Regex("[^a-zA-Z -]");
            workingName = rgx.Replace(workingName, "");
            workingName = workingName.Replace(" - ", "");

            // Remove our file extensions
            foreach (string s in FileProcessorFactory.ExtensionList)
            {
                workingName = workingName.Replace(s, "");
            }

            // Split what we have left into words
            workingName = workingName.Replace("  ", " ");
            string[] words = workingName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            List<string> parsedWords = new List<string>();

            // Look for TitleCase
            foreach (string s in words)
            {
                string[] test = Regex.Split(s, "(?<!(^|[A-Z]))(?=[A-Z])|(?<!^)(?=[A-Z][a-z])");

                if (test.Length > 1)
                {
                    parsedWords.AddRange(test);
                }
                else
                {
                    parsedWords.Add(s);
                }
            }

            // Remove months (if someone is named after a month really, outside of June they should 
            // be asking their parents some very important questions.
            parsedWords.RemoveAll(IsMonth);
            parsedWords.RemoveAll(IsDay);
            parsedWords.RemoveAll(IsTime);
            parsedWords.RemoveAll(IsAllUpper);

            // Remove duplicates
            parsedWords = parsedWords.Distinct().ToList();

            // Special words to remove
            string[] localRemoveWords = _session.Config.ProcessNameIgnore;
            List<string> filteredWords = new List<string>();
            foreach (string s in parsedWords)
            {
                if (!localRemoveWords.Contains(s.ToLower()) && s != "-")
                {
                    filteredWords.Add(s);
                }
            }

            // Now check for "containing", this is where we have partials "this" vs "thi"
            List<string> toRemove = new List<string>();
            foreach (string s in filteredWords)
            {
                foreach (string c in filteredWords)
                {
                    if ((c.StartsWith(s, StringComparison.Ordinal) || c.EndsWith(s, StringComparison.Ordinal)) && c != s)
                    {
                        toRemove.Add(s);
                    }
                }
            }
            foreach (string s in toRemove)
            {
                filteredWords.Remove(s);
            }


            // Ok we now need to work it

            // 2 words!!!
            if (filteredWords.Count == 2)
            {
                FirstName = filteredWords[0];
                LastName = filteredWords[1];
            }
            else if (filteredWords.Count > 2)
            {

                int endIndex = (filteredWords.Count - 1);
                bool buildLastName = true;

                // Work backwards
                while (buildLastName && endIndex > 0)
                {
                    LastName = filteredWords[endIndex] + LastName;


                    if (filteredWords[endIndex].StartsWith("-", StringComparison.Ordinal) || filteredWords[endIndex - 1].EndsWith("-", StringComparison.Ordinal))
                    {
                        endIndex--;
                    }
                    else
                    {
                        buildLastName = false;
                    }
                }

                // Build front
                FirstName = filteredWords[0];
                int frontIndex = 0;
                while ((filteredWords[frontIndex].EndsWith("-", StringComparison.Ordinal) || filteredWords[frontIndex + 1].StartsWith("-", StringComparison.Ordinal)) && (frontIndex < endIndex))
                {
                    FirstName = FirstName + filteredWords[frontIndex + 1];
                    frontIndex++;
                }
            }
        }

        /// <summary>
        /// Compare the specified submission to other submissions.
        /// </summary>
        /// <remarks>This is called in its own thread.</remarks>
        /// <returns>If the submission has been flagged.</returns>
        /// <param name="otherSubmissions">Other Submissions.</param>
        public bool Compare(ConcurrentBag<Submission> otherSubmissions)
        {

            // Create header information
            string foundChecks = string.Empty;
            foreach (ICheck check in Checks)
            {
                foundChecks += check.GetName() + ", ";
            }
            if (foundChecks.EndsWith(", ", StringComparison.Ordinal))
            {
                foundChecks = foundChecks.Substring(0, foundChecks.Length - 2);
            }
           
            _session.Log.Add(Markdown.Bold("[" + GUIDHash + "]") + " Evaluating " + RelativePath);
            _session.Log.Add(Markdown.Bold("[" + GUIDHash + "]") + " Found " + Checks.Count + " checks for this submission (" + foundChecks + ").");


            // Somehow it got through, so bounce it out but mark it as complete
            if (!Processor.IsProcessed())
            {

                _session.CompleteWorkUnit();
                return false;
            }



            //// Threaded
            //Parallel.ForEach(otherSubmissions, other =>
            //{
            //    // Skip current one or non processed
            //    if (other == this) return;
            //    if (!other.Processor.IsProcessed()) return;

            //    // Other checks
            //    Parallel.ForEach(Checks, check =>
            //    {
            //        check.Check(other);
            //    });
            //});

            // TODO: Add IsAborting Check on hunter

            // Non threaded approach
            foreach (ICheck check in Checks)
            {
                foreach (Submission other in otherSubmissions)
                {
                    // Skip current one or non processed
                    if (other == this) continue;
                    if (!other.Processor.IsProcessed()) continue;

                    check.Check(other);
                }
            }



            if (Flagged)
            {
                _session.Log.Add(Markdown.Bold("[" + GUIDHash + "]") + " " + RelativePath + " finished with flags.");
            }

            // Indicate one of our work units has finished.
            _session.CompleteWorkUnit();

            return Flagged;
        }

        /// <summary>
        /// Produce a markdown summary of the submission and any issues found.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Markdown.H2(RelativePath + " Report"));

            bool foundProblem = false;
            foreach (ICheck check in Checks)
            {
                if (check.Flagged())
                {
                    foundProblem = true;
                    builder.Append(check.ToString() + Platform.EndOfLine());
                }
            }

            if (!foundProblem)
            {
                builder.Append("No issues detected.");
            }

            string returnString = builder.ToString();
            if (returnString.EndsWith(Platform.EndOfLine(), StringComparison.Ordinal))
            {
                returnString = returnString.Substring(0, returnString.Length - Platform.EndOfLine().Length);
            }

            return returnString;
        }

        #endregion
    }
}
