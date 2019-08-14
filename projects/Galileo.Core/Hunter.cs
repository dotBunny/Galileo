using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Galileo.Core.Logging;
using I18NPortable;

namespace Galileo.Core
{
    internal static class Hunter
	{
        public const float PercentageHoldback = 0.05f;

		/// <summary>
        /// Cancel the specified <see cref="HunterSession"/>'s workload.
        /// </summary>
        /// <param name="session">The <see cref="HunterSession" /> that is processing.</param>
        public static void Cancel(HunterSession session)
        {
            // Tell session to stop
            session.Abort();


            if (session.HasOption(HunterSessionOptions.Logging))
            {
                session.Log.Add(Markdown.Bold("PROCESS CANCELLED"));
                session.Log.FlushCache();
            }
        }

        /// <summary>
        /// Start the processing of submissions found in the session, launches a control thread.
        /// </summary>
        /// <param name="session">The <see cref="HunterSession" /> to start processing.</param>
        public static void Process(HunterSession session)
        {         
			// Start the process threaded (if a valid option)
            if (session.HasOption(HunterSessionOptions.Threaded))
            {
                Thread thread = new Thread(Hunt);
				thread.Name = "Galileo-HUNT-" + session.Name;

                // Tell the sesssion to start working, associating the thread.
                session.Start(thread);

                // Start thread
                thread.Start(session);
            }
            else
            {
				// Start non-threaded version
                Hunt(session);
            }
        }

