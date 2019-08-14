using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.Checks;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using System.Xml.Linq;
using System.Linq;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// OpenDocument Text File Processor
    /// </summary>
    class ODTFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Local text document reference
        /// </summary>
        TextDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.ODTFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal ODTFileProcessor(Submissions.Submission target) : base(target) { }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Galileo.Core.FileProcessors.ODTFileProcessor"/> is reclaimed by garbage collection
        /// </summary>
        ~ODTFileProcessor()
        {
            _document = null;
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


                var sb = new StringBuilder();
                _document = new TextDocument();
                _document.Load(_target.AbsolutePath);

                //The header and footer are in the DocumentStyles part. Grab the XML of this part
                XElement stylesPart = XElement.Parse(_document.DocumentStyles.Styles.OuterXml);
                //Take all headers and footers text, concatenated with return carriage
                string stylesText = string.Join("\r\n", stylesPart.Descendants().Where(x => x.Name.LocalName == "header" || x.Name.LocalName == "footer").Select(y => y.Value));

                //Main content
                var mainPart = _document.Content.Cast<IContent>();
                var mainText = String.Join("\r\n", mainPart.Select(x => x.Node.InnerText));

                //Append both text variables
                sb.Append(stylesText + "\r\n");
                sb.Append(mainText);

                _target.Content = sb.ToString();

                // Cache length for later comparison as well as generate a generic hash
                _target.ContentLength = _target.Content.Length;
                _target.ContentHash = Compare.Hash(_target.Content);

                // Assign Meta Data
                if (_document.DocumentMetadata.Creator != null)
                {
                    // Format treats them the same
                    _target.MetaCreator = _document.DocumentMetadata.Creator;
                    _target.MetaLastModifiedBy = _document.DocumentMetadata.Creator;
                    if (_target.MetaCreator == "\"\"")
                    {
                        _target.MetaCreator = string.Empty;
                        _target.MetaLastModifiedBy = string.Empty;
                    }
                }
                _target.MetaDateCreated = DateTime.Parse(_document.DocumentMetadata.CreationDate);
                _target.MetaDateModified = DateTime.Parse(_document.DocumentMetadata.LastModified);

                // Set loaded flag
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
