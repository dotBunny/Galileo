using System;
using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class MetaDateModifiedCheck : BaseCheck, ICheck
    {
        internal MetaDateModifiedCheck(Submission submission) : base(submission)
        {
            Flag = new Flag(submission.Config.CheckMetaDateModifiedWeight);
            _localReference = submission.MetaDateModified.ToString();
        }

        public override void Check(Submission other)
        {
            // Check if we're doing this check
            if (!_submission.Config.CheckMetaDateModified ||
                !other.Processor.GetCheckTypes().HasFlag(GetType())) return;

            // Evaluate check
            if (_submission.MetaDateModified != DateTime.MinValue &&
                _submission.MetaDateModified == other.MetaDateModified)
            {
                Flag.AddSimilarSubmission(other, 1, other.MetaDateModified.ToString());
            }
        }

        public override string GetName() => Localization.ChecksLocalization.MetaDateModifiedName;
        public override string GetDescription() => Localization.ChecksLocalization.MetaDateModifiedDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.MetaDateModified;
    }
}