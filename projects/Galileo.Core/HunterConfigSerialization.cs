namespace Galileo.Core
{
    public class HunterConfigSerialization
    {
        public const string PlatformParallelismMaxDegrees = "Platform.Parallelism.MaxDegrees";

        public const string ProcessArchivesExtract = "Process.Archives.Extract";
        public const string ProcessArchivesExtractOnlySubmissions = "Process.Archives.ExtractOnlySubmissions";
        public const string ProcessArchivesDelete = "Process.Archives.Delete";
        public const string ProcessReportEmbedResources = "Process.Report.EmbedResources";
        public const string ProcessNameIgnore = "Process.Name.Ignore";

        public const string SharedIgnoredFiles = "Shared.IgnoredFiles";
        public const string SharedIgnoredFolders = "Shared.IgnoredFolders";
        public const string SharedIgnoredFileExtensions = "Shared.IgnoredFileExtensions";
        public const string SharedIgnoredUsernames = "Shared.IgnoredUsernames";

        public const string CheckFileName = "Check.FileName.Enabled";
        public const string CheckFileNameThreshold = "Check.FileName.Threshold";
        public const string CheckFileNameWeight = "Check.FileName.Weight";

        public const string CheckMetaCreator = "Check.Meta.Creator.Enabled";
        public const string CheckMetaCreatorWeight = "Check.Meta.Creator.Weight";
        public const string CheckMetaDateCreated = "Check.Meta.DateCreated.Enabled";
        public const string CheckMetaDateCreatedWeight = "Check.Meta.DateCreated.Weight";
        public const string CheckMetaDateLastPrinted = "Check.Meta.DateLastPrinted.Enabled";
        public const string CheckMetaDateLastPrintedWeight = "Check.Meta.DateLastPrinted.Weight";
        public const string CheckMetaDateModified = "Check.Meta.DateModified.Enabled";
        public const string CheckMetaDateModifiedWeight = "Check.Meta.DateModified.Weight";
        public const string CheckMetaLastModifiedBy = "Check.Meta.LastModifiedBy.Enabled";
        public const string CheckMetaLastModifiedByWeight = "Check.Meta.LastModifiedBy.Weight";

        public const string CheckContent = "Check.Content.Enabled";
        public const string CheckContentMaximumLength = "Check.Content.MaximumLength";
        public const string CheckContentThreshold = "Check.Content.Threshold";
        public const string CheckContentWeight = "Check.Content.Weight";
    }
}
