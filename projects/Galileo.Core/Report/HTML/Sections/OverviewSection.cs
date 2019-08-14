using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.Checks;
using Galileo.Core.Localization;

namespace Galileo.Core.Report.HTML.Sections
{
    class OverviewSection : BaseHTML
    {
        HunterSession _session;
        Dictionary<Guid, Submissions.Submission> _submissions;
        internal OverviewSection(HunterSession session, Dictionary<Guid, Submissions.Submission> sortedSubmissions)
        {
            _session = session;
            _submissions = sortedSubmissions;

            StringBuilder layout = new StringBuilder();

            layout.Append(CreateHeader());

            layout.Append(CreateSubmissionSummary());
            layout.Append(CreateCheckSummary());
            layout.Append(CreateDisabledChecksSummary());

            layout.Append(CreateFooter());

            _cache = layout.ToString();
        }

        string CreateHeader()
        {
            return "<div id=\"overview\" class=\"container\" style=\"padding-bottom: 30px;\">" +
                        "<h1>" + OverviewLocalization.Header + "</h1>" +
                        "<p><em>Galileo " + OverviewLocalization.Description + " " + _session.WorkingDirectory + " " + SharedLocalization.On.ToLower() + " " + _session.Ran.ToString() + ".</em></p>" +
                        "<div class=\"container\">";
        }
        string CreateFooter()
        {
            return "</div></div>";
        }

        string CreateDisabledCheckItem(string title, string description)
        {
            return "<li class=\"list-group-item\"><strong>" + title + "</strong><br /><small class=\"text-secondary\">" + description + "</small></li>";
        }
        string CreateDisabledChecksSummary()
        {
            StringBuilder layout = new StringBuilder();
            StringBuilder list = new StringBuilder();
            layout.Append(
                "<div id=\"overview-disabled-checks\" class=\"container\">" +
                "<h2>" + OverviewLocalization.DisabledCheckSummaryTitle + "</h2>");
            

            // Review Each Check
            if ( !_session.Config.CheckContent )
            {
                list.Append(CreateDisabledCheckItem(ChecksLocalization.ContentCheckName, ChecksLocalization.ContentCheckDescription));
            }
            if (!_session.Config.CheckFileName)
            {
                list.Append(CreateDisabledCheckItem(ChecksLocalization.FileNameName, ChecksLocalization.FileNameCheckDescription));
            }
            if ( !_session.Config.CheckMetaCreator)
            {
                list.Append(CreateDisabledCheckItem(ChecksLocalization.MetaCreatorName, ChecksLocalization.MetaCreatorDescription));
            }
            if (!_session.Config.CheckMetaDateCreated)
            {
                list.Append(CreateDisabledCheckItem(ChecksLocalization.MetaDateCreatedName, ChecksLocalization.MetaDateCreatedDescription));
            }
            if (!_session.Config.CheckMetaDateLastPrinted)
            {
                list.Append(CreateDisabledCheckItem(ChecksLocalization.MetaDateLastPrintedName, ChecksLocalization.MetaDateLastPrintedDescription));
            }
            if (!_session.Config.CheckMetaDateModified)
            {
                list.Append(CreateDisabledCheckItem(ChecksLocalization.MetaDateModifiedName, ChecksLocalization.MetaDateModifiedDescription));
            }
            if (!_session.Config.CheckMetaLastModifiedBy)
            {
                list.Append(CreateDisabledCheckItem(ChecksLocalization.MetaLastModifiedByName, ChecksLocalization.MetaLastModifiedByDescription));
            }


            if ( list.Length == 0 ) 
            {
                layout.Append("<p><em>" + OverviewLocalization.DisabledChecksNone + "</em></p>");
            }
            else 
            {
                layout.Append("<ul class=\"list-group\">" + list + "</ul>");
            }
            layout.Append("</div>");
            return layout.ToString();
        }

