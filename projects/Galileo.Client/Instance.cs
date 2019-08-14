using System;
using System.Collections.Generic;
using Galileo.Core.Logging;

namespace Galileo.Client
{
    /// <summary>
    /// Galileo Client Instance Logic
    /// </summary>
    public static class Instance
    {
        #region Enums

        /// <summary>
        /// The current state/screen of the client.
        /// </summary>
        public enum State
        {
            /// <summary>
            /// Initial State
            /// </summary>
            Startup,
            /// <summary>
            /// Process Screen
            /// </summary>
            Hunt,
            /// <summary>
            /// Preferences Screen
            /// </summary>
            Preferences,
            /// <summary>
            /// Updates Screen
            /// </summary>
            Updates,
            /// <summary>
            /// About Screen
            /// </summary>
            About,
            /// <summary>
            /// Blank Screen
            /// </summary>
            Blank
        }

        #endregion

        #region Properties

        /// <summary>
        /// Is there any hunter working?
        /// </summary>
        /// <value><c>true</c> if one is working; otherwise, <c>false</c>.</value>
        public static bool IsWorking
        {
            get
            {
                foreach(KeyValuePair<string, HuntHandler> item in Hunters)
                {
                    if (item.Value.IsWorking) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// The current Galileo instances version information.
        /// </summary>
        public static Core.HunterProfile Profile { get; private set; }

        /// <summary>
        /// A collection of Hunters used by the given client instance.
        /// </summary>
        /// <value>The hunters.</value>
        public static System.Collections.Generic.Dictionary<string, HuntHandler> Hunters { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize a Galileo client for work.
        /// </summary>
        /// <param name="profile">A HunterProfile containing the version information for all libraries/executables.</param>
        public static void Initialize(Core.HunterProfile profile)
        {
            // Assign our created session for reference all over the place
            Profile = profile;

            // Set Localization
            Localization.LocalizationProvider.Init();
            Localization.LocalizationProvider.SetLocale(profile.Localization);

            // Setup internal log
			Galileo.Core.Logging.Log.Session.Add("Client.Instance.Initialize", profile.ToString());
			Galileo.Core.Logging.Log.Session.Add("Client.Instance.Initialize", DateTime.Now.ToString(Localization.LocalizationCache.DateLongFormat));
			if (!Galileo.Core.Logging.Log.Session.DefaultLocation)
            {
				Galileo.Core.Logging.Log.Session.Add("Client.Instance.Initialize", "Instance log file was not able to be placed in the logs folder, it will use a temporary file instead.");
            }

            // Copy resource files into place so that we can access them from Galileo
            Resources.Initialize();
			Galileo.Core.Logging.Log.Session.Add("Client.Instance.Initialize", "Resources are in place.");

            // Create Holders
            Hunters = new System.Collections.Generic.Dictionary<string, HuntHandler>();
        }

        /// <summary>
        /// Add to internal log
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="message">The message.</param>
        public static void Log(string section, string message)
        {
			Galileo.Core.Logging.Log.Session.Add(section, message);
        }

        /// <summary>
        /// Properly shutdown Galileo instance.
        /// </summary>
        public static void Shutdown()
        {
            // Cancel any worker threads
            foreach (KeyValuePair<string, HuntHandler> h in Hunters)
            {
                if (h.Value.IsWorking)
                {
                    h.Value.Cancel();
                }
            }
        }

        #endregion
    }
}
