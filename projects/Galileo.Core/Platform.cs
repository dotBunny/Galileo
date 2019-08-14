using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Galileo.Core
{
    internal static class Platform
    {
        internal const int DevicePrefixLength = 4;

        /// <summary>
        /// Operating System
        /// </summary>
        enum OperatingSystem
        {
            /// <summary>
            /// Undefined
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Microsoft Windows Based
            /// </summary>
            Windows = 1,

            /// <summary>
            /// macOS Based
            /// </summary>
            macOS = 2,

            /// <summary>
            /// Linux Based
            /// </summary>
            Linux = 3
        }

        /// <summary>
        /// The cached operating system which the execution of the assembly is happening on.
        /// </summary>
        static OperatingSystem _cachedOperatingSystem = OperatingSystem.Unknown;

        private static string CachedAppDirectory;
        private static string CachedAppDataDirectory;



        public static string EndOfLine()
        {
            if (GetOS() == OperatingSystem.Windows)
            {
                return "\r\n";
            }
            return "\n";
        }


        /// <summary>
        /// Get the Operating System which execution is currently happening on, and cache it.
        /// </summary>
        /// <returns>The os.</returns>
        static OperatingSystem GetOS()
        {
            // Check for cache
            if (_cachedOperatingSystem != OperatingSystem.Unknown)
            {
                return _cachedOperatingSystem;
            }

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                _cachedOperatingSystem = OperatingSystem.macOS;

            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                _cachedOperatingSystem = OperatingSystem.Linux;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                _cachedOperatingSystem = OperatingSystem.Windows;
            }

            return _cachedOperatingSystem;
        }

        public static void Run(string path, string arguments, bool hide)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = path;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;
            
            // No need to show it
            if (hide)
            {
                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
            }

            if (arguments != string.Empty)
            {
				Galileo.Core.Logging.Log.Session.Add("Core.Platform.Run", path + " " + arguments);
            }
            else
            {
				Galileo.Core.Logging.Log.Session.Add("Core.Platform.Run", path);
            }


            proc.Start();
        }


        public static string GetAppDataDirectory()
        {
            if (string.IsNullOrEmpty(CachedAppDataDirectory))
            {
                CachedAppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Galileo");
            }
            return CachedAppDataDirectory;
        }

        public static string GetAppDirectory()
        {
            if (string.IsNullOrEmpty(CachedAppDirectory))
            {
                CachedAppDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
            return CachedAppDirectory;   
        }

        public static bool IsSourceFileNewer(string source, string destination)
        {
            FileInfo sourceFile = new FileInfo(source);
            FileInfo destFile = new FileInfo(destination);
            if (destFile.Exists)
            {
                if (sourceFile.LastWriteTime > destFile.LastWriteTime)
                {
                    return true;
                }
                return false;
            }
            return true;
        }



        static List<string> GetDirectories(string path, string ignoreBasePath)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);

            List<string> dirList = new List<string>();

            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        if (ignoreBasePath != string.Empty)
                        {
                            if (subDir.StartsWith(ignoreBasePath, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                        }

                        dirList.Add(subDir);
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }

            }

            return dirList;
        }

        internal static IEnumerable<string> GetFiles(string path, string ignoreBasePath)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        if (ignoreBasePath != string.Empty)
                        {
                            if (subDir.StartsWith(ignoreBasePath, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                        }
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }

        private static char[] InvalidFileNameCharactersCache = Path.GetInvalidFileNameChars();
        private static char[] InvalidPathChars = Path.GetInvalidPathChars();

        internal static string CleanFileName(string name)
        {
            return new string(name.Where(x => !InvalidFileNameCharactersCache.Contains(x)).ToArray());//.Where(x => !AdditionalInvalidChars.Contains(x)).ToArray());
        }
        internal static string CleanPath(string path)
        {

            string cleanPath = new string(path.Where(x => !InvalidPathChars.Contains(x)).ToArray());//.Where(x => !AdditionalInvalidChars.Contains(x)).ToArray());

            // Remove : past the start
            string prefix = cleanPath.Substring(0, DevicePrefixLength);
            string parsed = cleanPath.Substring(DevicePrefixLength).Replace(":", "_");


            return prefix + parsed;
        }

        public static bool IsValidPath(string path)
        {
            if (path.Any(c => InvalidPathChars.Contains(c)))
            {
                return false;
            }

            // Check for : past the start
            if (path.Substring(DevicePrefixLength).Contains(":"))
            {
                return false;
            }


            return true;
        }
        public static bool IsValidFileName(string name)
        {
            if (name.Any(c => InvalidFileNameCharactersCache.Contains(c)))
            {
                return false;
            }
            return true;
        }

        public static string GetEmbeddedResourceFile(string filename)
        {
            string result = string.Empty;
            var a = System.Reflection.Assembly.GetExecutingAssembly();
            using (var s = a.GetManifestResourceStream(filename))
            using (var r = new System.IO.StreamReader(s))
            {
                result = r.ReadToEnd();
            }
            return result;
        }

        public static bool CheckWriteDeletePermissions(string path)
        {
            try
            {

                string file = Path.Combine(path, "PermCheck_" + Compare.Hash(DateTime.Now.ToString()).Substring(0, 5) + ".tmp");
                File.Create(file).Close();
                File.Delete(file);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool IsNetworkPath(string path)
        {
            return path.StartsWith("\\", StringComparison.Ordinal);
        }

        public static string SystemInformation()
        {
            // Build Platform String
            string platform = "Unknown";
            switch(GetOS())
            {
                case OperatingSystem.Linux:
                    platform = "Linux";
                    break;
                case OperatingSystem.macOS:
                    platform = "macOS";
                    break;
                case OperatingSystem.Windows:
                    platform = "Windows";
                    break;
            }

            return  "## Platform ##" + EndOfLine() +
                    Environment.UserName + " @ " + Environment.MachineName + EndOfLine() +
                    platform + " (" + System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString() + ") " + Environment.OSVersion + EndOfLine() +
                    Environment.ProcessorCount + " Processors (Includes HT)" + EndOfLine() +
                    Environment.SystemPageSize + " Bytes In OS Memory Page" + EndOfLine() +
                    EndOfLine() +
                    "## Frameworks ##" + EndOfLine() +
                    System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        }
        
        // Certain File-Systems have reserved filenames that cannot be used
        // From: https://en.wikipedia.org/wiki/Filename#Reserved_characters_and_words
        private static string[] InvalidFileNames = {
            // FAT12, FAT16 and FAT32
            "$IDLE$", "AUX", "COM1", "COM2", "COM3", "COM4", "CON", "CONFIG$", "CLOCK$", "KEYBD$",
            "LPT1", "LPT2", "LPT3", "LPT4", "LST", "NUL", "PRN", "SCREEN$",
            // NTFS
            "$AttrDef", "$BadClus", "$Bitmap", "$Boot", "$LogFile", "$MFT", "$MFTMirr",
            "pagefile.sys", "$Secure", "$UpCase", "$Volume", "$Extend", "$ObjId", "$Quota", "$Reparse"
        };
        
        // File Systems have different allowances of what is an acceptable character in a file or path name
        // From: https://en.wikipedia.org/wiki/Filename#Reserved_characters_and_words
        //       https://kb.acronis.com/content/39790
        //       https://amigotechnotes.wordpress.com/2015/04/02/invalid-characters-in-file-names/
        //       https://www.dropbox.com/help/syncing-uploads/files-not-syncing
        private static readonly char[] FileSystemDisallowedCharacters = {
            (char) 0x7F, // (DEL) FAT12, FAT16, FAT32
            '"',  // FAT12, FAT16, FAT32, NTFS, Samba
            '*',  // FAT12, FAT16, FAT32, NTFS, Dropbox
            '/',  // FAT12, FAT16, FAT32, NTFS, Samba, ext4, HFS (Although technically okay, Finder has issues with it)
            ':',  // FAT12, FAT16, FAT32, NTFS, Samba, Dropbox, HFS
            '<',  // FAT12, FAT16, FAT32, NTFS, Samba, Dropbox
            '>',  // FAT12, FAT16, FAT32, NTFS, Samba, Dropbox
            '?',  // FAT12, FAT16, FAT32, NTFS, Samba, Dropbox
            '\\', // FAT12, FAT16, FAT32, NTFS, Samba, Dropbox
            '|',  // FAT12, FAT16, FAT32, NTFS, Samba, Dropbox
            '+',  // FAT12, FAT16, FAT32
            ',',  // FAT12, FAT16, FAT32
            // '.',  // FAT12, FAT16, FAT32
            ';',  // FAT12, FAT16, FAT32
            '=',  // FAT12, FAT16, FAT32
            '[',  // FAT12, FAT16, FAT32
            ']'   // FAT12, FAT16, FAT32
        };

        internal static string SafeFileSystemName(string fileName, char censorCharacter = '_')
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return Path.GetRandomFileName();
            }

            if (InvalidFileNames.Contains(fileName))
            {
                return Path.GetRandomFileName();
            }

            // Filter out names and characters that not allowed in filenames or path names for the 
            // FAT12, FAT16, FAT32, NTFS and HFS file-systems.
            // 
            // A new string will be returned if the name has been filtered, otherwise the original
            // string will be returned instead.

            StringBuilder fileNameBuilder = null;
            int length = fileName.Length;
            for(int i=0;i < length;i++)
            {
                char character = fileName[i];
                bool censor = false;

                // FAT-12, FAT-16, FAT-32 and NTFS disallow characters from
                // 0x00 (NUL) to 0x1F (US)
                if (character >= 0x00 && character <= 0x1F)
                    censor = true;
                // Check characters from various filesystems and online services
                else if (FileSystemDisallowedCharacters.Contains(character))
                    censor = true;

                if (censor == false)
                {
                    if (fileNameBuilder != null)
                        fileNameBuilder.Append(character);
                    continue;
                }
                else
                {
                    if (fileNameBuilder == null)
                    {
                        fileNameBuilder = new StringBuilder(length);
                        fileNameBuilder.Append(fileName, 0, i);
                    }

                    fileNameBuilder.Append(censorCharacter);
                }
            }

            // Future Notes:
            //      Office tends to use ~ as a temporary marker for some files this is used to mark home on *nix.
            //      Dropbox doesn't sync files ending with a .

            if (fileNameBuilder != null)
                return fileNameBuilder.ToString();

            return fileName;
        }
        
        internal static string[] SplitArchivePath(string archivePath)
        {
            return archivePath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }
                    , StringSplitOptions.RemoveEmptyEntries);
        }

        internal static string[] CleanArchivePaths(string[] paths)
        {
            int count = paths.Length;
            for(int i=0;i < count;i++)
            {
                // paths[i] = CleanFileName(paths[i]);
                paths[i] = SafeFileSystemName(paths[i]);
            }
            return paths;
        }
        
    }
}
