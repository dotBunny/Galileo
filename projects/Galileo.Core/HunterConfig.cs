using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using Galileo.Core.TypeConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Galileo.Core
{
    /// <summary>
    /// Hunter Config
    /// </summary>
    [JsonObject]
    public class HunterConfig
    {
		#region Fields

        [JsonIgnore]
		public static readonly string GalileoDefaultDataFolder = "_GALILEO";

        [JsonIgnore]
        public static readonly string[] ProcessNameIgnoreDefaults = { "lab", "assignment", "bonus", "questions", "question", "version", "answers", "wk", "week", "starter", "revised" };

        [JsonIgnore]
        public static readonly string[] SharedIgnoredFilesDefaults = {  };

        [JsonIgnore]
		public static readonly string[] SharedIgnoredFoldersDefaults = { };

        [JsonIgnore]
        public static readonly string[] SharedIgnoredFileExtensionsDefaults = { };

        [JsonIgnore]
        public static readonly string[] SharedIgnoredUsernamesDefaults = { "Windows User", "Student", "Unknown Creator" };

        [JsonProperty]
        public const int Version = 4;

        [JsonProperty("Process.Archives.Extract")]
        bool  _processArchivesExtract = true;

        [JsonProperty("Process.Archives.ExtractOnlySubmissions")]
        bool _processArchivesExtractOnlySubmissions = false;

        [JsonProperty("Process.Archives.Delete")]
        bool _processArchivesDelete = true;

        [JsonProperty("Process.Report.EmbedResources")]
        bool _processReportEmbedResources = true;

        [JsonProperty("Process.Name.Ignore")]
        string[] _processNameIgnore = ProcessNameIgnoreDefaults;

        [JsonProperty("Shared.IgnoredFiles")]
        string[] _sharedIgnoredFiles = SharedIgnoredFilesDefaults;

        [JsonProperty("Shared.IgnoredFolders")]
        string[] _sharedIgnoredFolders = SharedIgnoredFoldersDefaults;

        [JsonProperty("Shared.IgnoredFileExtensions")]
        string[] _sharedIgnoredFileExtensions = SharedIgnoredFileExtensionsDefaults;

        [JsonProperty("Shared.IgnoredUsernames")]
        string[] _sharedIgnoredUsernames = SharedIgnoredUsernamesDefaults;

        [JsonProperty("Check.FileName.Enabled")]
        bool _checkFileName = true;

        [JsonProperty("Check.FileName.Threshold")]
        float _checkFileNameThreshold = 0.92f;

        [JsonProperty("Check.FileName.Weight")]
        float _checkFileNameWeight = 0.8f;
        
        [JsonProperty("Check.Meta.Creator.Enabled")]
        bool _checkMetaCreator = true;

        [JsonProperty("Check.Meta.Creator.Weight")]
        float _checkMetaCreatorWeight = 0.95f;

        [JsonProperty("Check.Meta.DateCreated.Enabled")]
        bool _checkMetaDateCreated = true;

        [JsonProperty("Check.Meta.DateCreated.Weight")]
        float _checkMetaDateCreatedWeight = 0.4f;

        [JsonProperty("Check.Meta.DateLastPrinted.Enabled")]
        bool _checkMetaDateLastPrinted = true;

        [JsonProperty("Check.Meta.DateLastPrinted.Weight")]
        float _checkMetaDateLastPrintedWeight = 0.2f;

        [JsonProperty("Check.Meta.DateModified.Enabled")]
        bool _checkMetaDateModified = true;

        [JsonProperty("Check.Meta.DateModified.Weight")]
        float _checkMetaDateModifiedWeight = 0.8f;

        [JsonProperty("Check.Meta.LastModifiedBy.Enabled")]
        bool _checkMetaLastModifiedBy = true;

        [JsonProperty("Check.Meta.LastModifiedBy.Weight")]
        float _checkMetaLastModifiedByWeight = 0.95f;

        [JsonProperty("Check.Content.Enabled")]
        bool _checkContent = true;

        [JsonProperty("Check.Content.MaximumLength")]
        int _checkContentMaximumLength = 10000;

        [JsonProperty("Check.Content.Threshold")]
        float _checkContentThreshold = 0.9f;

        [JsonProperty("Check.Content.Weight")]
        float _checkContentWeight = 0.7f;

        [JsonProperty("Platform.Parallelism.MaxDegrees")]
        int _platformParallelismMaxDegrees = -1;

        #endregion

        #region Properties

        [Category("Process")]
        [Description("Should Galileo extract archives when evaluating the target folder?")]
        [DisplayName( "Extract Archives")]
        [DefaultValue(true)]
        public bool ProcessArchivesExtract { get { return _processArchivesExtract; } set { _processArchivesExtract = value;  } }

        [Category("Process")]
        [Description("Should Galileo extract only submissions from archives when evaluating the target folder?")]
        [DisplayName("Extract Only Submissions")]
        [DefaultValue(false)]
        public bool ProcessArchivesExtractOnlySubmissions { get { return _processArchivesExtractOnlySubmissions; } set { _processArchivesExtractOnlySubmissions = value; } }

        [Category("Process")]
        [Description("Should Galileo delete archives once extracted?")]
        [DisplayName("Delete Archives")]
        [DefaultValue(true)]
        public bool ProcessArchivesDelete { get { return _processArchivesDelete; } set { _processArchivesDelete = value; } }

        [Category("Process")]
        [Description("Should generated reports contain all resources needed to render? (No external links)")]
        [DisplayName("Embed Resources")]
        [DefaultValue(true)]
        public bool ProcessReportEmbedResources { get { return _processReportEmbedResources; } set { _processReportEmbedResources = value; } }

        [Category("Process")]
        [Description("Ignored items when attempting to detect the person's name from a submission.")]
        [DisplayName("Ignore Names")]
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] ProcessNameIgnore { get { return _processNameIgnore; } set { _processNameIgnore = value; } }
        public bool ShouldSerializeProcessNameIgnore()
        {
            return !ProcessNameIgnore.SameAs(ProcessNameIgnoreDefaults);
        }

        [Category("Shared")]
        [Description("Ignored filenames when evaluating the target folder.")]
        [DisplayName("Ignore Files")]
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] SharedIgnoredFiles { get { return _sharedIgnoredFiles; } set { _sharedIgnoredFiles = value; } }
        public bool ShouldSerializeSharedIgnoredFiles()
        {
            return !SharedIgnoredFiles.SameAs(SharedIgnoredFilesDefaults);
        }

        [Category("Shared")]
        [Description("Ignored folders when evaluating the target folder.")]
        [DisplayName("Ignore Folders")]
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] SharedIgnoredFolders { get { return _sharedIgnoredFolders; } set { _sharedIgnoredFolders = value; } }
        public bool ShouldSerializeSharedIgnoredFolders()
        {
            return !SharedIgnoredFolders.SameAs(SharedIgnoredFoldersDefaults);
        }

        [Category("Shared")]
        [Description("Ignored file extensions when evaluating the target folder.")]
        [DisplayName("Ignore File Extensions")]
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] SharedIgnoredFileExtensions { get { return _sharedIgnoredFileExtensions; } set { _sharedIgnoredFileExtensions = value; } }
        public bool ShouldSerializeSharedIgnoredFileExtensions()
        {
            return !SharedIgnoredFileExtensions.SameAs(SharedIgnoredFileExtensionsDefaults);
        }

        [Category("Shared")]
        [Description("Safe usernames found within files that won't get flagged.")]
        [DisplayName("Ignore Usernames")]
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] SharedIgnoredUsernames { get { return _sharedIgnoredUsernames; } set { _sharedIgnoredUsernames = value; } }
        public bool ShouldSerializeSharedIgnoredUsernames()
        {
            return !SharedIgnoredUsernames.SameAs(SharedIgnoredUsernamesDefaults);
        }

        [Category("File")]
        [Description("Evaluate the differences between file names using a Levenshtein Distance calculation to a percent difference. (Enabled?)")]
        [DisplayName("Check Filenames")]
        [DefaultValue(true)]
        public bool CheckFileName { get { return _checkFileName; } set { _checkFileName = value; } }

        [Category("File")]
        [Description("File names with greater than or equal to what percent similarity will raise a flag.")]
        [DisplayName("Filename Threshold")]
        [DefaultValue(0.92f)]
        public float CheckFileNameThreshold { get { return _checkFileNameThreshold; } set { _checkFileNameThreshold = value; } }

        [Category("File")]
        [Description("File names check weighting.")]
        [DisplayName("Filename Weighting")]
        [DefaultValue(0.8f)]
        public float CheckFileNameWeight { get { return _checkFileNameWeight; } set { _checkFileNameWeight = value; } }

        [Category("Meta Data")]
        [Description("Look for identical creator information inside the file, ignoring usernames found in the shared ignored username list. (Enabled?)")]
        [DisplayName("Check Creator")]
        [DefaultValue(true)]
        public bool CheckMetaCreator { get { return _checkMetaCreator; } set { _checkMetaCreator = value; } }

        [Category("Meta Data")]
        [Description("Meta data creator check weighting.")]
        [DisplayName("Creator Weighting")]
        [DefaultValue(0.95f)]
        public float CheckMetaCreatorWeight { get { return _checkMetaCreatorWeight; } set { _checkMetaCreatorWeight = value; } }

        [Category("Meta Data")]
        [Description("Look for identical file creation dates shared between submissions. (Enabled?)")]
        [DisplayName("Check Date Created")]
        [DefaultValue(true)]
        public bool CheckMetaDateCreated { get { return _checkMetaDateCreated; } set { _checkMetaDateCreated = value; } }

        [Category("Meta Data")]
        [Description("Meta data date created check weighting.")]
        [DisplayName("Date Created Weighting")]
        [DefaultValue(0.4f)]
        public float CheckMetaDateCreatedWeight { get { return _checkMetaDateCreatedWeight; } set { _checkMetaDateCreatedWeight = value; } }

        [Category("Meta Data")]
        [Description("Look for identical last printed dates shared between submissions.")]
        [DisplayName("Check Last Print Date")]
        [DefaultValue(true)]
        public bool CheckMetaDateLastPrinted { get { return _checkMetaDateLastPrinted; } set { _checkMetaDateLastPrinted = value; } }

        [Category("Meta Data")]
        [Description("Meta data date last printed check weighting.")]
        [DisplayName("Last Print Date Weighting")]
        [DefaultValue(0.2f)]
        public float CheckMetaDateLastPrintedWeight { get { return _checkMetaDateLastPrintedWeight; } set { _checkMetaDateLastPrintedWeight = value; } }

        [Category("Meta Data")]
        [Description("Look for identical last modified dates shared between submissions.")]
        [DisplayName("Check Date Modified")]
        [DefaultValue(true)]
        public bool CheckMetaDateModified { get { return _checkMetaDateModified; } set { _checkMetaDateModified = value; } }

        [Category("Meta Data")]
        [Description("Meta data date last modified check weighting.")]
        [DisplayName("Date Modified Weighting")]
        [DefaultValue(0.8f)]
        public float CheckMetaDateModifiedWeight { get { return _checkMetaDateModifiedWeight; } set { _checkMetaDateModifiedWeight = value; } }

        [Category("Meta Data")]
        [Description ("Look for identical users having last modified submissions, ignoring usernames found in the shared ignored username list.")]
        [DisplayName("Check Last Modified By")]
        [DefaultValue(true)]
        public bool CheckMetaLastModifiedBy { get { return _checkMetaLastModifiedBy; } set { _checkMetaLastModifiedBy = value; } }

        [Category("Meta Data")]
        [Description("Meta data last modified by check weighting.")]
        [DisplayName("Last Modified By Weighting")]
        [DefaultValue(0.95f)]
        public float CheckMetaLastModifiedByWeight { get { return _checkMetaLastModifiedByWeight; } set { _checkMetaLastModifiedByWeight = value; } }

        [Category("Content")]
        [Description("Analysis between the content of two different submissions using a Levenshtein Distance calculation to produce a percentage of similarity. (Enabled?)")]
        [DisplayName("Check Content")]
        [DefaultValue(true)]
        public bool CheckContent { get { return _checkContent; } set { _checkContent = value; } }

        [Category("Content")]
        [Description("The maximum length of content in a file to check using percent difference calculations.")]
        [DisplayName("Maximum Length")]
        [DefaultValue(10000)]
        public int CheckContentMaximumLength { get { return _checkContentMaximumLength; } set { _checkContentMaximumLength = value; } }

        [Category("Content")]
        [Description("Files with content greater than or equal to what percentage will raise a flag.")]
        [DisplayName("Threshold")]
        [DefaultValue(0.9f)]
        public float CheckContentThreshold { get { return _checkContentThreshold; } set { _checkContentThreshold = value; } }

        [Category("Content")]
        [Description("Content check weighting.")]
        [DisplayName("Weighting")]
        [DefaultValue(0.7f)]
        public float CheckContentWeight { get { return _checkContentWeight; } set { _checkContentWeight = value; } }

        [Category("Platform")]
        [Description("The maximum number of cores to dedicate to worker threads (-1 for all).")]
        [DisplayName("Number Of Cores")]
        [DefaultValue(-1)]
        public int PlatformParallelismMaxDegrees { get { return _platformParallelismMaxDegrees; } set { _platformParallelismMaxDegrees = value; } }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            // Shared Settings
            builder.Append(Markdown.H3("Shared Settings"));
            builder.Append("Setting | Value" + Platform.EndOfLine());
            builder.Append("------- | :----" + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.SharedIgnoredFolders + " | " + SharedIgnoredFolders.GetString() + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.SharedIgnoredFiles + " | " + SharedIgnoredFiles.GetString() + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.SharedIgnoredFileExtensions + " | " + SharedIgnoredFileExtensions.GetString() + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.SharedIgnoredUsernames + " | " + SharedIgnoredUsernames.GetString() + Platform.EndOfLine());
            builder.Append(Markdown.Linefeed());

            // File Settings
            builder.Append(Markdown.H3("File Settings"));
            builder.Append("Setting | Value" + Platform.EndOfLine());
            builder.Append("------- | :----" + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckFileName + " | " + CheckFileName + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckFileNameThreshold + " | " + CheckFileNameThreshold + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckFileNameWeight + " | " + CheckFileNameWeight + Platform.EndOfLine());
            builder.Append(Markdown.Linefeed());

            // Meta Data Settings
            builder.Append(Markdown.H3("Meta Data Settings"));
            builder.Append("Setting | Value" + Platform.EndOfLine());
            builder.Append("------- | :----" + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaCreator + " | " + CheckMetaCreator + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaCreatorWeight + " | " + CheckMetaCreatorWeight + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaDateCreated + " | " + CheckMetaDateCreated + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaDateCreatedWeight + " | " + CheckMetaDateCreatedWeight + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaDateLastPrinted + " | " + CheckMetaDateLastPrinted + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaDateLastPrintedWeight + " | " + CheckMetaDateLastPrintedWeight + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaDateModified + " | " + CheckMetaDateModified + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaDateModifiedWeight + " | " + CheckMetaDateModifiedWeight + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaLastModifiedBy + " | " + CheckMetaLastModifiedBy + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckMetaLastModifiedByWeight + " | " + CheckMetaLastModifiedByWeight + Platform.EndOfLine());
            builder.Append(Markdown.Linefeed());

            // Content Settings
            builder.Append(Markdown.H3("Content Settings"));
            builder.Append("Setting | Value" + Platform.EndOfLine());
            builder.Append("------- | :----" + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckContent + " | " + CheckContent + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckContentMaximumLength + " | " + CheckContentMaximumLength + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckContentThreshold + " | " + CheckContentThreshold + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.CheckContentWeight + " | " + CheckContentWeight + Platform.EndOfLine());
            builder.Append(Markdown.Linefeed());

            // Process Settings
            builder.Append(Markdown.H3("Process Settings"));
            builder.Append("Setting | Value" + Platform.EndOfLine());
            builder.Append("------- | :----" + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.ProcessArchivesExtract + " | " + ProcessArchivesExtract.ToString() + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.ProcessArchivesExtractOnlySubmissions + " | " + ProcessArchivesExtractOnlySubmissions.ToString() + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.ProcessArchivesDelete + " | " + ProcessArchivesDelete.ToString() + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.ProcessReportEmbedResources + " | " + ProcessReportEmbedResources.ToString() + Platform.EndOfLine());
            builder.Append(HunterConfigSerialization.ProcessNameIgnore + " | " + ProcessNameIgnore.GetString() + Platform.EndOfLine());
            builder.Append(Markdown.Linefeed());

            // Process Settings
            builder.Append(Markdown.H3("Platform Settings"));
            builder.Append("Setting | Value" + Platform.EndOfLine());
            builder.Append("------- | :----" + Platform.EndOfLine());
            if (PlatformParallelismMaxDegrees == -1)
            {
                builder.Append(HunterConfigSerialization.PlatformParallelismMaxDegrees + " | Max (-1)" + Platform.EndOfLine());
            }
            else
            {
                builder.Append(HunterConfigSerialization.PlatformParallelismMaxDegrees + " | " + PlatformParallelismMaxDegrees.ToString() + Platform.EndOfLine());   
            }
            builder.Append(Markdown.Linefeed());


            return builder.ToString();
        }

        public static string GetDataPath(string baseFolder)
        {
			return System.IO.Path.Combine(baseFolder, GalileoDefaultDataFolder);
        }
        public static string GetConfigPath(string baseFolder)
        {
            return System.IO.Path.Combine(GetDataPath(baseFolder), "galileo.json");
        }
        public static string GetLogPath(string baseFolder, string suffix)
        {
            return System.IO.Path.Combine(GetDataPath(baseFolder), "galileo_" + suffix + ".md");
        }
        public static string GetReportPath(string baseFolder)
        {
            return System.IO.Path.Combine(GetDataPath(baseFolder), "galileo.html");
        }

        public static HunterConfig GetConfig(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<HunterConfig>(System.IO.File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Logging.Log.Session.Add("Core.HunterConfig.GetConfig", "Failed to get config @ " + path + ": " + e.Message);
                return null;
            }
        }

        public void WriteConfig(string absolutePath)
		{
			// Always write out the config, updating any values in it that weren't found in loading
            System.IO.File.WriteAllText(absolutePath, JsonConvert.SerializeObject(this, Formatting.Indented));
		}
        #endregion
    }
}
