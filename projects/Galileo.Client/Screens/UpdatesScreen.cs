using System;
using Galileo.Core;
using I18NPortable;

namespace Galileo.Client.Screens
{
    /// <summary>
    /// Galileo's Update Screen Logic
    /// </summary>
    public static class UpdatesScreen
    {
        /// <summary>
        /// Clear any saved ignore version so that updates will fire
        /// </summary>
        /// <remarks>This relies on the client handling the update call</remarks>
        public static void CheckForUpdatesButton_Click()
        {
            Settings.IgnoreVersion = string.Empty;
        }

        /// <summary>
        /// Open the provided download link in the native browser
        /// </summary>
        /// <param name="url">The latest download url</param>
        public static void DownloadButton_Click(string url)
        {
            Platform.Run("open", url, true);
        }

        /// <summary>
        /// Get the appropriate next check text string
        /// </summary>
        /// <returns>The next check text</returns>
        public static string GetNextCheckText()
        {
            var hours = Update.UpdateProvider.HoursTillUpdate(out var lastDate);
            
            if (hours <= 0)
            {
                return "Checking ...";
            }
            
            if ( hours == 1 )
            {
                return "Client.NoUpdate.LastCheck".Translate(lastDate, "Client.NoUpdate.LastCheck.Hours".Translate("1", string.Empty));
            }
            
            return hours > 1 ? "Client.NoUpdate.LastCheck".Translate(lastDate, "Client.NoUpdate.LastCheck.Hours".Translate(hours, "s")) : string.Empty;
        }       

        /// <summary>
        /// Tell Galileo to ignore the current updates version
        /// </summary>
        /// <param name="version">The version to ignore</param>
        public static void IgnoreButton_Click(string version)
        {
            Settings.IgnoreVersion = version;
        }
    }
}
