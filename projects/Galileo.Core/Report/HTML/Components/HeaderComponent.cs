using System.Text;

namespace Galileo.Core.Report.HTML.Components
{
    class HeaderComponent : BaseHTML
    {
        internal HeaderComponent(bool embed, string title = "Galileo Report")
        {
            const string _bootstrapSource = "https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.3/css/bootstrap.min.css";
            const string _bootstrapIntegrity = "sha384-Zug+QiDoJOrZ5t4lssLdxGhVrurbmBWopoEl+M6BdEfwnCJZtKxi1KgxUyJq13dy";

            StringBuilder header = new StringBuilder();
            header.Append("<!doctype html>" +
                "<html lang=\"en\">" +
                    "<head>" +
                        "<meta charset=\"utf-8\">" +
                          "<meta name=\"viewport\" content=\"initial-scale=1, shrink-to-fit=no\">");

            if ( embed )
            {
                header.Append("<style>" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.bootstrap.css") + "</style>");
            }
            else
            {
                header.Append("<link rel=\"stylesheet\" href=\"" + _bootstrapSource + "\" integrity=\"" + _bootstrapIntegrity + "\" crossorigin=\"anonymous\">");
            }

            header.Append("<link href=\"" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.logo.base64") + "\" rel=\"icon\" type=\"image/svg+xml\">" + 
                        "<title>" + title + "</title>" +
                    "</head>" + 
                          "<body>");

            _cache = header.ToString();
        }
    }
}
