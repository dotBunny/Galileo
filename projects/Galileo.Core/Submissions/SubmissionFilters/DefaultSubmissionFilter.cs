using System;
namespace Galileo.Core.Submissions.SubmissionFilters
{
	/// <summary>
    /// Galileo Default Filters
    /// </summary>
	internal class DefaultSubmissionFilter : ISubmissionFilter
    {
		/// <summary>
        /// Filter out specific files from valid candidates
        /// </summary>
        /// <returns>Shoudl be filtered out?</returns>
        /// <param name="candidate">The target candidate.</param>
		public bool Filter(SubmissionCandidate candidate)
		{
			// Ignore Galileo's data directory
			if (candidate.IsDirectory)
			{
				if (candidate.FileName == HunterConfig.GalileoDefaultDataFolder)
				{
					return true;
				}

				switch (candidate.FileName.ToLower())
				{
					case ".git":
					case ".svn":
						return true;
				}
			}
			else 
			{
			    if(candidate.FileName == ".DS_Store")
				{
					return true;
				}
			}
                     
			return false;
		}
	}
}
