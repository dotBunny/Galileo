using System;
namespace Galileo.Core.Logging
{
	public delegate void LogEventHandler(string line);
       
	internal interface ILog
    {
        void FlushCache();
        void Setup(string path, bool cache = true);
        void Capture();
        void Capture(string path, bool cache = true);
        void Add(string content, bool newLine = true);
        void AddLogEventHandler(LogEventHandler handler);
    }
}
