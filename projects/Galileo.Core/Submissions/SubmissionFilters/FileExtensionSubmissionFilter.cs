using System;
using Galileo.Core.FileTypes;

namespace Galileo.Core.Submissions.SubmissionFilters
{
    internal class FileExtensionSubmissionFilter : ISubmissionFilter
    {
        string[] _blackList;
        
		internal FileExtensionSubmissionFilter(params string[] extensions)
        {
			_blackList = extensions;
        }

        public bool Filter(SubmissionCandidate candidate)
        {
            return Array.IndexOf(_blackList, candidate.FileExtension) != -1;
        }
    }
}
