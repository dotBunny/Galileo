using I18NPortable;
using Microsoft.Extensions.CommandLineUtils;

namespace Galileo.Client.CLI
{
    /// <summary>
    /// Command line options handler with specific server side options
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// Specified Target Folder
        /// </summary>
        /// <value>The target folder's path</value>
        public string TargetFolder { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.CLI.CommandLineOptions"/> class
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public CommandLineOptions(string[] args)
        {
            // Create parser and don't barf if we get an unrecognized argument
            CommandLineApplication commandLine = new CommandLineApplication(false);

            CommandOption optionTargetFolder = commandLine.Option("--folder <TARGET_FOLDER_PATH>", "CLI.CommandLineOptions.TargetFolder".Translate(), CommandOptionType.SingleValue);
           // CommandOption optionTargetFiles = commandLine.Option("--files <TARGET_FILES_PATH>", "Run process against target files.", CommandOptionType.MultipleValue);
            // output folder

            commandLine.HelpOption("--help");

            commandLine.OnExecute(() =>
            {
                // Deactivate the license if we have that as an option first, allowing for subsequent reactivate.
                if (optionTargetFolder.HasValue())
                {
                    if (System.IO.Directory.Exists(optionTargetFolder.Value()))
                    {
                        TargetFolder = optionTargetFolder.Value();
                    }
                }

                return 0;
            });

            // Parse Arguments
            commandLine.Execute(args);
        }
    }
}
