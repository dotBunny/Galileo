using System.Linq;
using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class MetaCreatorCheck : BaseCheck, ICheck
    {
        internal MetaCreatorCheck(Submission submission) : base(submission)
        {
            Flag = new Flag(_submission.Config.CheckMetaCreatorWeight);
            _localReference = submission.MetaCreator;
        }

        public override void Check(Submission other)
        {
            // Check if we're doing this check
            if (!_submission.Config.CheckMetaCreator || 
                !other.Processor.GetCheckTypes().HasFlag(GetType())) return;

            // Evaluate check
            if (_submission.MetaCreator != string.Empty &&
                !_submission.Config.SharedIgnoredUsernames.Contains(_submission.MetaCreator) &&
                _submission.MetaCreator == other.MetaCreator)
            {
                Flag.AddSimilarSubmission(other, 1, other.MetaCreator);
            }
        }

        public override string GetKBLink()
        {
            return Website.KB + "check/meta-creator/";
        }
        public override string GetName() => Localization.ChecksLocalization.MetaCreatorName;
        public override string GetDescription() => Localization.ChecksLocalization.MetaCreatorDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.MetaCreator;
    }
}