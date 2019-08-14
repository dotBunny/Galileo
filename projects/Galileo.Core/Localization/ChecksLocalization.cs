using System;
namespace Galileo.Core.Localization
{
    public static class ChecksLocalization
    {
        public static string ContentCheckName
        {
            get
            {
                return "Content Comparison";
            }
        }
        public static string ContentCheckDescription
        {
            get
            {
                return "Analysis between the content of two different submissions using a Levenshtein Distance calculation to produce a percentage of similarity.";
            }
        }
        public static string ContentHashName
        {
            get
            {
                return "Content Hash Comparison";
            }
        }
        public static string ContentHashDescription
        {
            get
            {
                return "Quickly compares hashes of calculated content to determine likeness.";
            }
        }
        public static string FileNameName
        {
            get
            {
                return "File Name Comparison";
            }
        }
        public static string FileNameCheckDescription
        {
            get
            {
                return "Evaluate the differences between file names using a Levenshtein Distance calculation to a percent difference.";
            }
        }
        public static string MetaCreatorName
        {
            get
            {
                return "Meta Data Creator";
            }
        }
        public static string MetaCreatorDescription
        {
            get
            {
                return "Look for identical creator information inside the file, ignoring usernames found in the shared ignored username list.";
            }
        }

        public static string MetaLastModifiedByName
        {
            get
            {
                return "Meta Last Modified By";
            }
        }
        public static string MetaLastModifiedByDescription
        {
            get
            {
                return "Look for identical users having last modified submissions, ignoring usernames found in the shared ignored username list.";
            }
        }
        public static string MetaDateCreatedName
        {
            get
            {
                return "Meta Date Created";
            }
        }
        public static string MetaDateCreatedDescription
        {
            get
            {
                return "Look for identical file creation dates shared between submissions.";
            }
        }
        public static string MetaDateLastPrintedName
        {
            get
            {
                return "Meta Date Last Printed";
            }
        }
        public static string MetaDateLastPrintedDescription
        {
            get
            {
                return "Look for identical last printed dates shared between submissions.";
            }
        }
        public static string MetaDateModifiedName
        {
            get
            {
                return "Meta Date Modified";
            }
        }
        public static string MetaDateModifiedDescription
        {
            get
            {
                return "Look for identical last modified dates shared between submissions.";
            }
        }

    }
}
