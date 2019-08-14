using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.Checks;
using Galileo.Core.Localization;

namespace Galileo.Core.Report.HTML.Sections
{
    class SubmissionsSection : BaseHTML
    {
        HunterSession _session;
        Dictionary<Guid, Submissions.Submission> _submissions;
        internal SubmissionsSection(HunterSession session, Dictionary<Guid, Submissions.Submission> sortedSubmissions)
        {
            _session = session;
            _submissions = sortedSubmissions;

            StringBuilder layout = new StringBuilder();

            layout.Append(CreateHeader());

            foreach(KeyValuePair<Guid, Submissions.Submission> s in sortedSubmissions)
            {
                if (s.Value.Processed)
                {
                    layout.Append(CreateSubmission(s.Value));
                }
            }
            layout.Append(CreateFooter());

            _cache = layout.ToString();
        }

        string CreateHeader()
        {
            return "<div id=\"submissions\" class=\"container\" style=\"padding-bottom: 30px;\">" +
                        "<h1>" + SubmissionsLocalizations.Header + "</h1>" +
                        "<p>" + SubmissionsLocalizations.Description + "</p>" +
                        "<div class=\"container\">";
        }

        string CreateFooter()
        {
            return "</div></div>";
        }

        string CreateSubmission(Submissions.Submission submission)
        {
            StringBuilder card = new StringBuilder();

            card.Append("<div class=\"card\" id=\"" + submission.GUID.ToString() + "\" style=\"margin-bottom: 30px;\">");

            if (submission.Flagged )
            {
                // -- Robin
                // WAS:
                //// card.Append("<div class=\"card-header text-muted cross\"><strong>" + submission.RelativePathNoFile + "</strong></div>");    
                card.Append("<div class=\"card-header text-muted cross\"><strong>" + submission.ContainerPath + "</strong></div>");    
            }
            else
            {
                // -- Robin
                // WAS:
                //// card.Append("<div class=\"card-header text-muted checkmark\"><strong>" + submission.RelativePathNoFile + "</strong></div>");
                card.Append("<div class=\"card-header text-muted checkmark\"><strong>" + submission.ContainerPath + "</strong></div>");
            }


            card.Append(CreateSubmissionInfoTable(submission));
            card.Append(CreateSubmissionChecks(submission));

            card.Append("</div>");
            return card.ToString();
        }

        string CreateSubmissionChecks(Submissions.Submission submission)
        {
            StringBuilder header = new StringBuilder();
            StringBuilder content = new StringBuilder();




            int count = 0;
            foreach(ICheck check in submission.Checks)
            {
                if ( check.Flagged()) 
                {
                    // Counter
                    count++;

                    // Button
                    header.Append("<a class=\"btn btn" + Content.GetColorCodeByWeight(check.GetWeight()) + " m-2\" data-toggle=\"collapse\" href=\"#check-" +  submission.GUID.ToString() + "-" + check.GetID() + "\" role=\"button\" aria-expanded=\"false\" data-parent=\"#" + submission.GUID.ToString() + "\">" + check.GetName() + " <span class=\"badge badge-light\">" + check.GetFlag().Similar.Count + "</span></a>");

                    // Content
                    content.Append("<div class=\"accordion-group\">");
                    content.Append("<div class=\"collapse\" id=\"check-" + submission.GUID.ToString() + "-" + check.GetID() + "\">");


                    content.Append("<div class=\"card-body\"><p class=\"text-muted\"><em>" + check.GetDescription() + " [<a href=\"" + check.GetKBLink() + "\" title=\"Knowledge Base Article\" target=\"_blank\">More</a>]</em></p>" + 
                                   "<div class=\"card-group\" style=\"padding-bottom: 15px;\">");

                    // Left Side Origin
                    content.Append(
                        "<div class=\"card\" style=\"background-color:rgba(0,0,0,.01);\">" +
                            "<div class=\"card-body\">" +
                                "<h5 class=\"card-title\">" + check.GetLocalReference() + "</h5>" +
                                //"<h6 class=\"card-subtitle mb-2 text-muted\">" + submission.RelativePathNoFile + "</h6>" +
                                //"<p class=\"card-text\"><small class=\"text-muted\">" +  + "</small></p>" +
                            "</div>" +
                        "</div>");

                    // Right Side Matches
                    content.Append("<div class=\"card text-right\"><div class=\"card-body\">");
                    foreach (KeyValuePair<Submissions.Submission, FlagItem> entry in check.GetFlag().Similar)
                    {
                        // -- Robin
                        // WAS:
                        /*
                        content.Append(
                            "<h5 class=\"card-title\"><a href=\"#" + entry.Key.GUID.ToString() + "\">" + entry.Key.FileNameWithExtension + "</a></h5>" +
                            "<h6 class=\"card-subtitle mb-2 text-muted\">" + entry.Key.RelativePathNoFile + "</h6>");
                        */
                        content.Append(
                            "<h5 class=\"card-title\"><a href=\"#" + entry.Key.GUID.ToString() + "\">" + entry.Key.FileNameWithExtension + "</a></h5>" +
                            "<h6 class=\"card-subtitle mb-2 text-muted\">" + entry.Key.ContainerPath + "</h6>");

                        content.Append("<p class=\"card-text\"><small class=\"text-muted\">");
                        if (entry.Value.Reference != "" && entry.Value.Reference != string.Empty)
                        {
                            content.Append(entry.Value.Reference);
                        }
                        content.Append(" [" + Compare.ToPercentage((float)entry.Value.Similarity) + "]");
                        content.Append("</small></p>");
                    }

                    // Close it all offer (2+3+1)
                    content.Append("</div></div></div></div></div></div>");
                }
            }

            // No checks triggered content
            if ( count == 0 )
            {
                return string.Empty;
            }
            else
            {
                header.Insert(0, "<div class=\"card-header card-footer\">");
                header.Append("</div>");

                return header.ToString() + content.ToString();
            }
        }

