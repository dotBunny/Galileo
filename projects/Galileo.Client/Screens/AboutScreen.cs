using Galileo.Core;
using Galileo.Core.Logging;

namespace Galileo.Client.Screens
{
    /// <summary>
    /// Galileo's About Screen Logic
    /// </summary>
    public static class AboutScreen
    {
        #region Methods

        /// <summary>
        /// Return the long format of the client library version information.
        /// </summary>
        /// <returns>A long format version string.</returns>
        /// <param name="session">A HunterProfile with a defined client library version.</param>
        public static string ClientLibraryVersionString(HunterProfile session)
        {
            return "Galileo.Client-" + session.ClientLibraryVersion;
        }

        /// <summary>
        /// Return the long format of the client version information.
        /// </summary>
        /// <returns>A long format version string.</returns>
        /// <param name="session">A HunterProfile with a defined client version.</param>
        public static string ClientVersionString(HunterProfile session)
        {
#if __MACOS__
            return "Galileo.Client.Mac-" + session.ClientVersion;
#elif __WINDOWS__
            return "Galileo.Client.Win-" + session.ClientVersion;
#elif __LINUX__
            return "Galileo.Client.Linux-" + session.ClientVersion;
#else
            return "Galileo.Client.Platform-" + session.ClientVersion;
#endif
        }

        /// <summary>
        /// Return the long format of the core version information.
        /// </summary>
        /// <returns>The long format version string.</returns>
        /// <param name="session">A HunterProfile with a defined core version.</param>
        public static string CoreLibraryVersionString(HunterProfile session)
        {
            return "Galileo.Core-" + session.CoreLibraryVersion;
        }

        /// <summary>
        /// Returns the platform specific information to be used in the system information box.
        /// </summary>
        /// <returns>The system information content.</returns>
        public static string DebugInformation()
        {
            return Platform.SystemInformation();
        }

        /// <summary>
        /// Opens the EULA in the native associated RTF editor.
        /// </summary>
        public static void EULAButton_Click()
        {
            var filePath = System.IO.Path.Combine(Platform.GetAppDataDirectory(), "EULA.rtf");
            if (System.IO.File.Exists(filePath))
            {
#if __MACOS__
                Platform.Run("open", filePath, false);
#else
                Platform.Run(filePath, string.Empty, false);
#endif
            }
            else
            {
                Log.Session.Add("Client.Screens.AboutScreen.OpenEULA", "Unable to open: " + filePath);
            }
        }

        /// <summary>
        /// Opens the Third Party Licenses in the native associated RTF editor.
        /// </summary>
        public static void ThirdPartyLicensesButton_Click()
        {
            var filePath = System.IO.Path.Combine(Platform.GetAppDataDirectory(), "ThirdPartyLicenses.rtf");
            if (System.IO.File.Exists(filePath))
            {
#if __MACOS__
                Platform.Run("open", filePath, false);
#else
                Platform.Run(filePath, string.Empty, false);
#endif
            }
            else
            {
               Log.Session.Add("Client.Screens.AboutScreen.OpenThirdPartyLicenses", "Unable to open: " + filePath);
            }
            //  Platform.Run(Links.Website + Links.ThirdPartyLicenses, string.Empty, false);
        }

        /// <summary>
        /// Opens the internal logs folder.
        /// </summary>
        public static void LogsButton_Click()
        {
            if (System.IO.File.Exists(Log.Session.Path))
            {
#if __MACOS__
                Platform.Run("open", Log.Session.DefaultLocation ? Log.Session.Folder : Log.Session.Path, true);
#elif __WINDOWS__
                if (Log.Session.DefaultLocation)
                {
                    if (System.IO.Directory.Exists("C:\\Windows\\SysWOW64"))
                    {
                        Platform.Run("C:\\Windows\\SysWOW64\\explorer.exe", Log.Session.Folder, false);
                    }
                    else
                    {
                        Platform.Run("C:\\Windows\\explorer.exe", Log.Session.Folder, false);
                    }
                }
                else
                {

                    Platform.Run("C:\\Windows\\System32\\notepad.exe", Log.Session.Path, false);
                }
#else
                if (Log.Session.DefaultLocation)
                {
                    Platform.Run(Log.Session.Folder, string.Empty, false);
                }
                else
                {
                    Platform.Run(Log.Session.Path, string.Empty, false);
                }
#endif
            }
            else
            {
                Log.Session.Add("Client.Screens.AboutScreen.OpenLogsFolder", "Unable to find Galileo log file.");
            }
        }
        #endregion
    }
}
