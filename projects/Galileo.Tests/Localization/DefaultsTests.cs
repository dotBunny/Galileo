using Xunit;

namespace Galileo.Tests.Localization
{
    public class DefaultsTests
    {
        [Fact]
        public void FallbackLocale()
        {
            // Initialize Localization
            Galileo.Localization.LocalizationProvider.Init();

            // Ensure default locale/culture as Canadian English
            Assert.Equal("en-CA", I18NPortable.I18N.Current.Locale);
        }
    }
}