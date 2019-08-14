namespace Galileo.Core.Report.HTML.Components
{
    class Navbar : BaseHTML
    {        
        internal Navbar(string version = "")
        {
            _cache =
                "<nav id=\"navbar\" class=\"navbar navbar-expand-sm navbar-light bg-light sticky-top\">" +
                    "<a class=\"navbar-brand\" href=\"#\">" +
                        "<img src=\"" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.logo.base64") + "\" width=\"30\" height=\"30\" class=\"d-inline-block align-top\" alt=\"Galileo\">" +
                        "Galileo" +
                    "</a>" +
                    "<button class=\"navbar-toggler\" type=\"button\" data-toggle=\"collapse\" data-target=\"#navbarSupportedContent\" aria-controls=\"navbarSupportedContent\" aria-expanded=\"false\" aria-label=\"Toggle navigation\">" +
                        "<span class=\"navbar-toggler-icon\"></span>" +
                    "</button>" +
                    "<div class=\"collapse navbar-collapse\" id=\"navbarSupportedContent\">" +
                        "<ul class=\"navbar-nav mr-auto\">" +
                            "<li class=\"nav-item\">" +
                                "<a class=\"nav-link\" href=\"#overview\">" + Localization.OverviewLocalization.Header + "</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a class=\"nav-link\" href=\"#submissions\">" + Localization.SubmissionsLocalizations.Header + "</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a class=\"nav-link\" href=\"#settings\">" + Localization.SettingsLocalization.Header + "</a>" +
                            "</li>" +
                        "</ul>" +
                        "<span class=\"d-none d-sm-block font-weight-light text-muted\"><small>" + version + "</small></span>" +
                    "</div>" +
                "</nav>";
        }
    }
}