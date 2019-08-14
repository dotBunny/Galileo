using System;

using System.IO;

using Galileo.Client.Update;

using Galileo.Core;

using I18NPortable;

using Newtonsoft.Json;



namespace Galileo.Client.Screens

{

    /// <summary>

    /// Galileo's Preferences Screen Logic

    /// </summary>

    public static class PreferencesScreen

    {

        /// <summary>

        /// Export the default process config settings to file

        /// </summary>

        /// <returns>The result message</returns>

        /// <param name="exportPath">The path of the export file</param>

        public static string DefaultsExportButton_Click(string exportPath)

        {

            try

            {

				Settings.DefaultConfig.WriteConfig(exportPath);            

            }

            catch (Exception e)

            {

                return "Client.Preferences.Defaults.Export.Exception".Translate(e.Message);

            }

            return "Client.Preferences.Defaults.Export.Success".Translate();

        }



        /// <summary>

        /// Import default settings for the process config

        /// </summary>

        /// <returns>The result message</returns>

        /// <param name="importPath">The path to the file to import</param>

        public static string DefaultsImportButton_Click(string importPath)

        {

            try

            {

                Settings.DefaultConfig = JsonConvert.DeserializeObject<HunterConfig>(File.ReadAllText(importPath));



            }

            catch (Exception e)

            {

                return "Client.Preferences.Defaults.Import.Exception".Translate(e.Message);

            }

            return "Client.Preferences.Defaults.Import.Success".Translate();

        }



        /// <summary>

        /// Restore Defaults preferences

        /// </summary>

        public static void DefaultsRestoreDefaultsButton_Click()

        {

            Settings.RemoveConfigItems();

        }



        /// <summary>

        /// Set the default target folder

        /// </summary>

        /// <param name="path">The target folder path</param>

        public static void PreferencesDefaultsTargetFolderPath_Click(string path)

        {

            Settings.DefaultFolder = path;

        }



        /// <summary>

        /// Indicate if reports should be opened automatically when finished processing

        /// </summary>

        /// <param name="status">If they should be opened</param>

        public static void GeneralReportAutomaticOpenCheckButton_Click(bool status)

        {

            Settings.OpenReportsAutomatically = status;

        }



        /// <summary>

        /// Restore General preferences

        /// </summary>

        public static void GeneralRestoreDefaultsButton_Click(IClient client)

        {

            Settings.RemoveGeneralItems();



            // Update provider

            Localization.LocalizationProvider.SetLocale(Settings.Localization);



            // Localize

            client.Localize();

        }



        /// <summary>

        /// Restore Update preferences

        /// </summary>

        public static void UpdatesRestoreDefaultsButton_Click()

        {

            Settings.RemoveUpdateItems();

        }



        /// <summary>

        /// Indicate if usage data should be transmitted when processing

        /// </summary>

        /// <param name="status">If they should be sent</param>

        public static void GeneralSendUsageDataButton_Click(bool status)

        {

            Settings.AnonymousUsageStats = status;

        }



        /// <summary>

        /// Opens the preferences overview section of the knowledge base in the native web browser

        /// </summary>

        public static void HelpButton_Click()

        {

            Platform.Run("open", Links.Website + "/kb/preference/overview/", true);

        }



        /// <summary>

        /// Set flag indicating if updates should be checked periodically.

        /// </summary>

        /// <param name="status">Should updates be checked for?</param>

        public static void SetCheckForUpdates(bool status)

        {

            Settings.ShouldCheckForUpdates = status;

        }



        /// <summary>

        /// Set the localization for the application

        /// </summary>

        /// <param name="index">Locale Index</param>

        /// <param name="client">Client Reference</param>

        public static void SetLocale(int index, IClient client)

        {

            if (index > Localization.LocalizationProvider.SupportedLocales.Length || index < 0) return;

            var code = Localization.LocalizationProvider.SupportedLocales[index];



            if (Settings.Localization == code) return;

            

            Settings.Localization = code;

            Localization.LocalizationProvider.SetLocale(code);



            client?.Localize();

        }



        /// <summary>

        /// Set the <see cref="UpdateProvider.Channel" /> which updates should be assessed.

        /// </summary>

        /// <param name="channel">The update channel</param>

        public static void SetUpdateChannel(UpdateProvider.Channel channel)

        {

            Settings.UpdatesChannel = channel;

        }



        /// <summary>

        /// Set the <see cref="UpdateProvider.Frequency" /> at which updates should be checked

        /// </summary>

        /// <param name="frequency">The update frequency</param>

        public static void SetUpdateFrequency(UpdateProvider.Frequency frequency)

        {

            Settings.UpdateCheckFrequency = frequency;

        }





    }

}

