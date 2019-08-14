using System;
namespace Galileo.Core.Submissions.SubmissionFilters
{
	internal class DirectoryNameSubmissionFilter : ISubmissionFilter
    {
        string[] _blackList;

		internal DirectoryNameSubmissionFilter(params string[] blacklist)
        {
			_blackList = blacklist;
        }

        public bool Filter(SubmissionCandidate candidate)
        {
			return candidate.IsDirectory && (Array.IndexOf(_blackList, candidate.FileName) != -1);
        }
    }
}