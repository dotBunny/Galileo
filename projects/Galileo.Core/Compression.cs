using System;
using System.IO.Compression;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Galileo.Core
{
    static class Compression
    {
        public static bool IsArchive(string extension)
        {
            switch(extension.ToLower())
            {
                case ".zip":
                    return true;
            }
            return false;
        }

        public static IEnumerable<string> Extract(string file, string basePath)
        {
            // Extract and clean up names
            string path = Path.Combine(basePath, "A_" + Compare.Hash(Path.GetFileName(file)).Substring(0,6));

            // Create the directory to be used for storing the archives crap
            Directory.CreateDirectory(path);

            if (!File.Exists(file))
            {
                throw new FileNotFoundException("Unable to find/access archive (" + file + ").");
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("Unable to find/access the created directory (" + path + ").");
            }

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 0x1000, useAsync: false);
            try
            {
                ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Read, leaveOpen: false, entryNameEncoding: System.Text.Encoding.ASCII);

                if (zip == null)
                    throw new ArgumentNullException(nameof(zip));

                if (path == null)
                    throw new ArgumentNullException(nameof(path));

                // Rely on Directory.CreateDirectory for validation of destinationDirectoryName.

                // Note that this will give us a good DirectoryInfo even if destinationDirectoryName exists:
                DirectoryInfo di = Directory.CreateDirectory(path);
                string destinationDirectoryFullPath = di.FullName;

                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    // Handle malformed paths inside of zip
                    if (entry.FullName.EndsWith("/", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith("\\", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string cleanPass = Platform.CleanPath(entry.FullName);
                    string fileName = Path.GetFileName(cleanPass);
                    string pass = cleanPass.TrimEnd(fileName.ToArray<char>());

                    string fileDestinationPath = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, pass, fileName));


                    if (!fileDestinationPath.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                        throw new IOException("ExtractingResultsInOutside");

                    // If it is a file:
                    // Create containing directory:
                    Directory.CreateDirectory(Path.GetDirectoryName(fileDestinationPath));
                    entry.ExtractToFile(fileDestinationPath, overwrite: true);
                }

 
                fs.Dispose();
            }
            catch (Exception e)
            {
                fs.Dispose();
                Logging.Log.Session.Add("Core.Compression.Extract", e.Message);
            }

            // Return files that were extracted
            return Platform.GetFiles(path, string.Empty);

        }


    }

    
    interface IArchiveInfo
    {
        bool IsOpen { get; }
        
        FileTypes.FileType FileType { get; }

        void Open();
        
        void Close();
        
        bool Extract(string fullName, string path);
        
        IEnumerator<string> GetEnumerator();
    }

    class ZipArchiveInfo : IArchiveInfo
    {
        #region Fields
        private string _path;
            
        private ZipArchive _info;
        #endregion
        
        public ZipArchiveInfo(string path)
        {
            _path = path;
            Open();
        }
     
        public bool IsOpen => _info != null;

        public FileTypes.FileType FileType => FileTypes.Types.Zip;

        public void Open()
        {
            if (IsOpen == false)
            {
                _info = ZipFile.OpenRead(_path);
            }
        }

        public void Close()
        {
            if (IsOpen)
            {
                _info.Dispose();
                _info = null;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach(var entry in _info.Entries)
            {
                // Skip over any directories. Although in testing
                // not all in zip files have these come up.
                string name = entry.FullName;
                int    nameLength = name.Length - 1;

                if (name.LastIndexOf(System.IO.Path.DirectorySeparatorChar) == nameLength || 
                    name.LastIndexOf(System.IO.Path.AltDirectorySeparatorChar) == nameLength)
                {
                    continue;
                }


                yield return name;
            }
        }

        public bool Extract(string fullName, string path)
        {
            if (IsOpen)
            {
                var entry = _info.GetEntry(fullName);
                if (entry == null)
                    return false;
                entry.ExtractToFile(path, true);
            }
            return false;
        }
    }
}
