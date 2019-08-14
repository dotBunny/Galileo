using System;
namespace Galileo.Client
{
	/// <summary>
    /// Client Procss Config Representation
    /// </summary>
    public static class ProcessConfig
    {
		
        public enum ConfigDataSourceObjectType
        {
            Boolean,
            String,
            Float,
            Integer,
            FilePath,
            FolderPath,
            Invalid
        }

        public class ConfigDataSourceObject
        {
            public ConfigDataSourceObject(string category, string newSetting, string newValue, string description, ConfigDataSourceObjectType newType = ConfigDataSourceObjectType.Boolean, bool readOnly = false)
            {
                Category = category;
                Setting = newSetting;
                Value = newValue;
                Type = newType;
                Description = description;
                IsReadOnly = readOnly;
            }
            public string Category;
            public string Setting;
            public string Value;
            public string Description;
            public ConfigDataSourceObjectType Type;
            public bool IsReadOnly;

            public override string ToString()
            {
                return Value;
            }

            public ConfigDataSourceObject Clone()
            {
                return new ConfigDataSourceObject(Category, Setting, Value, Description, Type, IsReadOnly);
            }
        }

        /// <summary>
        /// Create a DataSource set of data from config
        /// </summary>
        /// <returns>The data source items</returns>
        public static System.Collections.Generic.List<ConfigDataSourceObject> GetDataSourceObject(Core.HunterConfig config)
        {
            System.Collections.Generic.List<ConfigDataSourceObject> returnDataset = new System.Collections.Generic.List<ConfigDataSourceObject>
            {
                // Shared Settings
                new ConfigDataSourceObject("Shared", Core.HunterConfigSerialization.SharedIgnoredFolders, string.Join(", ", config.SharedIgnoredFolders), "Ignored folders when evaluating the target folder.", ConfigDataSourceObjectType.String),
                new ConfigDataSourceObject("Shared", Core.HunterConfigSerialization.SharedIgnoredFiles, string.Join(", ", config.SharedIgnoredFiles), "Ignored filenames when evaluating the target folder.", ConfigDataSourceObjectType.String),
                new ConfigDataSourceObject("Shared", Core.HunterConfigSerialization.SharedIgnoredFileExtensions, string.Join(", ", config.SharedIgnoredFileExtensions), "Ignored file extensions when evaluating the target folder.", ConfigDataSourceObjectType.String),
                new ConfigDataSourceObject("Shared", Core.HunterConfigSerialization.SharedIgnoredUsernames, string.Join(", ", config.SharedIgnoredUsernames), "Safe usernames found within files that won't get flagged.", ConfigDataSourceObjectType.String),

                // File Settings
                new ConfigDataSourceObject("File", Core.HunterConfigSerialization.CheckFileName, config.CheckFileName.ToString(), Core.Localization.ChecksLocalization.FileNameCheckDescription + " (Enabled?)"),
                new ConfigDataSourceObject("File", Core.HunterConfigSerialization.CheckFileNameThreshold, config.CheckFileNameThreshold.ToString(), "File names with greater than or equal to what percent similarity will raise a flag.", ConfigDataSourceObjectType.Float),
                new ConfigDataSourceObject("File", Core.HunterConfigSerialization.CheckFileNameWeight, config.CheckFileNameWeight.ToString(), "File names check weighting.", ConfigDataSourceObjectType.Float),

                // Meta Data Settings
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaCreator, config.CheckMetaCreator.ToString(), Core.Localization.ChecksLocalization.MetaCreatorDescription + " (Enabled?)"),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaCreatorWeight, config.CheckMetaCreatorWeight.ToString(), "Meta data creator check weighting.", ConfigDataSourceObjectType.Float),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaDateCreated, config.CheckMetaDateCreated.ToString(), Core.Localization.ChecksLocalization.MetaDateCreatedDescription + " (Enabled?)"),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaDateCreatedWeight, config.CheckMetaDateCreatedWeight.ToString(), "Meta data date created check weighting.", ConfigDataSourceObjectType.Float),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaDateLastPrinted, config.CheckMetaDateLastPrinted.ToString(), Core.Localization.ChecksLocalization.MetaDateLastPrintedDescription + " (Enabled?)"),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaDateLastPrintedWeight, config.CheckMetaDateLastPrintedWeight.ToString(), "Meta data date last printed check weighting.", ConfigDataSourceObjectType.Float),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaDateModified, config.CheckMetaDateModified.ToString(), Core.Localization.ChecksLocalization.MetaDateModifiedDescription + " (Enabled?)"),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaDateModifiedWeight, config.CheckMetaDateModifiedWeight.ToString(), "Meta data date last modified check weighting.", ConfigDataSourceObjectType.Float),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaLastModifiedBy, config.CheckMetaLastModifiedBy.ToString(), Core.Localization.ChecksLocalization.MetaLastModifiedByDescription + " (Enabled?)"),
                new ConfigDataSourceObject("Meta Data", Core.HunterConfigSerialization.CheckMetaLastModifiedByWeight, config.CheckMetaLastModifiedByWeight.ToString(), "Meta data last modified by check weighting.", ConfigDataSourceObjectType.Float),

                // Content Settings
                new ConfigDataSourceObject("Content", Core.HunterConfigSerialization.CheckContent, config.CheckContent.ToString(), Core.Localization.ChecksLocalization.ContentCheckDescription + " (Enabled?)"),
                new ConfigDataSourceObject("Content", Core.HunterConfigSerialization.CheckContentMaximumLength, config.CheckContentMaximumLength.ToString(), "The maximum length of content in a file to check using percent difference calculations.", ConfigDataSourceObjectType.Integer),
                new ConfigDataSourceObject("Content", Core.HunterConfigSerialization.CheckContentThreshold, config.CheckContentThreshold.ToString(), "Files with content greater than or equal to what percentage will raise a flag.", ConfigDataSourceObjectType.Float),
                new ConfigDataSourceObject("Content", Core.HunterConfigSerialization.CheckContentWeight, config.CheckContentWeight.ToString(), "Content check weighting.", ConfigDataSourceObjectType.Float),

                // Process Settings
                new ConfigDataSourceObject("Process", Core.HunterConfigSerialization.ProcessArchivesExtract, config.ProcessArchivesExtract.ToString(), "Should Galileo extract archives when evaluating the target folder?"),
                new ConfigDataSourceObject("Process", Core.HunterConfigSerialization.ProcessArchivesExtractOnlySubmissions, config.ProcessArchivesExtractOnlySubmissions.ToString(), "Should Galileo extract only submissions from archives when evaluating the target folder?"),
                new ConfigDataSourceObject("Process", Core.HunterConfigSerialization.ProcessArchivesDelete, config.ProcessArchivesDelete.ToString(), "Should Galileo delete archives once extracted?"),
                new ConfigDataSourceObject("Process", Core.HunterConfigSerialization.ProcessReportEmbedResources, config.ProcessReportEmbedResources.ToString(), "Should generated reports contain all resources needed to render? (No external links)"),
                new ConfigDataSourceObject("Process", Core.HunterConfigSerialization.ProcessNameIgnore, string.Join(", ", config.ProcessNameIgnore), "Ignored items when attempting to detect the person's name from a submission.", ConfigDataSourceObjectType.String),

                // Resources Settings
                new ConfigDataSourceObject("Platform", Core.HunterConfigSerialization.PlatformParallelismMaxDegrees, config.PlatformParallelismMaxDegrees.ToString(), "Max degrees of parallelism allowed [1-" + Environment.ProcessorCount + "].\n(-1 automatically uses maximum)", ConfigDataSourceObjectType.Integer)
            };

            return returnDataset;
        }
        
        public static Core.HunterConfig FromDataSourceObjects(System.Collections.Generic.List<ConfigDataSourceObject> items)
        {
            // Creates and defaults everything
            Core.HunterConfig returnConfig = new Core.HunterConfig();

            foreach (ConfigDataSourceObject i in items)
            {
                switch (i.Setting)
                {
                    // Shared Settings
                    case Core.HunterConfigSerialization.SharedIgnoredFolders:
                        returnConfig.SharedIgnoredFolders = Array.ConvertAll(i.Value.Split(','), p => p.Trim());
                        break;
                    case Core.HunterConfigSerialization.SharedIgnoredFiles:
                        returnConfig.SharedIgnoredFiles = Array.ConvertAll(i.Value.Split(','), p => p.Trim());
                        break;
                    case Core.HunterConfigSerialization.SharedIgnoredFileExtensions:
                        returnConfig.SharedIgnoredFileExtensions = Array.ConvertAll(i.Value.Split(','), p => p.Trim());
                        break;
                    case Core.HunterConfigSerialization.SharedIgnoredUsernames:
                        returnConfig.SharedIgnoredUsernames = Array.ConvertAll(i.Value.Split(','), p => p.Trim());
                        break;

                    // File Settings
                    case Core.HunterConfigSerialization.CheckFileName:
                        returnConfig.CheckFileName = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckFileNameThreshold:
                        returnConfig.CheckFileNameThreshold = float.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckFileNameWeight:
                        returnConfig.CheckFileNameWeight = float.Parse(i.Value);
                        break;

                    // Meta Data Settings
                    case Core.HunterConfigSerialization.CheckMetaCreator:
                        returnConfig.CheckMetaCreator = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaCreatorWeight:
                        returnConfig.CheckMetaCreatorWeight = float.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaDateCreated:
                        returnConfig.CheckMetaDateCreated = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaDateCreatedWeight:
                        returnConfig.CheckMetaDateCreatedWeight = float.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaDateLastPrinted:
                        returnConfig.CheckMetaDateLastPrinted = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaDateLastPrintedWeight:
                        returnConfig.CheckMetaDateLastPrintedWeight = float.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaDateModified:
                        returnConfig.CheckMetaDateModified = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaDateModifiedWeight:
                        returnConfig.CheckMetaDateModifiedWeight = float.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaLastModifiedBy:
                        returnConfig.CheckMetaLastModifiedBy = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckMetaLastModifiedByWeight:
                        returnConfig.CheckMetaLastModifiedByWeight = float.Parse(i.Value);
                        break;

                    // Content Settings
                    case Core.HunterConfigSerialization.CheckContent:
                        returnConfig.CheckContent = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckContentMaximumLength:
                        returnConfig.CheckContentMaximumLength = int.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckContentThreshold:
                        returnConfig.CheckContentThreshold = float.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.CheckContentWeight:
                        returnConfig.CheckContentWeight = float.Parse(i.Value);
                        break;

                    // Process Settings
                    case Core.HunterConfigSerialization.ProcessArchivesExtract:
                        returnConfig.ProcessArchivesExtract = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.ProcessArchivesExtractOnlySubmissions:
                        returnConfig.ProcessArchivesExtractOnlySubmissions = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.ProcessArchivesDelete:
                        returnConfig.ProcessArchivesDelete = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.ProcessReportEmbedResources:
                        returnConfig.ProcessReportEmbedResources = bool.Parse(i.Value);
                        break;
                    case Core.HunterConfigSerialization.ProcessNameIgnore:
                        returnConfig.ProcessNameIgnore = Array.ConvertAll(i.Value.Split(','), p => p.Trim());
                        break;

                    // Platform Settings
                    case Core.HunterConfigSerialization.PlatformParallelismMaxDegrees:
                        returnConfig.PlatformParallelismMaxDegrees = int.Parse(i.Value);
                        break;

                }
            }

            // Send it back
            return returnConfig;
        }

    }
}
