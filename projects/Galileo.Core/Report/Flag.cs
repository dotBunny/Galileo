using System.Collections.Generic;
using Galileo.Core.Submissions;

namespace Galileo.Core.Report
{
    internal class Flag
    {
        public bool Check
        {
            get
            {
                if (Similar.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Flag(float Weighting = 1f)
        {
            Weight = Weighting;
        }

        public Dictionary<Submission, FlagItem> Similar = new Dictionary<Submission, FlagItem>();
        public float Weight = 1f;


        public void AddSimilarSubmission(Submission submission, double similarity, string reference)
        {
            if (!Similar.ContainsKey(submission))
            {
                Similar.Add(submission, new FlagItem { Similarity = similarity, Reference = reference});
            }
            else if ( Similar[submission].Similarity < similarity )
            {
                Similar[submission] = new FlagItem { Similarity = similarity, Reference = reference };
            }
        }
    }
}
