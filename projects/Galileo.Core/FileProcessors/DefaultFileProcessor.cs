using Galileo.Core.Checks;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Default File Processor
    /// </summary>
    class DefaultFileProcessor : IFileProcessor
    {
        #region Fields

        /// <summary>
        /// Was the document successfully loaded?
        /// </summary>
        internal bool _loaded;

        /// <summary>
        /// The submission reference
        /// </summary>
        internal Submissions.Submission _target;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.DefaultFileProcessor"/> class.
        /// </summary>
        /// <param name="target">Target.</param>
        internal DefaultFileProcessor(Submissions.Submission target)
        {
            _target = target;
        }

        /// <summary>
        /// Gets the valid checks to process through this file
        /// </summary>
        /// <returns>The check types</returns>
        public virtual CheckFactory.CheckType GetCheckTypes()
        {
            return CheckFactory.CheckType.None;
        }

        /// <summary>
        /// Was the file processed?
        /// </summary>
        /// <returns><c>true</c>, if processed successfully, <c>false</c> otherwise.</returns>
        public virtual bool IsProcessed()
        {
            return _loaded;
        }

        /// <summary>
        /// Process the submissions absolute path
        /// </summary>
        /// <returns>Was the submission able to be loaded?</returns>
        public virtual bool Process()
        {
            _loaded = false;
            return false;
        }
    }
}
