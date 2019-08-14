using System;
using System.IO;
using Galileo.Core.Checks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Adobe Acrobat File Processor
    /// </summary>
    class PDFFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Local document reference
        /// </summary>
        PdfReader _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.PDFFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal PDFFileProcessor(Submissions.Submission target) : base(target) { }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Galileo.Core.FileProcessors.PDFFileProcessor"/> is reclaimed by garbage collection
        /// </summary>
        ~PDFFileProcessor()
        {
            if (_document != null)
            {
                _document.Dispose();
            }
        }

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
                // Create our document
                _document = new PdfReader(_target.AbsolutePath);

                // Read meta data only if its not encrypted
                if (!_document.IsMetadataEncrypted())
                {
                    // Meta
                    if (_document.Info.ContainsKey("Author"))
                    {
                        _target.MetaCreator = _document.Info["Author"];
                    }

                    if (_document.Info.ContainsKey("CreationDate"))
                    {
                        DateTime.TryParse(_document.Info["CreationDate"], out _target.MetaDateCreated);
                    }

                    if (_document.Info.ContainsKey("ModDate"))
                    {
                        DateTime.TryParse(_document.Info["ModDate"], out _target.MetaDateModified);
                    }
                }


                // Content
                StringWriter output = new StringWriter();

                for (int i = 1; i <= _document.NumberOfPages; i++)
                {
                    output.WriteLine(PdfTextExtractor.GetTextFromPage(_document, i, new SimpleTextExtractionStrategy()));
                }

                _target.Content = output.ToString();

                // Cache length for later comparison
                _target.ContentLength = _target.Content.Length;
                _target.ContentHash = Compare.Hash(_target.Content);


                _loaded = true;
            }
            catch (Exception e)
            {
                _target.Logging.Add("- " + Markdown.Bold("An exception occured when processing " + _target.AbsolutePath + ", " + e.Message, true));
            }

            return _loaded;
        }

        #endregion
    }
}
