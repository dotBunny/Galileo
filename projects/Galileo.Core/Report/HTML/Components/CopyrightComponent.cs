namespace Galileo.Core.Report.HTML.Components
{
    class CopyrightComponent : BaseHTML
    {
        public CopyrightComponent()
        {
            _cache =
                "<div class=\"mx-auto text-muted\" style=\"width: 30px; padding-bottom: 30px; padding-top: 20px;\">" +
                "<a href=\"http://dotbunny.com/\"><img style=\"max-width: 30px; max-height: 30px;\" src=\"" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.dotbunny.base64") + "\" alt=\"dotBunny\" /></a>" +
                "</div>";
        }
    }
}
