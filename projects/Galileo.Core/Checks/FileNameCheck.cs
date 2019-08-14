using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class FileNameCheck : BaseCheck, ICheck
    {
        internal FileNameCheck(Submission submission) : base(submission)
        {
            Flag = new Flag(submission.Config.CheckFileNameWeight);
            _localReference = submission.FileName;
        }

        public override void Check(Submission other)
        {
            // Check if we're doing this check
            if (!_submission.Config.CheckFileName || 
                !other.Processor.GetCheckTypes().HasFlag(GetType())) return;

            // Check if the other has the same capability
//            other.Processor.GetCheckTypes()

            // TODO : Remove copy parts (1) / copy
            // TODO: 2018.2

            // Calculate difference
            double fileNameComparison = Compare.CalculateSimilarity(_submission.FileName, other.FileName);

            // Evaluate check
            if (fileNameComparison > _submission.Config.CheckFileNameThreshold)
            {
                Flag.AddSimilarSubmission(other, fileNameComparison, other.FileName);
            }
        }

        public override string GetKBLink()
        {
            return Website.KB + "check/file-name/";
        }
        public override string GetName() => Localization.ChecksLocalization.FileNameName;
        public override string GetDescription() => Localization.ChecksLocalization.FileNameCheckDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.FileName;
    }
}