using System;
namespace Galileo.Core.Logging
{
	internal class NullLog : ILog
    {
        public void Add(string content, bool newLine)
        {
        }

        public void Setup(string path, bool cache)
        {
            
        }
        public void Capture()
        {
            
        }
        public void Capture(string path, bool cache)
        {
        }

        public void FlushCache()
        {
        }

        public void AddLogEventHandler(LogEventHandler handler)
        {
        }
    }

}