        string CreateSubmissionInfoTable(Submissions.Submission submission)
        {
            StringBuilder info = new StringBuilder();

            info.Append("<div class=\"card-body\"><h4 class=\"card-title orange\">" + submission.FileNameWithExtension + " <a class=\"btn btn-secondary btn-sm float-right d-none d-md-block\" href=\"" + submission.AbsolutePath + "\" role=\"button\">" + SharedLocalization.Open + "</a></h4>" +
                        "<div class=\"row\">" +
                            "<div class=\"col-lg-6\" style=\"padding-top:20px;\">" +
                                "<h4 class=\"card-title\">" + SubmissionsLocalizations.SubmissionInfo + "</h4>" +
                                "<table class=\"table table-bordered table-striped\" style=\"background-color: #fff\">" +
                                    "<thead>" +
                                        "<tr>" +
                                            "<th>" + SharedLocalization.Item + "</th><th>" + SharedLocalization.Value + "</th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>");
            // Submission Info
            info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.FileSize, Content.GetKilobyteString(submission.FileSize)));
            info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.CreationDate, submission.FileDate.ToString()));
            info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.DetectedName, Content.GetDetectedName(submission)));
            info.Append(Content.GetTwoCellTableRow(SharedLocalization.Hash, "<small>" + submission.ContentHash + "</small>"));
            info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.ContentLength, submission.ContentLength.ToString()));

            info.Append("</tbody>" +
                                "</table>" +
                            "</div>" +
                            "<div class=\"col-lg-6\" style=\"padding-top:20px;\">" +
                                "<h4 class=\"card-title\">" + SubmissionsLocalizations.MetaInformation + "</h4>" +
                                "<table class=\"table table-bordered table-striped\" style=\"background-color: #fff\">" +
                                    "<thead>" +
                                        "<tr><th>" + SharedLocalization.Item + "</th><th>" + SharedLocalization.Value + "</th></tr>" +
                                    "</thead>" +
                                    "<tbody>");

            // Meta Information
            if (submission.MetaCreator != string.Empty)
            {
                info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.MetaCreator, submission.MetaCreator));
            }
            if (submission.MetaLastModifiedBy != string.Empty)
            {
                info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.LastModifiedBy, submission.MetaLastModifiedBy));
            }
            if (submission.MetaDateCreated != DateTime.MinValue)
            {
                info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.MetaCreationDate, submission.MetaDateCreated.ToString()));
            }

            if (submission.MetaDateModified != DateTime.MinValue)
            {
                info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.MetaLastModified, submission.MetaDateModified.ToString()));
            }
            if (submission.MetaDateLastPrinted != DateTime.MinValue)
            {
                info.Append(Content.GetTwoCellTableRow(SubmissionsLocalizations.MetaPrinted, submission.MetaDateLastPrinted.ToString()));
            }

            info.Append("</tbody></table></div></div></div>");

            return info.ToString();
        }

    }
}


