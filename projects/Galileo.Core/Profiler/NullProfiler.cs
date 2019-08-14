using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.Profiler
{
    class NullProfiler : IProfiler
    {
        public void SubmitDeltaEvent(string name, TimeSpan span)
        {
        }

        public void SubmitDeltaEvent(string name, TimeSpan span, string text)
        {
        }

        public void SubmitTimeEvent(string name, DateTime time)
        {
        }

        public void SubmitTimeEvent(string name, DateTime time, string text)
        {
        }
    }
}
