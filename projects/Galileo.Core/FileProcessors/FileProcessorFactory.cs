using System;
namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// File Processor Factory
    /// </summary>
    static class FileProcessorFactory
    {
        /// <summary>
        /// Supported file extensions
        /// </summary>
        public static readonly string[] ExtensionList = { "docx", "xlsx", "xlsm", "pdf", "odt", "dxf", "txt", "rtf", "pptx", "html", "wpd" }; 
      
        /// <summary>
        ///  Supported file types
        /// </summary>
        public enum FileProcessorType
        {
            /// <summary>
            ///Unknown File Type
            /// </summary>
            Unknown,

            /// <summary>
            /// Ignored File Type
            /// </summary>
            Ignored,

            /// <summary>
            /// Word Document 2016+
            /// </summary>
            DOCX,

            /// <summary>
            /// Excel Spreadsheet 2016+ (includes macro files)
            /// </summary>
            XLSX,

            /// <summary>
            /// Acrobat PDF
            /// </summary>
            PDF,

            /// <summary>
            /// Open Office Document
            /// </summary>
            ODT,

            /// <summary>
            /// CAD Interchange Format
            /// </summary>
            DXF,

            /// <summary>
            /// Text files
            /// </summary>
            TXT,

            /// <summary>
            /// Rich Text Format
            /// </summary>
            RTF,

            /// <summary>
            /// PowerPoint Presentation
            /// </summary>
            PPTX,

            /// <summary>
            /// Hypertext Markup Language
            /// </summary>
            HTML,

            /// <summary>
            /// Corel WordPerfect
            /// </summary>
            WPD
        }

        /// <summary>
        /// Create the appropriate <see cref="Galileo.Core.FileProcessors.IFileProcessor"/> for the submission
        /// </summary>
        /// <returns>The file processor</returns>
        /// <param name="submission">Submission Reference</param>
        public static IFileProcessor CreateFileProcessor(Submissions.Submission submission)
        {
            // Figure out the processor
            switch (submission.Type)
            {
                case FileProcessorType.DOCX:
                    return new DOCXFileProcessor(submission);
                case FileProcessorType.XLSX:
                    return new XLSXFileProcessor(submission);
                case FileProcessorType.PDF:
                    return new PDFFileProcessor(submission);
                case FileProcessorType.ODT:
                    return new ODTFileProcessor(submission);
                case FileProcessorType.DXF:
                    return new DXFFileProcessor(submission);
                case FileProcessorType.TXT:
                    return new TXTFileProcessor(submission);
                case FileProcessorType.RTF:
                    return new RTFFileProcessor(submission);
                case FileProcessorType.PPTX:
                    return new PPTXFileProcessor(submission);
                case FileProcessorType.HTML:
                    return new HTMLFileProcessor(submission);
                case FileProcessorType.WPD:
                    return new WPDFileProcessor(submission);
                default:
                    return new DefaultFileProcessor(submission);
            }
        }

        /// <summary>
        /// Gets the file processor type from the submission extenson
        /// </summary>
        /// <returns>The file processor type</returns>
        /// <param name="extension">The file extensions</param>
        public static FileProcessorType GetFileProcessorType(string extension)
        {
            extension = extension.ToLower();
            if (extension.StartsWith(".", StringComparison.Ordinal))
            {
                extension = extension.Substring(1);
            }

            switch(extension)
            {
                case "docx":
                    return FileProcessorType.DOCX;
                case "xlsx":
                case "xlsm":
                    return FileProcessorType.XLSX;
                case "pdf":
                    return FileProcessorType.PDF;
                case "odt":
                    return FileProcessorType.ODT;
                case "dxf":
                    return FileProcessorType.DXF;
                case "txt":
                    return FileProcessorType.TXT;
                case "rtf":
                    return FileProcessorType.RTF;
                case "pptx":
                    return FileProcessorType.PPTX;
                case "html":
                    return FileProcessorType.HTML;
                case "wpd":
                    return FileProcessorType.WPD;
                    
            }

            return FileProcessorType.Unknown;
        }
    }
}
