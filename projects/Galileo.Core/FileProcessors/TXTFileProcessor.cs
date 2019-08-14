using System;
using System.IO;
using Galileo.Core.Checks;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Plain Text File Processor
    /// </summary>
    class TXTFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.TXTFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal TXTFileProcessor(Submissions.Submission target) : base(target) { } 

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
                _target.Content = File.ReadAllText(_target.AbsolutePath);
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