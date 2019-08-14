using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class ContentHashCheck : BaseCheck, ICheck
    {
        internal ContentHashCheck(Submission submission) : base(submission) 
        {
            Flag = new Flag(1f); // We dont allow hash check to have anything but 1!
            _localReference = submission.ContentHash;
        }

        public override void Check(Submission other)
        {

            if (!other.Processor.GetCheckTypes().HasFlag(GetType())) return;
                
            if ( _submission.ContentHash != string.Empty && _submission.ContentHash == other.ContentHash)
            {
                Flag.AddSimilarSubmission(other, 1, other.ContentHash);
            }
        }

        public override string GetKBLink()
        {
            return Website.KB + "check/content-hash/";
        }
        public override string GetName() => Localization.ChecksLocalization.ContentHashName;
        public override string GetDescription() => Localization.ChecksLocalization.ContentHashDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.ContentHash;
    }
}
