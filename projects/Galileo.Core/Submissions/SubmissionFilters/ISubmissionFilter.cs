namespace Galileo.Core.Submissions.SubmissionFilters
{
	internal interface ISubmissionFilter
    {
        bool Filter(SubmissionCandidate candidate);
    }
}
