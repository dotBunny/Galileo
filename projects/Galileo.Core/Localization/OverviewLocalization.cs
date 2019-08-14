using System;
namespace Galileo.Core.Localization
{
    public static class OverviewLocalization
    {
        public static string Header
        {
            get
            {
                return "Overview";
            }
        }
        public static string Description
        {
            get
            {
                return "hunted in";
            }

        }
        public static string NoItemsFound
        {
            get
            {
                return "No items found.";
            }
        }
        public static string SubmissionSummaryTitle
        {
            get
            {
                return "Submission Summary";
            }
        }

        public static string SubmissionSummaryDescription
        {
            get
            {
                return "While Galileo strives to be able to handle all submissions thrown at it; there are limits and times where it can only do so much. Below is a numeric breakdown on what submissions were looked at, and if Galileo was able to process them. <em>Pay special attention to submissions that are unable to be processed</em>.";
            }
        }

        public static string SubmissionSummaryTotalSubmissions
        {
            get
            {
                return "Total Submissions";
            }
        }

        public static string SubmissionSummaryProcessedSubmissions
        {
            get
            {
                return "Processed Submissions";
            }
        }

        public static string SubmissionSummaryInvalidSubmissions
        {
            get
            {
                return "Invalid Submissions";
            }
        }

        public static string SubmissionSummaryFlaggedSubmissions
        {
            get
            {
                return "Flagged Submissions";
            }
        }

        public static string DisabledCheckSummaryTitle
        {
            get
            {
                return "Disabled Check Summary";
            }
        }

        public static string CheckSummaryTitle
        {
            get
            {
                return "Check Summary";
            }
        }
        public static string CheckSummaryDescription
        {
            get
            {
                return "Below are a list of <em>Checks</em> which have been triggered that need your review. The <strong class=\"text-danger\">Red</strong> and <strong class=\"text-warning\">Yellow</strong> <em>Checks</em> should be reviewed for possible instances of plagarism. The <strong class=\"text-secondary\">Grey</strong> <em>Checks</em> are informational, but might provide additional information in making an educated decision about the integrity of an assignment.</p><p><em>Click a Check button to display it's alerts.</em>";
            }
        }

        public static string NoChecksNotice
        {
            get
            {
                return "Wait ... you don't have any checks flagged. That's amazing.";
            }
        }

        public static string DisabledChecksTitle
        {
            get
            {
                return "Disabled Checks";
            }
        }
        public static string DisabledChecksNone
        {
            get
            {
                return "No checks were disabled during this process.";
            }
        }
        public static string DisabledChecksDescription
        {
            get
            {
                return "Below are a list of <em>Checks</em> which have been disabled for this process.";
            }
        }
    }
}
