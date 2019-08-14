using System;
using Galileo.Core.FileTypes;

namespace Galileo.Core.Submissions.SubmissionFilters
{
    internal class FileTypeSubmissionFilter : ISubmissionFilter
    {
        FileType[] _blackList;

		FileTypeSubmissionFilter(params FileType[] blackList)
        {
            _blackList = blackList;
        }

        public bool Filter(SubmissionCandidate candidate)
        {
            return Array.IndexOf(_blackList, candidate.FileType) != -1;
        }
    }
}
