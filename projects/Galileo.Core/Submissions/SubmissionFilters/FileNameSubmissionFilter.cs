using System;
namespace Galileo.Core.Submissions.SubmissionFilters
{
    internal class FileNameSubmissionFilter : ISubmissionFilter
    {
        string[] _blackList;

		internal FileNameSubmissionFilter(params string[] blacklist)
        {
            _blackList = blacklist;
        }

        public bool Filter(SubmissionCandidate candidate)
        {
			return !candidate.IsDirectory && (Array.IndexOf(_blackList, candidate.FileName) != -1);
        }
    }
}