        string CreateCheckSummary()
        {
            StringBuilder layout = new StringBuilder();

            layout.Append("<div id=\"overview-checks\" class=\"container\" style=\"padding-bottom: 30px;\">" +
                                "<h2>" + OverviewLocalization.CheckSummaryTitle + "</h2>" +
                                "<p>" + OverviewLocalization.CheckSummaryDescription + "</p>");

            // Parse all submissions into check related categories
            Dictionary<CheckFactory.CheckType, List<Submissions.Submission>> ChecksReference = new Dictionary<CheckFactory.CheckType, List<Submissions.Submission>>();


            foreach(KeyValuePair<Guid, Submissions.Submission> s in _submissions)
            {
                // Itterate over a submissions checks
                foreach(ICheck c in s.Value.Checks)
                {
                    // Only process check if its tripped
                    if (!c.Flagged()) continue;

                    // Make sure our list is there
                    if (!ChecksReference.ContainsKey(c.GetType()) )
                    {
                        ChecksReference.Add(c.GetType(), new List<Submissions.Submission>());
                    }

                    // Add submission to reference to that check
                    if (!ChecksReference[c.GetType()].Contains(s.Value))
                    {
                        ChecksReference[c.GetType()].Add(s.Value);
                    }
  
                }
            }

            layout.Append("<p>");


            StringBuilder checkBlocks = new StringBuilder();
            foreach(KeyValuePair<CheckFactory.CheckType, List<Submissions.Submission>> c in ChecksReference)
            {
                
                ICheck checkInstance = c.Value[0].GetCheck(c.Key);
                if ( checkInstance != null )
                {
                    // Create Menu Item
                    layout.Append("<a class=\"btn btn" + Content.GetColorCodeByWeight(checkInstance.GetWeight()) + " m-2\" data-toggle=\"collapse\" href=\"#overview-" + checkInstance.GetID() + "\" role=\"button\" aria-expanded=\"false\" data-parent=\"#overview-checks\">" + checkInstance.GetName() + " <span class=\"badge badge-light\">" + c.Value.Count + "</span></a>");

                    // Create Submission Block
                    checkBlocks.Append(
                        "<div class=\"collapse\" id=\"overview-" + checkInstance.GetID() +"\">" +
                            "<div class=\"card\">" +
                                "<h3 class=\"card-header\">" + checkInstance.GetName() + "</h3>" +
                                "<div class=\"card-body\">" +
                                "<p class=\"card-text text-muted\"><em>" + checkInstance.GetDescription() + " [<a href=\"" + checkInstance.GetKBLink() + "\" title=\"Knowledge Base Article\" target=\"_blank\">More</a>]</em></p>");


                    foreach(Submissions.Submission s in c.Value)
                    {
                        // Left Side Origin
                        // -- Robin
                        // WAS:
                        /*
                        checkBlocks.Append(
                        "<div class=\"card-group\" style=\"padding-bottom: 15px;\">" +
                            "<div class=\"card\" style=\"background-color:rgba(0,0,0,.01);\">" +
                                "<div class=\"card-body\">" +
                                    "<h5 class=\"card-title\"><a href=\"#" + s.GUID.ToString() + "\">" + s.FileNameWithExtension + "</a></h5>" + 
                                    "<h6 class=\"card-subtitle mb-2 text-muted\">" + s.RelativePathNoFile + "</h6>" +
                                    "<p class=\"card-text\"><small class=\"text-muted\">" + s.GetCheck(c.Key).GetLocalReference() + "</small></p>" +
                                "</div>" +
                            "</div>");
                        */
                        
                        checkBlocks.Append(
                        "<div class=\"card-group\" style=\"padding-bottom: 15px;\">" +
                            "<div class=\"card\" style=\"background-color:rgba(0,0,0,.01);\">" +
                                "<div class=\"card-body\">" +
                                    "<h5 class=\"card-title\"><a href=\"#" + s.GUID.ToString() + "\">" + s.FileNameWithExtension + "</a></h5>" + 
                                    "<h6 class=\"card-subtitle mb-2 text-muted\">" + s.ContainerPath + "</h6>" +
                                    "<p class=\"card-text\"><small class=\"text-muted\">" + s.GetCheck(c.Key).GetLocalReference() + "</small></p>" +
                                "</div>" +
                            "</div>");

                        checkBlocks.Append("<div class=\"card text-right\"><div class=\"card-body\">");

                        // Right Side Matches
                        foreach(KeyValuePair<Submissions.Submission, FlagItem> entry in s.GetCheck(c.Key).GetFlag().Similar)
                        {
                            // -- Robin
                            // WAS:
                            /*
                            checkBlocks.Append(
                                "<h5 class=\"card-title\"><a href=\"#" + entry.Key.GUID.ToString() + "\">" + entry.Key.FileNameWithExtension + "</a></h5>" +
                                "<h6 class=\"card-subtitle mb-2 text-muted\">" + entry.Key.RelativePathNoFile + "</h6>");
                            */
                            
                            checkBlocks.Append(
                                "<h5 class=\"card-title\"><a href=\"#" + entry.Key.GUID.ToString() + "\">" + entry.Key.FileNameWithExtension + "</a></h5>" +
                                "<h6 class=\"card-subtitle mb-2 text-muted\">" + entry.Key.ContainerPath + "</h6>");

                            checkBlocks.Append("<p class=\"card-text\"><small class=\"text-muted\">");
                            if ( entry.Value.Reference != "" && entry.Value.Reference != string.Empty)
                            {
                                checkBlocks.Append(entry.Value.Reference);
                            }
                            checkBlocks.Append(" [" + Compare.ToPercentage((float)entry.Value.Similarity) + "]");
                            checkBlocks.Append("</small></p>");
                        }
                        checkBlocks.Append("</div></div></div>");
                    }


                    checkBlocks.Append("</div></div></div>");

                }
            }
            if (ChecksReference.Count == 0)
            {
                layout.Append("<div class=\"alert alert-success\">" + OverviewLocalization.NoChecksNotice + "</div>");
            }

            layout.Append("</p><div class=\"accordion-group\">");
            layout.Append(checkBlocks.ToString());
            layout.Append("</div></div>");


            return layout.ToString();
        }

