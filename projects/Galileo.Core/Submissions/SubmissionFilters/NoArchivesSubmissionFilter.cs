namespace Galileo.Core.Submissions.SubmissionFilters
{
	internal class NoArchivesSubmissionFilter : ISubmissionFilter
    {
        public bool Filter(SubmissionCandidate candidate)
        {
            return candidate.IsArchive == true;
        }
    }
}
