using System;
using System.IO;
using System.Linq;

namespace Galileo.Core.Logging
{
    internal class LogInternal
    {
        public const int PreviousLogsToKeep = 5;
        public const string LogExtension = ".log";

        public string Folder { get; private set;  }
        public string Path { get; private set; }
        public bool DefaultLocation { get; private set; }
        Log _log;

        public LogInternal()
        {
            string potentialLogFolder = System.IO.Path.Combine(Platform.GetAppDataDirectory(), "Logs");


            // Verifiy that the log folder exists
            if ( !Directory.Exists(potentialLogFolder) )
            {
                try
                {
                    // Try to create the directory, just incase there is some sort of miracle to be had
                    Directory.CreateDirectory(potentialLogFolder);
                    DefaultLocation = true;
                }
                catch 
                {
                    // Looks like we are not working in our default location
                    DefaultLocation = false;
                }
            }
            else
            {
                DefaultLocation = true;
            }

            // Check we can write to the folder
            if (DefaultLocation && !Platform.CheckWriteDeletePermissions(potentialLogFolder))
            {
                DefaultLocation = false;
            }

            if ( DefaultLocation )
            {
                Folder = potentialLogFolder;
                Path = System.IO.Path.Combine(Folder, 
				                              "galileo_" + DateTime.Now.ToString(Galileo.Localization.LocalizationCache.DateLongSafeFormat) + "_" + Compare.Hash(DateTime.Now.ToString()).Substring(0,4) + LogExtension);

                // Clean up logs that are in folder already
                DirectoryInfo directory = new DirectoryInfo(potentialLogFolder);


                System.Collections.Generic.List<FileInfo> FileList = new System.Collections.Generic.List<FileInfo>();

                foreach (FileInfo f in directory.GetFiles())
                {
                    // Clear out any file that isn't a log file
                    if ( f.Extension != LogExtension )
                    {
                        f.Delete();
                    }
                    else
                    {
                        FileList.Add(f);
                    }
                }

                if ( FileList.Count > PreviousLogsToKeep)
                {
                    FileList.Sort((x, y) => string.Compare(y.Name, x.Name, StringComparison.Ordinal));

                    for (int i = PreviousLogsToKeep; i < FileList.Count; i++ )
                    {
                        FileList[i].Delete();
                    }
                }
            }
            else
            {
                Folder = System.IO.Path.GetTempPath();
                Path = System.IO.Path.Combine(Folder, "galileo.log");
            }


            // Create log file and write to it immediately
            _log = new Log();
            _log.Capture(Path, false);

            Add("Galileo.Core.LogInternal", "Log Created");
            Add("Galileo.Core.LogInternal", Platform.SystemInformation());
        }


        public void Add(string section, string content)
        {
            _log.Add("[" + section + "] " + content);
        }
        public void Raw(string content)
        {
            _log.Add(content);
        }
    }
}
