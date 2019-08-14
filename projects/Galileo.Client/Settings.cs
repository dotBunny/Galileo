using System;

using Newtonsoft.Json;

using Plugin.Settings;

using Plugin.Settings.Abstractions;



namespace Galileo.Client

{

    /// <summary>

    /// Client Settings

    /// </summary>

    public static class Settings

    {

        #region Constant Fields



        /// <summary>

        /// The stored preference key for if anonymous usage data should be sent.

        /// </summary>

        internal const string GeneralAnonymousUsageStatsKey = "General.AnonymousUsageStats";



        /// <summary>

        /// The stored preference key for the selected localization.

        /// </summary>

        internal const string GeneralLocalizationKey = "General.Localization";



        /// <summary>

        /// The stored preference key for if reports should be opened automatically.

        /// </summary>

        internal const string GeneralOpenReportsAutomaticallyKey = "General.OpenReportsAutomatically";



        /// <summary>

        /// The stored preference key of the process default config.

        /// </summary>

        internal const string ProcessDefaultConfigKey = "Process.DefaultConfig";



        /// <summary>

        /// The stored preference key of the process default target folder.

        /// </summary>

        internal const string ProcessDefaultTargetFolderKey = "Process.DefaultTargetFolder";



        /// <summary>

        /// The stored check for updates preferences key.

        /// </summary>

        internal const string UpdatesCheckForUpdatesKey = "Updates.CheckForUpdates";



        /// <summary>

        /// The stored updates channel preferences key.

        /// </summary>

        internal const string UpdatesChannelKey = "Updates.Channel";



        /// <summary>

        /// The stored update frequency preferences key.

        /// </summary>

        internal const string UpdatesFrequencyKey = "Updates.Frequency";



        /// <summary>

        /// The stored updates ignore version preferences key.

        /// </summary>

        internal const string UpdatesIgnoreVersionKey = "Updates.IgnoreVersion";



        /// <summary>

        /// The stored last update check preferences key.

        /// </summary>

        internal const string UpdatesLastCheckKey = "Updates.LastCheck";



        /// <summary>

        /// Stored window location preference key.

        /// </summary>

        internal const string WindowLocationKey = "Window.Location";



        /// <summary>

        /// Stored window size preference key.

        /// </summary>

        internal const string WindowSizeKey = "Window.Size";



        /// <summary>

        /// The windows minimum size horizontally.

        /// </summary>

        internal const int WindowSizeMinimumX = 800;



        /// <summary>

        /// The windows minimum size vertically.

        /// </summary>

        internal const int WindowSizeMinimumY = 500;



        #endregion



        #region Properties



        /// <summary>

        /// Can we upload anonymous usage stats?

        /// </summary>

        /// <value><c>true</c> if allowed; otherwise, <c>false</c>.</value>

        public static bool AnonymousUsageStats

        {

            get

            {

                return ApplicationSettings.GetValueOrDefault(GeneralAnonymousUsageStatsKey, true);

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(GeneralAnonymousUsageStatsKey, value);

            }

        }



        /// <summary>

        /// The default config to use when processing submissions.

        /// </summary>

        /// <value>The default config.</value>

        public static Core.HunterConfig DefaultConfig

        {

            get

            {

                return JsonConvert.DeserializeObject<Core.HunterConfig>(

                        ApplicationSettings.GetValueOrDefault(

                            ProcessDefaultConfigKey,

                            JsonConvert.SerializeObject(new Core.HunterConfig(), Formatting.Indented)));

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(ProcessDefaultConfigKey, JsonConvert.SerializeObject(value, Formatting.Indented));

            }

        }



        /// <summary>

        /// The default folder to first target when a new process is made.

        /// </summary>

        /// <value>The folder path.</value>

        public static string DefaultFolder

