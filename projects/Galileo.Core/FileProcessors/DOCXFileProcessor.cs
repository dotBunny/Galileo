using System;
using System.Text;
using DocumentFormat.OpenXml; 
using DocumentFormat.OpenXml.Packaging;
using Galileo.Core.Checks;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Microsoft Word File Processor
    /// </summary>
    class DOCXFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Local document reference
        /// </summary>
        WordprocessingDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.DOCXFileProcessor"/> class.
        /// </summary>
        /// <param name="target">The submission</param>
        internal DOCXFileProcessor(Submissions.Submission target) : base(target) { }

        #region Methods

        /// <summary>
        /// Gets the valid checks to process through this file
        /// </summary>
        /// <returns>The check types</returns>
        public override CheckFactory.CheckType GetCheckTypes()
        {
            return CheckFactory.CheckType.ContentHash |
                   CheckFactory.CheckType.Content |
                   CheckFactory.CheckType.FileName |
                   CheckFactory.CheckType.MetaCreator |
                   CheckFactory.CheckType.MetaDateCreated |
                   CheckFactory.CheckType.MetaDateLastPrinted |
                   CheckFactory.CheckType.MetaDateModified |
                   CheckFactory.CheckType.MetaLastModifiedBy;
        }

        /// <summary>
        /// Process the submissions absolute path
        /// </summary>
        /// <returns>Was the submission able to be loaded?</returns>
        public override bool Process()
        {
            _loaded = false;
            try
            {
                _document = WordprocessingDocument.Open(_target.AbsolutePath, false);

                // Build Plain Text Version
                OpenXmlElement element = _document.MainDocumentPart.Document.Body;
                if (element != null)
                {
                    // Get plain text of content
                    _target.Content = GetPlainText(element);

                    // Cache length for later comparison
                    _target.ContentLength = _target.Content.Length;

                    _target.ContentHash = Core.Compare.Hash(_target.Content);
                }


                // Collect Meta Data
                _target.MetaCreator = _document.PackageProperties.Creator;
                _target.MetaLastModifiedBy = _document.PackageProperties.LastModifiedBy;

                if (_document.PackageProperties.LastPrinted != null)
                {
                    _target.MetaDateLastPrinted = (DateTime)_document.PackageProperties.LastPrinted;
                }

                if (_document.PackageProperties.Created != null)
                {
                    _target.MetaDateCreated = (DateTime)_document.PackageProperties.Created;
                }

                if (_document.PackageProperties.Modified != null)
                {
                    _target.MetaDateModified = (DateTime)_document.PackageProperties.Modified;
                }

                _loaded = true;
            }
            catch (Exception e)
            {
                _target.Logging.Add("- " + Markdown.Bold("An exception occured when processing " + _target.AbsolutePath + ", " + e.Message, true));
            }
            return _loaded;
        }

        /// <summary> 
        /// Read Plain Text in all XmlElements of word document 
        /// </summary> 
        /// <param name="element">XmlElement in document</param> 
        /// <returns>Plain Text in XmlElement</returns> 
        string GetPlainText(OpenXmlElement element)
        {
            StringBuilder PlainTextInWord = new StringBuilder();
            foreach (OpenXmlElement section in element.Elements())
            {
                switch (section.LocalName)
                {
                    // Text 
                    case "t":
                        PlainTextInWord.Append(section.InnerText);
                        break;


                    case "cr":                          // Carriage return 
                    case "br":                          // Page break 
                        PlainTextInWord.Append(Environment.NewLine);
                        break;


                    // Tab 
                    case "tab":
                        PlainTextInWord.Append("\t");
                        break;


                    // Paragraph 
                    case "p":
                        PlainTextInWord.Append(GetPlainText(section));
                        PlainTextInWord.AppendLine(Environment.NewLine);
                        break;


                    default:
                        PlainTextInWord.Append(GetPlainText(section));
                        break;
                }
            }
            return PlainTextInWord.ToString();
        }

        #endregion
    }
}
