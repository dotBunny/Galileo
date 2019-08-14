using System.Text;

namespace Galileo.Core.Report.HTML.Components
{
    class CustomJavascriptComponent : BaseHTML
    {
        const string jQuerySource = "https://code.jquery.com/jquery-3.2.1.slim.min.js";
        const string jQueryIntegrity = "sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN";

        const string bootstrapSource = "https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.3/js/bootstrap.min.js";
        const string bootstrapIntegrity = "sha384-a5N7Y/aK3qNeh15eJKGWxsqtnX/wWdSZSKp+81YjTmS15nvnvxKHuzaWwXHDli+4";

        public CustomJavascriptComponent(bool embed)
        {
            StringBuilder script = new StringBuilder();


            if (embed)
            {
                script.Append("<script type=\"application/javascript\">" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.jquery.js") + "</script>" +
                "<script type=\"application/javascript\">" + Platform.GetEmbeddedResourceFile("Galileo.Core.Report.HTML.External.bootstrap.js") + "</script>");
            }
            else
            {
                script.Append("<script src=\"" + jQuerySource + "\" integrity=\"" + jQueryIntegrity + "\" crossorigin=\"anonymous\"></script>");
                script.Append("<script src=\"" + bootstrapSource + "\" integrity=\"" + bootstrapIntegrity + "\" crossorigin=\"anonymous\"></script>");
            }

                script.Append("<script type=\"application/javascript\">" +
                    "$('.navbar li a').click(function(event) {" +
                        "event.preventDefault();" + 
                        "$($(this).attr('href'))[0].scrollIntoView();" +
                        "scrollBy(0, -55);" +
                    "});" + 
                    "$('#submission-summary-total-button').click(function() { $('#submission-summary-total').collapse('toggle'); $('#submission-summary-processed').collapse('hide'); $('#submission-summary-flagged').collapse('hide'); $('#submission-summary-invalid').collapse('hide');});" +
                    "$('#submission-summary-processed-button').click(function() { $('#submission-summary-total').collapse('hide'); $('#submission-summary-processed').collapse('toggle'); $('#submission-summary-flagged').collapse('hide'); $('#submission-summary-invalid').collapse('hide');});" +
                    "$('#submission-summary-flagged-button').click(function() { $('#submission-summary-total').collapse('hide'); $('#submission-summary-processed').collapse('hide'); $('#submission-summary-flagged').collapse('toggle'); $('#submission-summary-invalid').collapse('hide'); });" +
                    "$('#submission-summary-invalid-button').click(function() { $('#submission-summary-total').collapse('hide'); $('#submission-summary-processed').collapse('hide'); $('#submission-summary-flagged').collapse('hide'); $('#submission-summary-invalid').collapse('toggle'); });" +
                    "</script>");
            
            _cache = script.ToString();
        }
    }
}