using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Galileo.Core.Logging
{
    class LogWriter : TextWriter
    {
        public int CacheLimit = 25;
        public bool UseCache;

        ConcurrentQueue<string> CachedData = new ConcurrentQueue<string>();
        Encoding CurrentEncoding = Encoding.UTF8;
        readonly TextWriter ConsoleOutput;
        StreamWriter Writer;

        public LogWriter(string path, Encoding encoding, TextWriter standardOutput)
        {
            CurrentEncoding = encoding;
            ConsoleOutput = standardOutput;

            // Create our writer
            Writer = new StreamWriter(
                Path.Combine(path),
                false,
                encoding);
        }

        ~LogWriter()
        {
            if (Writer != null)
            {
                //Writer.Flush();
                //Writer.Close();
                try
                {
                    Writer.Dispose();
                    Writer = null;
                }
                catch(ObjectDisposedException)
                {
                }
            }
        }

        public override Encoding Encoding
        {
            get
            {
                return CurrentEncoding;
            }
        }

        public override void Write(string value)
        {
            ConsoleOutput.Write(value);

            if (UseCache)
            {
                CachedData.Enqueue(value);
            }
            else
            {
                Writer.Write(value);
                Writer.Flush();
            }
        }

        public override void WriteLine(string value)
        {
            ConsoleOutput.WriteLine(value);

            if (UseCache)
            {
                CachedData.Enqueue(value);
            }
            else
            {

                Writer.WriteLine(value);
                Writer.Flush();
            }
        }

        public void WriteCache()
        {
            if (CachedData.Count == 0) return;

            // Loop over cached items and send them to the stream to be written
            string output = string.Empty;
            while(CachedData.Count > 0)
            {
                if (CachedData.TryDequeue(out output))
                {
                    Writer.WriteLine(output);
                }
            }

            // Flush the writer to make sure they actually were outputted
            Writer.Flush();
        }
    }
}
