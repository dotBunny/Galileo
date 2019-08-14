using System;
using System.Collections.Generic;

namespace Galileo.Core.Search
{
    class Results
    {
        public List<Result> Items = new List<Result>();

        public class Result
        {
            public string ResultTitle;
            public int Rank;
            public string Description;
            public string URL;
        }

        public Results(string DOM)
        {
            // Convert DOM to results list
        }
    }
}
