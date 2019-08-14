using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.Profiler
{
    class LogProfiler : IProfiler
    {
        Galileo.Core.Logging.ILog _log;
        
        public LogProfiler(Galileo.Core.Logging.ILog log)
        {
            _log = log;
        }

        public void SubmitDeltaEvent(string name, TimeSpan span)
        {
            _log.Add(string.Format("{0} at {1}", name, span));
        }

        public void SubmitDeltaEvent(string name, TimeSpan span, string text)
        {
            _log.Add(string.Format("{0} '{1}' at {2}", name, text, span));
        }

        public void SubmitTimeEvent(string name, DateTime time)
        {
            _log.Add(string.Format("{0} at {1}", name, time));
        }

        public void SubmitTimeEvent(string name, DateTime time, string text)
        {
            _log.Add(string.Format("{0} '{1}' at {2}", name, text, time));
        }
    }
}
