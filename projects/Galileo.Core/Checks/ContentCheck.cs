using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    class ContentCheck : BaseCheck, ICheck
    {
        public ContentCheck(Submission submission) : base(submission) 
        {
            Flag = new Flag(submission.Config.CheckContentWeight);
            _localReference = "<em>File Content</em>";
        }

        public override void Check(Submission other)
        {
            // Check if this is disabled
            if (!_submission.Config.CheckContent ||
                !other.Processor.GetCheckTypes().HasFlag(GetType()))
            {
                return;
            }

            // Create local copies of content to avoid colllisons, and trim it
            string originalContent = _submission.Content;
            if (_submission.ContentLength > _submission.Config.CheckContentMaximumLength)
            {
                originalContent = originalContent.Substring(0, _submission.Config.CheckContentMaximumLength);
            }

            string otherContent = other.Content;
            if (other.ContentLength > _submission.Config.CheckContentMaximumLength)
            {
                otherContent = otherContent.Substring(0, _submission.Config.CheckContentMaximumLength);
            }


            double contentComparison = Compare.CalculateSimilarity(originalContent, otherContent);

            if (contentComparison >= _submission.Config.CheckContentThreshold)
            {
                Flag.AddSimilarSubmission(other, contentComparison, string.Empty);
            } 
        }

        public override string GetKBLink()
        {
            return Website.KB + "check/content/";
        }
        public override string GetName() => Localization.ChecksLocalization.ContentCheckName;
        public override string GetDescription() => Localization.ChecksLocalization.ContentCheckDescription;
        public override CheckFactory.CheckType GetType() => CheckFactory.CheckType.Content;
    }
}