        {

            get

            {

                return ApplicationSettings.GetValueOrDefault(ProcessDefaultTargetFolderKey, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(ProcessDefaultTargetFolderKey, value);

            }

        }



        /// <summary>

        /// Should a particular package version be ignored when checking for auto-udpates?

        /// </summary>

        /// <value>The package version to ignore.</value>

        public static string IgnoreVersion

        {

            get

            {



                return ApplicationSettings.GetValueOrDefault(UpdatesIgnoreVersionKey, string.Empty);

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(UpdatesIgnoreVersionKey, value);

            }

        }



        /// <summary>

        /// The last timestamp updates were checked for.

        /// </summary>

        /// <value>The last time (UTC) Galileo checked for updates.</value>

        public static DateTime LastUpdateCheck

        {

            get

            {

                return ApplicationSettings.GetValueOrDefault(UpdatesLastCheckKey, DateTime.Parse("2018-04-20 08:00:00")).ToUniversalTime();

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(UpdatesLastCheckKey, value.ToUniversalTime());

            }

        }



        /// <summary>

        /// What localization should we be using?

        /// </summary>

        /// <value>Localization code</value>

        public static string Localization

        {

            get

            {

                return ApplicationSettings.GetValueOrDefault(GeneralLocalizationKey, "en-CA");

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(GeneralLocalizationKey, value);

            }

        }



        /// <summary>

        /// Should reports be opened automatically upon finishing processing?

        /// </summary>

        /// <value><c>true</c> if open reports automatically; otherwise, <c>false</c>.</value>

        public static bool OpenReportsAutomatically

        {

            get

            {

                return ApplicationSettings.GetValueOrDefault(GeneralOpenReportsAutomaticallyKey, true);

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(GeneralOpenReportsAutomaticallyKey, value);

            }

        }



        /// <summary>

        /// Should the client check for udpates?

        /// </summary>

        /// <value>true/false</value>

        public static bool ShouldCheckForUpdates

        {

            get

            {

                return ApplicationSettings.GetValueOrDefault(UpdatesCheckForUpdatesKey, true);

            }

            set

            {

                ApplicationSettings.AddOrUpdateValue(UpdatesCheckForUpdatesKey, value);

            }

        }







        /// <summary>

        /// The update channel to query for new updates.

        /// </summary>

        /// <value>The channel name.</value>

        public static Update.UpdateProvider.Channel UpdatesChannel

        {

            get

            {

                if (ApplicationSettings.GetValueOrDefault(UpdatesChannelKey, "Release") == "Beta")

                {

                    return Update.UpdateProvider.Channel.Beta;

                }

                return Update.UpdateProvider.Channel.Release;

            }

            set

            {

                if (value == Update.UpdateProvider.Channel.Beta)

                {

                    ApplicationSettings.AddOrUpdateValue(UpdatesChannelKey, "Beta");

                }

                else

                {

                    ApplicationSettings.AddOrUpdateValue(UpdatesChannelKey, "Release");

                }

            }

        }



        /// <summary>

        /// The frequency to check for updates.

        /// </summary>

        /// <value>The frequency descriptor to use.</value>

        public static Update.UpdateProvider.Frequency UpdateCheckFrequency

        {

            get

            {

                switch (ApplicationSettings.GetValueOrDefault(UpdatesFrequencyKey, "Weekly"))

                {

                    case "Daily":

                        return Update.UpdateProvider.Frequency.Daily;

                    case "Monthly":

                        return Update.UpdateProvider.Frequency.Monthly;

                }

                return Update.UpdateProvider.Frequency.Weekly;

            }

            set

            {

                if (value == Update.UpdateProvider.Frequency.Daily)

                {

                    ApplicationSettings.AddOrUpdateValue(UpdatesFrequencyKey, "Daily");

                }

                else if (value == Update.UpdateProvider.Frequency.Monthly)

                {

                    ApplicationSettings.AddOrUpdateValue(UpdatesFrequencyKey, "Monthly");

                }

                else

                {

                    ApplicationSettings.AddOrUpdateValue(UpdatesFrequencyKey, "Weekly");

                }

            }

        }



        /// <summary>

        /// Gets or sets the window location.

        /// </summary>

        /// <value>The window location.</value>

        public static Core.Types.Vector2<int> WindowLocation

        {

            get

            {

                var setting = new Core.Types.Vector2<int>();

                setting.FromString(ApplicationSettings.GetValueOrDefault(WindowLocationKey, "-1,-1"));

                return setting;

            }

            set

            {

                // Only save if none of the window is off the screen

                if ( value.X >= 0 && value.Y >= 0 ) {

                    ApplicationSettings.AddOrUpdateValue(WindowLocationKey, value.ToString()); 

                }

            }

        }



        /// <summary>

        /// Gets or sets the size of the window.

        /// </summary>

        /// <value>The size of the window.</value>

        public static Core.Types.Vector2<int> WindowSize

        {

            get

            {

                var setting = new Core.Types.Vector2<int>();

                setting.FromString(ApplicationSettings.GetValueOrDefault(WindowSizeKey, WindowSizeMinimumX + "," + WindowSizeMinimumY));

                return setting;

            }

            set

            {

                // Make sure we are never saving smaller then we should (dont know how it would happen)

                if (value.X < WindowSizeMinimumX) value.X = WindowSizeMinimumX;

                if (value.Y < WindowSizeMinimumY) value.Y = WindowSizeMinimumY;

                ApplicationSettings.AddOrUpdateValue(WindowSizeKey, value.ToString());

            }

        }



        /// <summary>

        /// Get the current applications settings.

        /// </summary>

        /// <value>The applications settings.</value>

        internal static ISettings ApplicationSettings

        {

            get

            {

                return CrossSettings.Current;

            }

        }



        #endregion



        #region Methods



        /// <summary>

        /// Remove all preferences to their default values (by removing the entries).

        /// </summary>

        public static void RemovalAll()

        {

            RemoveGeneralItems();

            RemoveConfigItems();

            RemoveUpdateItems();

        }



        /// <summary>

        /// Removes all process (defaults) preferences.

        /// </summary>

        public static void RemoveConfigItems()

        {

            ApplicationSettings.Remove(ProcessDefaultTargetFolderKey);

            ApplicationSettings.Remove(ProcessDefaultConfigKey);

        }



        /// <summary>

        /// Remove all general screen preferences (this includes the window sizing)

        /// </summary>

        public static void RemoveGeneralItems()

        {



            ApplicationSettings.Remove(GeneralAnonymousUsageStatsKey);

            ApplicationSettings.Remove(GeneralOpenReportsAutomaticallyKey);

            ApplicationSettings.Remove(GeneralLocalizationKey);



            RemoveWindowItems();

        }



        /// <summary>

        /// Remove all update checking related preferences.

        /// </summary>

        public static void RemoveUpdateItems()

        {

            ApplicationSettings.Remove(UpdatesCheckForUpdatesKey);

            ApplicationSettings.Remove(UpdatesIgnoreVersionKey);

            ApplicationSettings.Remove(UpdatesChannelKey);

            ApplicationSettings.Remove(UpdatesFrequencyKey);

            ApplicationSettings.Remove(UpdatesLastCheckKey);

        }



        /// <summary>

        /// Remove specifically all window related preferences.

        /// </summary>

        public static void RemoveWindowItems()

        {

            ApplicationSettings.Remove(WindowSizeKey);

            ApplicationSettings.Remove(WindowLocationKey);

        }



        #endregion

    }

}