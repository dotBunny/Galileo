using System.Linq;
using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class MetaLastModifiedByCheck : BaseCheck, ICheck
    {
        internal MetaLastModifiedByCheck(Submission submission) : base(submission)
        {
            Flag = new Flag(submission.Config.CheckMetaLastModifiedByWeight); 
            _localReference = submission.MetaLastModifiedBy;
        }

        public override void Check(Submission other)
        {
            // Check if we're doing this check
            if (!_submission.Config.CheckMetaLastModifiedBy ||
                !other.Processor.GetCheckTypes().HasFlag(GetType())) return;

            // Evaluate check
            if (_submission.MetaLastModifiedBy != string.Empty &&
                !_submission.Config.SharedIgnoredUsernames.Contains(_submission.MetaLastModifiedBy))
            {
                // Check if the last modified by are the same, but also if the creator of the other document is a match
                if(_submission.MetaLastModifiedBy == other.MetaLastModifiedBy)
                {
                    Flag.AddSimilarSubmission(other, 1, other.MetaLastModifiedBy);
                }
                else if (_submission.MetaLastModifiedBy == other.MetaCreator)
                {
                    Flag.AddSimilarSubmission(other, 1, other.MetaCreator + "[Creator]");
                }
            }
        }

        public override string GetName() => Localization.ChecksLocalization.MetaLastModifiedByName;
        public override string GetDescription() => Localization.ChecksLocalization.MetaLastModifiedByDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.MetaLastModifiedBy;
    }
}