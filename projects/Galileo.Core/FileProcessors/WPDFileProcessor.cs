using System;
using System.Text;
using Galileo.Core.Checks;
using Galileo.Vendor.Corel;

namespace Galileo.Core.FileProcessors
{
    
    /// <summary>
    /// Corel WordPerfect File Processor
    /// </summary>
    class WPDFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.WPDFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal WPDFileProcessor(Submissions.Submission target) : base(target) { }

        #region Methods

        /// <summary>
        /// Gets the valid checks to process through this file
        /// </summary>
        /// <returns>The check types</returns>
        public override CheckFactory.CheckType GetCheckTypes()
        {
            return CheckFactory.CheckType.ContentHash |
                   CheckFactory.CheckType.Content |
                   CheckFactory.CheckType.FileName;
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
                WP6Document doc = new WP6Document(_target.AbsolutePath);

                // Build document content
                StringBuilder contentBuilder = new StringBuilder();
                foreach(WPToken t in doc.documentArea.WPStream)
                {
                    Type tokenType = t.GetType();
                    if ( tokenType == typeof(Character))
                    {
                        Character character = (Character)t;
                        contentBuilder.Append(character.content);

                    }
                   
                }
                _target.Content = contentBuilder.ToString();
                _target.ContentLength = _target.Content.Length;
                _target.ContentHash = Compare.Hash(_target.Content);




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