using System;
using System.Reflection;
using Galileo.Core;
using I18NPortable;

namespace Galileo.Client.CLI
{
    /// <summary>
    /// Galileo Server Executable
    /// </summary>
    class Program
    {
        /// <summary>
        /// Is Galileo currently processing?
        /// </summary>
        static bool _isProcessing;

        /// <summary>
        /// The entry point of the program, where the program control starts and ends
        /// </summary>
        /// <param name="args">The command-line arguments</param>
        static void Main(string[] args)
        {
            // Start up Galileo
            Instance.Initialize(HunterProfile.Create("2019.1.0", Assembly.GetAssembly(typeof(Settings)).GetName().Version, Settings.Localization));
            // Handle client command line arguments
            Client.CommandLineOptions commandLine = new Client.CommandLineOptions(args);

            // Handle server specific command lines arguments
            CommandLineOptions serverCommandLine = new CommandLineOptions(args);

            if (commandLine.HelpRequested || commandLine.ShouldQuit)
            {
                return;
            }

            // Point it in the right direction

            HuntHandler handler = null;

            if (!string.IsNullOrEmpty(serverCommandLine.TargetFolder))
            {
                handler = new HuntHandler(HuntHandler.CreateID(), Instance.Profile, serverCommandLine.TargetFolder);
            }
            else
            {
                handler = HuntHandler.Create(HuntHandler.CreateID(), Instance.Profile);
            }

            
            // Create our loop stopper
            handler.OnProcessComplete += delegate
            {
                _isProcessing = false;
            };
            handler.OnProcessLogEvent += (string id, string line) => {
                Console.WriteLine(line);
            };
            // Point it in the right direction
            if (!string.IsNullOrEmpty(serverCommandLine.TargetFolder))
            {
                // Setup data directory?
                handler.PreProcess();
                // Go!                handler.Process();
            }
            else
            {
                Console.WriteLine("CLI.Process.NoTarget".Translate());                return;
            }
            // Loop
            while(_isProcessing)
            {
            }
        }
    }
}
