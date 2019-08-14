namespace Galileo.Core.Report
{
    internal class FlagItem
    {
        public double Similarity { get; set; }
        public string Reference { get; set; }

        internal string GetPercentage()
        {
            return (Similarity * 100).ToString("N0") + "%";
        }
    }
}
