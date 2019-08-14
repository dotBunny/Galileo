using System;
namespace Galileo.Core.Localization
{
    public static class SettingsLocalization
    {
        public static string ContentSettingsTitle
        {
            get
            {
                return "Content Settings";
            }
        }
        public static string Header
        {
            get
            {
                return "Settings";
            }
        }
        public static string Description
        {
            get
            {
                return "The settings that were used during the processing of this submission. You can alter these prior to running the process, via the preferences window, or editing the <em>galileo.json</em> file in the target folder.";
            }

        }
        public static string ProcessSettingsTitle
        {
            get
            {
                return "Process Settings";
            }
        }
        public static string SharedSettingsTitle
        {
            get
            {
                return "Shared Settings";
            }
        }
        public static string FileSettingsTitle
        {
            get
            {
                return "File Settings";
            }
        }
        public static string MetaDataSettingsTitle
        {
            get
            {
                return "Meta Data Settings";
            }
        }
    }
}
