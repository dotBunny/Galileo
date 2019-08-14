using System;
using System.IO;
using System.Linq;
using Galileo.Core.Checks;
using HtmlAgilityPack;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Hypertext Markup Language File Processor
    /// </summary>
    class HTMLFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.TXTFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal HTMLFileProcessor(Submissions.Submission target) : base(target) { }

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
                    CheckFactory.CheckType.MetaDateModified;
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
                var doc = new HtmlDocument();
                doc.LoadHtml(File.ReadAllText(_target.AbsolutePath));


                HtmlNode creatorNode = doc.DocumentNode.SelectSingleNode("//meta[@name='author']");
                if (creatorNode != null)
                {
                    _target.MetaCreator = creatorNode.Attributes["content"].Value;
                }


                DateTime tempDate;

                var metaModifiedNode = doc.DocumentNode.SelectSingleNode("//meta[@property='article:published_time']");
                if (metaModifiedNode != null)
                {
                    string time = metaModifiedNode.Attributes["content"].Value;
                    if (DateTime.TryParse(time, out tempDate))
                    {
                        _target.MetaDateCreated = tempDate;
                    }
                }

                var mdnode = doc.DocumentNode.SelectSingleNode("//meta[@property='article:modified_time']");
                if (mdnode != null)
                {
                    string time = mdnode.Attributes["content"].Value;
                    if (DateTime.TryParse(time, out tempDate))
                    {
                        _target.MetaDateModified = tempDate;
                    }
                }


                string content = doc.DocumentNode.SelectSingleNode("//body").InnerText;
                content = content.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(" ", string.Empty);
                _target.Content = content;
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