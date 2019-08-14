namespace Galileo.Core.Submissions.SubmissionFilters
{
	internal class NoDirectoriesSubmissionFilter : ISubmissionFilter
    {
        public bool Filter(SubmissionCandidate candidate)
        {
            return candidate.IsDirectory == true;
        }
    }
}
