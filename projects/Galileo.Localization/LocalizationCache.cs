using System.Collections.Generic;

using I18NPortable;

namespace Galileo.Localization
{
    /// <summary>
    /// Localization Cache
    /// </summary>
    public static class LocalizationCache
	{
		#region Fields

		/// <summary>
		/// Long Date Format
		/// </summary>
		public static string DateLongFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// Safe Long Date Format
        /// </summary>
		/// <remarks>
		/// Useful for using in file names.
		/// </remarks>
		public static string DateLongSafeFormat = "yyyyMMdd-HHmmss";

        /// <summary>
        /// Short Date Format
        /// </summary>
		public static string DateShortFormat = "yyyy-MM-dd";

        /// <summary>
		/// Safe Short Date Format
        /// </summary>
        /// <remarks>
		/// Useful for using in file names.
		/// </remarks>
		public static string DateShortSafeFormat = "yyyyMMdd";

		/// <summary>
        /// Cached Default Value String
        /// </summary>
        public static string KeywordDefaultValue = "Default Value";

        /// <summary>
        /// Cached Description Keyword
        /// </summary>
        public static string KeywordDescription = "Description";

        /// <summary>
        /// Cached Setting Keyword
        /// </summary>
        public static string KeywordSetting = "Setting";

        /// <summary>
        /// Cached Value Keyword
        /// </summary>
        public static string KeywordValue = "Value";      
      
		/// <summary>
        /// Possible line ending characters
        /// </summary>
        public static char[] LineEndings = { '\n', '\r' };
        
		/// <summary>
        /// Translated Cache
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> Locales = new Dictionary<string, Dictionary<string, string>>();

        #endregion

		/// <summary>
		/// Update static cached values.      
		/// </summary>
		/// <remarks>
		/// This does not update the locales data.
		/// </remarks>
		public static void Update()
		{
			DateLongFormat = "DateLong".Translate();
			DateLongSafeFormat = "DateLongSafe".Translate();
			DateShortFormat = "DateShort".Translate();
			DateShortSafeFormat = "DateShortSafe".Translate();
                     
			KeywordSetting = "Client.Process.ConfigTable.Setting".Translate();
			KeywordDescription = "Client.Process.ConfigTable.Description".Translate();
			KeywordValue = "Client.Process.ConfigTable.Value".Translate();
			KeywordDefaultValue = "Client.Preferences.Defaults.ConfigTable.DefaultValue".Translate();
		}
    }
}
