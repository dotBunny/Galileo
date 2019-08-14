using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.Report.HTML
{
    static class Content
    {
        public static string GetColorCodeByWeight(float weight)
        {
            if (weight >= 0.85f)
            {
                return "-danger";
            }
            if (weight >= 0.65f)
            {
                return "-warning";
            }
            return "-secondary";
        }

        public static string GetKilobyteString(long value)
        {
            return Math.Ceiling(value * 0.001) + " kb";
        }

        public static string GetSubmissionListTable(Dictionary<Guid, Submissions.Submission> submissions, string extraClasses = "")
        {
            StringBuilder layout = new StringBuilder();
            layout.Append("<div class=\"table-responsive\"><table class=\"table table-striped");
            if (extraClasses != "" && extraClasses != string.Empty)
            {
                layout.Append(" " + extraClasses);
            }
            layout.Append("\">" +
                            "<thead>" +
                                "<tr>" +
                                    "<th scope=\"col\">" + Localization.SubmissionsLocalizations.FileName + "</th>" +
                                    "<th class=\"d-none d-md-table-cell\" scope=\"col\">" + Localization.SubmissionsLocalizations.DetectedName + "</th>" +
                                    "<th class=\"d-none d-md-table-cell\" scope=\"col\">" + Localization.SubmissionsLocalizations.FileSize + "</th>" +
                                    "<th class=\"d-none d-lg-table-cell\" scope=\"col\">" + Localization.SubmissionsLocalizations.FileDate + "</th>" +
                                    "<th class=\"d-none d-lg-table-cell\" scope=\"col\">" + Localization.SubmissionsLocalizations.Link + "</th>" +
                                "</tr>" +
                            "</thead>" +
                            "<tbody class=\"table-hover\">");


            foreach (KeyValuePair<Guid, Submissions.Submission> s in submissions)
            {
                layout.Append("<tr>" +
                                "<td><a href=\"#" + s.Key.ToString() + "\">" + s.Value.FileNameWithExtension + "</a></td>" +
                                "<td class=\"d-none d-md-table-cell\">" + GetDetectedName(s.Value) + "</td>" +
                                "<td class=\"d-none d-md-table-cell\">" + Content.GetKilobyteString(s.Value.FileSize) + "</td>" +
                                "<td class=\"d-none d-lg-table-cell\">" + s.Value.FileDate.ToString() + "</td>" +
                                "<td class=\"d-none d-lg-table-cell\"><a class=\"btn btn-secondary btn-sm\" href=\"" + s.Value.AbsolutePath + "\" role=\"button\">" + Localization.SharedLocalization.Open + "</a></td>" +
                              "</tr>");
            }

            layout.Append("</tbody></table></div>");
            return layout.ToString();
        }

        public static string GetTwoCellTableRow(string item, string itemValue)
        {
            return "<tr><td>" + item + "</td><td>" + itemValue + "</td></tr>";
        }

        public static string GetDetectedName(Submissions.Submission submission)
        {
            string fullName = string.Empty;


            if (submission.LastName != string.Empty)
            {
                fullName = submission.LastName + ", ";
            }
            else
            {
                fullName = "<em>Unknown</em>, ";
            }

            if (submission.FirstName != string.Empty)
            {
                fullName += submission.FirstName;
            }
            else
            {
                fullName += "<em>Unknown</em>";
            }
            if (fullName == "<em>Unknown</em>, <em>Unknown</em>")
            {
                fullName = "<em>Unknown</em>";
            }
            return fullName;
        }

    }
}
