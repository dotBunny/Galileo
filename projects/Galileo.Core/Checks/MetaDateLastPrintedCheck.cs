using System;
using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class MetaDateLastPrintedCheck : BaseCheck, ICheck
    {
        internal MetaDateLastPrintedCheck(Submission submission) : base(submission)
        {
            Flag = new Flag(submission.Config.CheckMetaDateLastPrintedWeight);
            _localReference = submission.MetaDateLastPrinted.ToString();
        }

        public override void Check(Submission other)
        {
            // Check if we're doing this check
            if (!_submission.Config.CheckMetaDateLastPrinted ||
                !other.Processor.GetCheckTypes().HasFlag(GetType())) return;

            // Evaluate check
            if (_submission.MetaDateLastPrinted != DateTime.MinValue &&
                _submission.MetaDateLastPrinted == other.MetaDateLastPrinted)
            {
                Flag.AddSimilarSubmission(other, 1, other.MetaDateLastPrinted.ToString());
            }
        }

        public override string GetName() => Localization.ChecksLocalization.MetaDateLastPrintedName;
        public override string GetDescription() => Localization.ChecksLocalization.MetaDateLastPrintedDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.MetaDateLastPrinted;
    }
}