        string CreateSubmissionSummary()
        {
            StringBuilder layout = new StringBuilder();
            layout.Append(  "<div id=\"overview-submissions\" class=\"container\" style=\"padding-bottom: 30px;\">" +
                                "<h2>" + OverviewLocalization.SubmissionSummaryTitle + "</h2>" +
                                "<p>" + OverviewLocalization.SubmissionSummaryDescription + "</p>" +
                                "<ul id=\"submission-summary-list\" class=\"list-group\">");



            // Sort Submissions
            Dictionary<Guid, Submissions.Submission> FlaggedSubmissions = new Dictionary<Guid, Submissions.Submission>();
            Dictionary<Guid, Submissions.Submission> ProcessedSubmissions = new Dictionary<Guid, Submissions.Submission>();
            Dictionary<Guid, Submissions.Submission> InvalidSubmissions = new Dictionary<Guid, Submissions.Submission>(); // No Checks
            foreach (KeyValuePair<Guid, Submissions.Submission> s in _submissions)
            {
                // Invalid
                if (s.Value.Checks.Count == 0 ){
                    InvalidSubmissions.Add(s.Key, s.Value);
                }

                // Flagged
                if (s.Value.Flagged)
                {
                    FlaggedSubmissions.Add(s.Key, s.Value);;
                }

                // Processed
                if (s.Value.Processed)
                {
                    ProcessedSubmissions.Add(s.Key, s.Value);
                }
            }

            // Total Submissions Item
            if ( _submissions.Count <= 0 ) 
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryTotalSubmissions, "hand " + ListGroupItemDanger, true, _submissions.Count, "id=\"submission-summary-total-button\""));
            }
            else if ( _submissions.Count <= 3 ) 
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryTotalSubmissions, "hand " + ListGroupItemWarning, true, _submissions.Count, "id=\"submission-summary-total-button\"")); 
            }
            else 
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryTotalSubmissions, "hand", true, _submissions.Count, "id=\"submission-summary-total-button\""));
            }

            // Total Submissions List
            if (_submissions.Count > 0)
            {
                layout.Append("<div id=\"submission-summary-total\" class=\"collapse\" role=\"tabpanel\">" + Content.GetSubmissionListTable(_submissions, "list-group-item") + "</div>");
            }
            else
            {
                layout.Append("<div id=\"submission-summary-total\" class=\"collapse\" role=\"tabpanel\"><div class=\"list-group-item\">" + OverviewLocalization.NoItemsFound + "</div></div>");
            }

            // Processed Submissions Item
            if ( ProcessedSubmissions.Count == 0 )
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryProcessedSubmissions, "hand " + ListGroupItemDanger, true, ProcessedSubmissions.Count, "id=\"submission-summary-processed-button\""));
            }
            else if ( ProcessedSubmissions.Count <= 3 )
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryProcessedSubmissions, "hand " + ListGroupItemWarning, true, ProcessedSubmissions.Count, "id=\"submission-summary-processed-button\""));
            }
            else
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryProcessedSubmissions, "hand", true, ProcessedSubmissions.Count, "id=\"submission-summary-processed-button\""));
            }

            // Processed Submissions List
            if (ProcessedSubmissions.Count > 0)
            {
                layout.Append("<div id=\"submission-summary-processed\" class=\"collapse\" role=\"tabpanel\">" + Content.GetSubmissionListTable(ProcessedSubmissions, "list-group-item") + "</div>");
            }
            else
            {
                layout.Append("<div id=\"submission-summary-processed\" class=\"collapse\" role=\"tabpanel\"><div class=\"list-group-item\">" + OverviewLocalization.NoItemsFound + "</div></div>");
            }

            // Invalid Submissions Item
            if ( InvalidSubmissions.Count > 5 )
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryInvalidSubmissions, "hand " + ListGroupItemDanger, true, InvalidSubmissions.Count, "id=\"submission-summary-invalid-button\""));
            } 
            else if ( InvalidSubmissions.Count > 0 ) 
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryInvalidSubmissions, "hand " + ListGroupItemWarning, true, InvalidSubmissions.Count, "id=\"submission-summary-invalid-button\""));
            }
            else 
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryInvalidSubmissions, "hand", true, InvalidSubmissions.Count, "id=\"submission-summary-invalid-button\""));
            }

            if (InvalidSubmissions.Count > 0)
            {
                // Invalid Submissions List
                layout.Append("<div id=\"submission-summary-invalid\" class=\"collapse\" role=\"tabpanel\">" + Content.GetSubmissionListTable(InvalidSubmissions, "list-group-item") + "</div>");
            }
            else
            {
                layout.Append("<div id=\"submission-summary-invalid\" class=\"collapse\" role=\"tabpanel\"><div class=\"list-group-item\">" + OverviewLocalization.NoItemsFound + "</div></div>");
            }

            // Flagged Submissions Item
            if (FlaggedSubmissions.Count > 5)
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryFlaggedSubmissions, "hand " + ListGroupItemDanger, true, FlaggedSubmissions.Count, "id=\"submission-summary-flagged-button\""));
            }
            else if (FlaggedSubmissions.Count > 0)
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryFlaggedSubmissions, "hand " + ListGroupItemWarning, true, FlaggedSubmissions.Count, "id=\"submission-summary-flagged-button\""));
            }
            else
            {
                layout.Append(ListGroupItem(OverviewLocalization.SubmissionSummaryFlaggedSubmissions, "hand", true, FlaggedSubmissions.Count, "id=\"submission-summary-flagged-button\""));
            }

            // Flagged Submissions List
            if (FlaggedSubmissions.Count > 0)
            {
                layout.Append("<div id=\"submission-summary-flagged\" class=\"collapse\" role=\"tabpanel\">" + Content.GetSubmissionListTable(FlaggedSubmissions, "list-group-item") + "</div>");
            }
            else
            {
                layout.Append("<div id=\"submission-summary-flagged\" class=\"collapse\" role=\"tabpanel\"><div class=\"list-group-item\">" + OverviewLocalization.NoItemsFound + "</div></div>");
            }

            layout.Append("</ul></div>");

            return layout.ToString();
        }

        static string ListGroupItem(string text, string classes = "", bool badge = false, int badgeAmount = 0, string extraTagInfo = "")
        {
            StringBuilder layout = new StringBuilder();


            layout.Append("<li class=\"list-group-item d-flex justify-content-between align-items-center");

            if (classes != "" && classes != string.Empty)
            {
                layout.Append(" " + classes);
            }
            layout.Append("\"");

            if (extraTagInfo != "" && extraTagInfo != string.Empty)
            {
                layout.Append(" " + extraTagInfo);
            }

            layout.Append(">" + text);
            if (badge)
            {
                layout.Append(" <span class=\"badge badge-pill\">" + badgeAmount.ToString() + "</span>");
            }
            layout.Append("</li>");
            return layout.ToString();
        }
    }
}
