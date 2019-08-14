namespace Galileo.Core.Submissions.SubmissionFilters
{
	internal class NoFilesSubmissionFilter : ISubmissionFilter
    {
        public bool Filter(SubmissionCandidate candidate)
        {
            return candidate.IsFile == true;
        }
    }
}
