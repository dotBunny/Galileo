using System;
using System.Threading;
using System.IO;

namespace Galileo.Core.Logging
{


    internal class Log : ILog
    {

        public static readonly LogInternal Session = new LogInternal();

        public LogEventHandler OnLogEvent;
        public string Path { get; private set; }
        public bool Echo { get; set; }

        LogWriter _output;
        Timer _periodicWriter;
        bool _shouldCache = true;
        bool _capturing = false;

        ~Log()
        {
            _periodicWriter?.Dispose();
            _output?.Dispose();
            _periodicWriter = null;
            _output = null;
        }

        public void FlushCache()
        {
            if (_output != null)
            {
                _output.WriteCache();
            }
        }
        public void Setup(string path, bool cache = true)
        {
            Path = path;
            _shouldCache = cache;
        }

        public void Capture()
        {
            Capture(Path, _shouldCache);
        }

        public void Capture(string path, bool cache = true)
        {
            if (_capturing) return;

            // Determine if we need to move old log files out of the way
            Path = path;
            _shouldCache = cache;

            // The writer its self knows to not append
            _output = new LogWriter(
                Path,
                Console.OutputEncoding,
                Console.Out)
            {
                UseCache = cache
            };

            // Setup safety thread
            _periodicWriter = new Timer(
                e => FlushCache(),
                null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            _capturing = true;
        }

        public void Add(string content, bool newLine = true)
        {
            // The "  " is for markdown
            if (newLine)
            {
                string output = content + "  " + Platform.EndOfLine();
                _output?.Write(output);

                // Strip output for usage
                OnLogEvent?.Invoke(output);
            }
            else
            {
                _output?.Write(content);
                OnLogEvent?.Invoke(content);
            }
        }

        public void AddLogEventHandler(LogEventHandler handler)
        {
            OnLogEvent += handler;
        }
    }


}
