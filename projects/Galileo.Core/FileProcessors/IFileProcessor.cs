namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// File Processor Interface
    /// </summary>
    interface IFileProcessor
    {
        /// <summary>
        /// Gets the valid checks to process through this file
        /// </summary>
        /// <returns>The check types</returns>
        Checks.CheckFactory.CheckType GetCheckTypes();

        /// <summary>
        /// Was the file processed?
        /// </summary>
        /// <returns><c>true</c>, if processed successfully, <c>false</c> otherwise.</returns>
        bool IsProcessed();

        /// <summary>
        /// Process the submissions absolute path
        /// </summary>
        /// <returns>Was the submission able to be loaded?</returns>
        bool Process();
    }
}