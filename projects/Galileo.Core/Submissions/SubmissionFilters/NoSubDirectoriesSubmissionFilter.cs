namespace Galileo.Core.Submissions.SubmissionFilters
{
	internal class NoSubDirectoriesSubmissionFilter : ISubmissionFilter
    {
        public bool Filter(SubmissionCandidate candidate)
        {
            return candidate.IsDirectory == true && (candidate.HasParent && candidate.Parent.IsDirectory);
        }
    }
}
