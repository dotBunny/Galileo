namespace Galileo.Core.Report.HTML.Components
{
    class CustomStylesheetComponent : BaseHTML
    {
        internal CustomStylesheetComponent()
        {
            _cache =
                "<style>" +
                    ".branding {" +
                        "background-repeat: no-repeat;" +
                        "background-image: url('" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.logo.base64") + "');" +
                        "background-size: 150%;" + 
                        "opacity: 0.1;" +
                    "} " +
                    ".hand { " +
                        "cursor: pointer;" +
                    "} " +
                    "a { " +
                        "color: #ff9600;" +
                    "} " +
                    "a:hover { " +
                        "color: #c67808;" +
                    "} " +
                    ".orange { " +
                        "color: #ff9600;" +
                    "} " +
                    ".checkmark {" +
                        "background-repeat: no-repeat;" +
                        "background-image: url('" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.check.base64") + "');" +
                        "background-position: center right;" +
                        "background-size: contain;" +
                        "background-origin: content-box;" +
                    "} " + 
                    ".cross {" +
                        "background-repeat: no-repeat;" +
                        "background-image: url('" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.cross.base64") + "');" +
                        "background-position: center right;" +
                        "background-size: contain;" +
                        "background-origin: content-box;" +
                    "} " + 
                "</style>";
        }
    }
}