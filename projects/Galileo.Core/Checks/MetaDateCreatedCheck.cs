using System;
using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class MetaDateCreatedCheck : BaseCheck, ICheck
    {
        internal MetaDateCreatedCheck(Submission submission) : base(submission)
        {
            Flag = new Flag(submission.Config.CheckMetaDateCreatedWeight); 
            _localReference = submission.MetaDateCreated.ToString();
        }

        public override void Check(Submission other)
        {
            // Check if we're doing this check
            if (!_submission.Config.CheckMetaDateCreated ||
                !other.Processor.GetCheckTypes().HasFlag(GetType())) return;

            // Evaluate check
            if ( _submission.MetaDateCreated != DateTime.MinValue &&
               _submission.MetaDateCreated == other.MetaDateCreated)
            {
                Flag.AddSimilarSubmission(other, 1, other.MetaDateCreated.ToString());
            }       
        }

        public override string GetName() => Localization.ChecksLocalization.MetaDateCreatedName;
        public override string GetDescription() => Localization.ChecksLocalization.MetaDateCreatedDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.MetaDateCreated;
    }
}
