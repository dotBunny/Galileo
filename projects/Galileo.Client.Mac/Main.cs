#if __PACKAGE__
using System;
#endif
using AppKit;

namespace Galileo.Client.Mac
{
    /// <summary>
    /// Program Main
    /// </summary>
    static class MainClass
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            NSApplication.Init();

#if __PACKAGE__
            try
            {
                NSApplication.Main(args);
            }
            catch(Exception e)
            {
                Core.Log.Session.Add("UNHANDLED EXCEPTION", e.Message + "\n" + e.StackTrace);
            }
#else

            NSApplication.Main(args);
#endif
        }
    }
}
