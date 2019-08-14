using System;
using System.IO;
using Galileo.Core.Logging;

namespace Galileo.Client
{
    /// <summary>
    /// Galileo's Embeded Resources Logic
    /// </summary>
    public static class Resources
    {
        #region Constant Fields

        /// <summary>
        /// The Preferences Icon
        /// </summary>
        public const string PreferencesIcon = FontAwesome.FAWrench;

        /// <summary>
        /// The Updates Icon (No Updates)
        /// </summary>
        public const string UpdatesIconNone = FontAwesome.FADownload;

        /// <summary>
        /// The Updates Icon (Found Updates)
        /// </summary>
        public const string UpdatesIconFound = FontAwesome.FAExclamationTriangle;

        /// <summary>
        /// The Process Icon
        /// </summary>
        public const string ProcessIcon = FontAwesome.FACog;

        /// <summary>
        /// The Activate Icon (Locked)
        /// </summary>
        public const string ActivateIcon = FontAwesome.FALock;

        #endregion

        #region Methods


        /// <summary>
        /// Initialize Resources
        /// </summary>
        public static void Initialize()
        {
            string eulaSource = Path.Combine(Core.Platform.GetAppDirectory(), "EULA.rtf");
            string eulaDestination = Path.Combine(Core.Platform.GetAppDataDirectory(), "EULA.rtf");
            string licensesSource = Path.Combine(Core.Platform.GetAppDirectory(), "ThirdPartyLicenses.rtf");
            string licensesDestination = Path.Combine(Core.Platform.GetAppDataDirectory(), "ThirdPartyLicenses.rtf");

            if (Core.Platform.IsSourceFileNewer(eulaSource, eulaDestination))
            {
                try
                {
                    File.Copy(eulaSource, eulaDestination, true);
                }
                catch (Exception e)
                {
                    Log.Session.Add("Client.Resources.Initialize", e.Message);
                }

            }
            if (Core.Platform.IsSourceFileNewer(licensesSource, licensesDestination))
            {
                try
                {
                    File.Copy(licensesSource, licensesDestination, true);
                }
                catch (Exception e)
                {
                    Log.Session.Add("Client.Resources.Initialize", e.Message);
                }
            }
        }

        #endregion
    }
}
