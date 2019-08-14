using System;
using System.Text;
using Galileo.Core.Localization;

namespace Galileo.Core.Report.HTML.Sections
{
    class SettingsSection :BaseHTML
    {
        internal SettingsSection(HunterConfig config)
        {
         
            StringBuilder layout = new StringBuilder();

            layout.Append(CreateHeader());

            // !!!! Need to create rows for every two sections !!!

            // Row 1
            layout.Append(CreateRowHeader());


            // Shared Settings
            layout.Append(CreateTableHeader(SettingsLocalization.SharedSettingsTitle));
            layout.Append(CreateTableItem(HunterConfigSerialization.SharedIgnoredFolders, config.SharedIgnoredFolders.GetString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.SharedIgnoredFiles, config.SharedIgnoredFiles.GetString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.SharedIgnoredFileExtensions, config.SharedIgnoredFileExtensions.GetString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.SharedIgnoredUsernames, config.SharedIgnoredUsernames.GetString()));
            layout.Append(CreateTableFooter());

            // File Settings
            layout.Append(CreateTableHeader(SettingsLocalization.FileSettingsTitle));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckFileName, config.CheckFileName.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckFileNameThreshold, config.CheckFileNameThreshold.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckFileNameWeight, config.CheckFileNameWeight.ToString()));
            layout.Append(CreateTableFooter());

            layout.Append(CreateRowFooter());

            // Row 2
            layout.Append(CreateRowHeader());

            // Meta Data Settings
            layout.Append(CreateTableHeader(SettingsLocalization.MetaDataSettingsTitle));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaCreator, config.CheckMetaCreator.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaCreatorWeight, config.CheckMetaCreatorWeight.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaDateCreated, config.CheckMetaDateCreated.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaDateCreatedWeight, config.CheckMetaDateCreatedWeight.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaDateLastPrinted, config.CheckMetaDateLastPrinted.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaDateLastPrintedWeight, config.CheckMetaDateLastPrintedWeight.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaDateModified, config.CheckMetaDateModified.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaDateModifiedWeight, config.CheckMetaDateModifiedWeight.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaLastModifiedBy, config.CheckMetaLastModifiedBy.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckMetaLastModifiedByWeight, config.CheckMetaLastModifiedByWeight.ToString()));
            layout.Append(CreateTableFooter());

            // Content Settings
            layout.Append(CreateTableHeader(SettingsLocalization.ContentSettingsTitle));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckContent, config.CheckContent.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckContentThreshold, config.CheckContentThreshold.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckContentMaximumLength, config.CheckContentMaximumLength.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.CheckContentWeight, config.CheckContentWeight.ToString()));
            layout.Append(CreateTableFooter());

            layout.Append(CreateRowFooter());

            // Row 3
            layout.Append(CreateRowHeader());

            // Process Settings
            layout.Append(CreateTableHeader(SettingsLocalization.ProcessSettingsTitle));
            layout.Append(CreateTableItem(HunterConfigSerialization.ProcessArchivesExtract, config.ProcessArchivesExtract.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.ProcessArchivesExtractOnlySubmissions, config.ProcessArchivesExtractOnlySubmissions.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.ProcessArchivesDelete, config.ProcessArchivesDelete.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.ProcessReportEmbedResources, config.ProcessReportEmbedResources.ToString()));
            layout.Append(CreateTableItem(HunterConfigSerialization.ProcessNameIgnore, config.ProcessNameIgnore.GetString()));
            layout.Append(CreateTableFooter());

            layout.Append(CreateRowFooter());

            layout.Append(CreateFooter());

            _cache = layout.ToString();
        }

        string CreateHeader()
        {
             return "<div id=\"settings\" class=\"container\" style=\"padding-bottom: 30px;\">" +
                        "<h1>" + SettingsLocalization.Header + "</h1>" +
                        "<p>" + SettingsLocalization.Description + "</p>" +
                        "<div class=\"container\">";
        }
        string CreateRowHeader()
        {
            return "<div class=\"row\">";
        }
        string CreateTableHeader(string title)
        {
            return "<div class=\"col-lg-6\" style=\"padding-top:20px;\">" +
                        "<h4>" + title + "</h4>" +
                        "<table class=\"table table-bordered table-striped\" style=\"background-color: #fff\">" +
                            "<thead>" +
                                "<tr>" +
                                    "<th>" + SharedLocalization.Setting + "</th><th>" + SharedLocalization.Value + "</th>" +
                                "</tr>" +
                            "</thead>" +
                            "<tbody>";
        }
        string CreateTableItem(string setting, string settingValue)
        {
                        return "<tr>" +
                                    "<td>" + setting + "</td>" +
                                    "<td>" + settingValue + "</td>" +
                                "</tr>";
        }
        string CreateTableFooter()
        {
            return          "</tbody>" +
                        "</table>" +
                    "</div>";
        }
        string CreateRowFooter()
        {
            return "</div>";
        } 
        string CreateFooter()
        {
            return "</div></div>";
        }
    }
}