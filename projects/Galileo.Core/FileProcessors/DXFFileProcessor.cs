using System;
using System.IO;
using System.Text;
using Galileo.Core.Checks;
using IxMilia.Dxf;
using IxMilia.Dxf.Blocks;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// AutoCAD DXF File Processor
    /// </summary>
    class DXFFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Local document reference
        /// </summary>
        DxfFile _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.DXFFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal DXFFileProcessor(Submissions.Submission target):base(target) { }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Galileo.Core.FileProcessors.DXFFileProcessor"/> is reclaimed by garbage collection
        /// </summary>
        ~DXFFileProcessor()
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
                FileStream fs = new FileStream(_target.AbsolutePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _document = DxfFile.Load(fs);

                // Build meta data
                _target.MetaDateCreated = _document.Header.CreationDate;
                _target.MetaDateModified = _document.Header.UpdateDate;
                _target.MetaLastModifiedBy = _document.Header.LastSavedBy;

                // Get content
                // Document.Blocks[0].

                StringBuilder builder = new StringBuilder();
                foreach (DxfBlock b in _document.Blocks)
                {
                    if (!b.IsAnonymous)
                    {
                        builder.Append(b.Name);
                    }
                }
                _target.Content = builder.ToString();
                _target.ContentLength = _target.Content.Length;
                _target.ContentHash = Compare.Hash(_target.Content);

                fs.Dispose();

                _loaded = true;
            }
            catch (Exception e)
            {
                _target.Logging.Add("- " + Markdown.Bold("An exception occured when processing " + _target.AbsolutePath + ", " + e.Message));
            }

            return _loaded;
        }

        #endregion
    }
}