        /// <summary>
        /// Hunt Process Control Flow
        /// </summary>
		/// <remarks>This requires the session to be setup fully at this point.</remarks>
        /// <param name="sessionObject">Session object.</param>
        internal static void Hunt(object sessionObject)
        {
            if (sessionObject == null || !(sessionObject is HunterSession))
            {
                Log.Session.Add("Core.Hunter.Hunt", "Provided session object is null or not a HunterSession");
                return;
            }

            // Recast the hunter session to its proper type
            HunterSession session = (HunterSession) sessionObject;

            // Intialize Log (if we have too)
            session.Log.Capture();
          
            // Reset session progress stats as well as all other working parts
            session.Reset();

            // Update progress, only triggering one update call to UI
            session.SetProgress(0.0f);


            // Log initialization
            if (session.HasOption(HunterSessionOptions.Logging))
            {
                session.Log.Add(Markdown.H1("Galileo " + session.Profile.PackageVersion));

                session.Log.Add(
                    Markdown.Bold("Core Library") + " " + session.Profile.CoreLibraryVersion + ", " + 
                    Markdown.Bold("Client Library") + " " + session.Profile.ClientLibraryVersion + ", " +
                    Markdown.Bold("Client Version") + " " + session.Profile.ClientVersion);

                session.Log.Add(Markdown.Emphasis("Hunting in " + session.WorkingDirectory));
                Log.Session.Add("Core.Hunter.Hunt", "Hunting in " + session.WorkingDirectory);
                session.Log.Add(Markdown.Emphasis("Ran " + session.Ran.ToString()));
                session.Log.Add(Markdown.Linefeed(), false);
            }

            // Output Config To Log
            if (session.HasOption(HunterSessionOptions.Logging))
            {
                session.Log.Add(Markdown.H1("Configuration"));
                session.Log.Add(session.Config.ToString());
                session.Log.Add(Markdown.Linefeed());
            }

            if (session.HasOption(HunterSessionOptions.Export))
            {
                foreach(var submission in session.SubmissionsBag)
                {
                    session.Exporter.Begin(string.Format("{0}.xml", submission.GUIDHash));
                    
                    session.Exporter.MetaData("x-id", submission.GUID.ToString());
                    session.Exporter.MetaData("x-file-name", submission.FileNameWithExtension);
                    session.Exporter.MetaData("x-file-date", submission.FileDate.ToString());
                    session.Exporter.MetaData("x-file-size", submission.FileSize.ToString());
                    session.Exporter.MetaData("x-first-name", submission.FirstName);
                    session.Exporter.MetaData("x-last-name", submission.LastName);
                    session.Exporter.MetaData("x-processor", submission.Processor.GetType().Name);
                    session.Exporter.MetaData("student-id", submission.StudentID);
                    session.Exporter.MetaData("creator", submission.MetaCreator);
                    session.Exporter.MetaData("date-created", submission.MetaDateCreated.ToString());
                    session.Exporter.MetaData("date-printed", submission.MetaDateLastPrinted.ToString());
                    session.Exporter.MetaData("date-modified", submission.MetaDateModified.ToString());
                    session.Exporter.MetaData("user-modified-by", submission.MetaLastModifiedBy.ToString());
                    session.Exporter.MetaData("user", submission.MetaUsername.ToString());

                    session.Exporter.TextSection("Content", submission.Content);
                    
                    session.Exporter.End();
                }
            }
                     
            // Mark start time
            DateTime InitializeStart = DateTime.Now;
          
            // NOTE: We shouldnt be moving things around in the sense of the base folder,
            // it shouldn't be bad, and moving everything causes problems. I've commented
            // this out for now, but not sure if this is the right move. Changing the working directory in the 
            // new setup breaks many things. (and most likely would have caused issues with the log in the old
            // setup.

            // <snip>


            // Validate Base Directory
            //session.Log.Add("Validating base path ...");

            // 
            // If we don't have a valid base diretory we need to try and make one
            //if (!Platform.IsValidPath(session.WorkingDirectory) )
            //{
            //    string cleanPath = Platform.CleanPath(session.WorkingDirectory);

            //    Directory.Move(session.WorkingDirectory, cleanPath);

            //    // Making seems to have failed, bail out!
            //    if (!Directory.Exists(cleanPath))
            //    {
            //        session.Log.Add("Unable to move " + session.WorkingDirectory + " to a valid path. Aborting.");
            //        session.InvokeComplete();
            //        return;
            //    }
            //    session.WorkingDirectory = cleanPath;
            //}

            // </snip>
                     
			// Output current config
            session.Config.WriteConfig(HunterConfig.GetConfigPath(session.WorkingDirectory));

            // Cross Check Assignments
            session.Log.Add(Markdown.H1("Evaluating Files"));
            DateTime EvaluationStart = DateTime.Now;
          
            // Setup our work units
            session.WorkUnits = session.SubmissionCount;

            // Report Search Item Count
            session.Analytics.Event("Process", "Start", session.SubmissionCount);


            // Setup Parallel
            ParallelOptions _parallelOptions = new ParallelOptions();
            session.CancellationToken = new CancellationTokenSource();

          
            // Initial resources limiting
            if (session.Config.PlatformParallelismMaxDegrees == -1 || 
                session.Config.PlatformParallelismMaxDegrees > Environment.ProcessorCount || 
                session.Config.PlatformParallelismMaxDegrees < -1 ||
                session.Config.PlatformParallelismMaxDegrees == 0)
            {
                _parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            }
            else
            {
                _parallelOptions.MaxDegreeOfParallelism = session.Config.PlatformParallelismMaxDegrees;
            }
            _parallelOptions.CancellationToken = session.CancellationToken.Token;

            
            // Send everything out to be compared across as many threads as we can
            try
            {
                Parallel.ForEach(session.SubmissionsBag, _parallelOptions, (s) =>
                {
                    s.Compare(session.SubmissionsBag);

                    // If a parallel process completes when aborted, we need to make sure we dont start up another
                    if (_parallelOptions.CancellationToken == null)

                    {

                        // Do nothing as we've already worked the cancel logic and this is a left over parallel process

                    }
                    else if (_parallelOptions.CancellationToken.IsCancellationRequested)

                    {

                        // Do nothing as we've already requested the cancellation

                    }
                    else

                    {

                        _parallelOptions.CancellationToken.ThrowIfCancellationRequested();

                    }
                });
            }
            catch (OperationCanceledException)
            {
                if (_parallelOptions != null && _parallelOptions.CancellationToken != null)

                {

                    session.Log.Add("Cancel Detected.");

                }
            }
            catch (Exception e)
            {
                session.Log.Add(e.Message);
            }
            finally
            {
                session.CancellationToken.Dispose();
                session.CancellationToken = null;
            }  
         
            // Calculate Analytics
			session.Log.Add("Calculating Analytics");
            Dictionary<string, int> stats = new Dictionary<string, int>();
            foreach(Submissions.Submission s in session.SubmissionsBag)
            {
                foreach (Checks.ICheck c in s.Checks)
                {
                    if (c.Flagged())
                    {
                        if (stats.ContainsKey(c.GetName()))
                        {
                            stats[c.GetName()]++; 
                        } else {
                            stats.Add(c.GetName(), 1);
                        }
                            
                    }
                }
            }

            if (session.HasOption(HunterSessionOptions.Analytics))
            {
                // Report Stats
                foreach(KeyValuePair<string, int> item in stats)
                {
                    session.Analytics.Event("Process", "Flagged", item.Key, item.Value);
                }
                
                // Report Process Timing
                session.Analytics.Timing("Process", "Duration", session.WorkUnits + " Files", (int)(DateTime.Now - EvaluationStart).TotalMilliseconds);

            }

            // Update log when completed
            if (session.HasOption(HunterSessionOptions.Logging))
            {
                session.Log.Add(Markdown.Linefeed(), false);
                session.Log.Add(Markdown.Bold("Evaluating Files Completed (" + (DateTime.Now - EvaluationStart).TotalSeconds + "s)"));
            }
            
            
            // Generate Report
            session.SetProgress(1-PercentageHoldback);
            session.Log.Add("Generating Report ...");

            Report.Layout newReport = new Report.Layout(session);
            File.WriteAllText(HunterConfig.GetReportPath(session.WorkingDirectory), newReport.ToString());
            Log.Session.Add("Core.Hunter.Hunt", "Report Generated @ " + HunterConfig.GetReportPath(session.WorkingDirectory));

            if (session.HasOption(HunterSessionOptions.Logging))
            {
              session.Log.Add(Markdown.H1("Process.Log.Header".Translate()));
            
              // Generate Numbers
              int flaggedCount = 0;
              foreach (Submissions.Submission s in session.SubmissionsBag)
              {
                  if (s.Flagged)
                  {
                      flaggedCount += 1;
                  }
              }

              session.Log.Add("Process.Log.EvaluatedSubmissions".Translate(session.SubmissionCount));
              session.Log.Add("Process.Log.FlaggedSubmissions".Translate(flaggedCount));
              session.Log.Add(Markdown.Linefeed());

              // Make sure cache is outputted
              session.Log.FlushCache();
            }

            // We're done with this!
			session.EndProcess();

            // Trigger Complete
            session.InvokeComplete();
        }
    }
}