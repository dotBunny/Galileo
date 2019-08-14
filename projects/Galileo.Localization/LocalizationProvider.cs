using System.Reflection;
using I18NPortable;
using System.Linq;
using System;

namespace Galileo.Localization
{
    /// <summary>
    /// Localization Provider
    /// </summary>
    public static class LocalizationProvider
    {
#if __PACKAGE__
		/// <summary>
		/// An array of supported locales
		/// </summary>
		public static string[] SupportedLocales = { "en-CA", "fr-CA" };

        /// <summary>
        /// An array of more friendly locales names
        /// </summary>
        public static string[] SupportedLocalesDescription = { "English", "French"};
#else
		/// <summary>
        /// An array of supported locales
        /// </summary>
		public static string[] SupportedLocales = { "en-CA", "fr-CA", "keys" };

        /// <summary>
        /// An array of more friendly locales names
        /// </summary>
		public static string[] SupportedLocalesDescription = { "English", "French", "Translation Keys" };

#endif
		/// <summary>
		/// Initialize
		/// </summary>
		public static void Init()
        {
#if DEBUG
            I18N.Current
                .AddLocaleReader(new LocalizationReader(), ".stub") // Tells our system to use the txt stubs
                .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                .SetFallbackLocale("en-CA") // Optional but recommended: locale to load in case the system locale is not supported
                .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                .Init(Assembly.GetExecutingAssembly());

#else
            I18N.Current
                .AddLocaleReader(new LocalizationReader(), ".stub") // Tells our system to use the txt stubs
                .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                .SetFallbackLocale("en-CA") // Optional but recommended: locale to load in case the system locale is not supported
                .SetThrowWhenKeyNotFound(false) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                .Init(Assembly.GetExecutingAssembly());
#endif
			LocalizationCache.Update();
        }

        /// <summary>
        /// Set the localization internally (does not update items)
        /// </summary>
        /// <param name="code">Valid locale</param>
        public static void SetLocale(string code)
        {
            if (SupportedLocales.Contains(code)) 
            {
                I18N.Current.Locale = code;
				LocalizationCache.Update();
            }
                     
            if (SupportedLocalesDescription.Contains(code) )
			{
				I18N.Current.Locale = SupportedLocales[Array.FindIndex(SupportedLocalesDescription,(obj) => obj == code)];
				LocalizationCache.Update();
			}

			//I18N.Current.Locale = SupportedLocales[0];
        }

        /// <summary>
        /// Get the currently set locale
        /// </summary>
        /// <returns>The locale string</returns>
        public static string GetCulture()
        {
            return I18N.Current.Locale;
        }

        /// <summary>
        /// Gets the index of the supported locale
        /// </summary>
        /// <returns>The locale index</returns>
        /// <param name="code">The locale code</param>
        public static int GetLocaleIndex(string code)
        {
            for (int i = 0; i < SupportedLocales.Length; i++)
            {
                if ( code == SupportedLocales[i] )
                {
                    return i;
                }
            }
			for (int i = 0; i < SupportedLocalesDescription.Length; i++)
            {
				if (code == SupportedLocalesDescription[i])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
