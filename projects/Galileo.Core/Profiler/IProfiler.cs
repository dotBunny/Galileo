using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.Profiler
{
    interface IProfiler
    {
        void SubmitTimeEvent(string name, DateTime time);
        void SubmitTimeEvent(string name, DateTime time, string text);
        void SubmitDeltaEvent(string name, TimeSpan span);
        void SubmitDeltaEvent(string name, TimeSpan span, string text);
    }
}
