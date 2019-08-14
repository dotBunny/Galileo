using System;
using I18NPortable;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using Galileo.Core.Logging;

namespace Galileo.Client
{
    /// <summary>
    /// Command-line Options Handler
    /// </summary>
    public class CommandLineOptions
    {
        public CommandLineOptions(string[] args)
        {

            // Create parser and don't barf if we get an unrecognized argument
            CommandLineApplication commandLine = new CommandLineApplication(false);
            CommandOption optionConfigDefault = commandLine.Option("--import-defaults <PATH>", "Client.CommandLineOptions.ImportDefaultsHelp".Translate(), CommandOptionType.SingleValue);
			CommandOption optionLocale = commandLine.Option("--locale <CODE>", "Client.CommandLineOptions.LocaleHelp".Translate(), CommandOptionType.SingleValue);
			CommandOption optionResetWindow = commandLine.Option("--reset-window", "Client.CommandLineOptions.ResetWindowHelp".Translate(), CommandOptionType.NoValue);
            CommandOption optionQuit = commandLine.Option("--quit", "Client.CommandLineOptions.QuitHelp".Translate(), CommandOptionType.NoValue);

            // CLI features ---
            // CommandOption optionTargetFolder = commandLine.Option("--folder <TARGET_FOLDER_PATH>", "Run process against target folder.", CommandOptionType.SingleValue);
            // CommandOption optionTargetFiles = commandLine.Option("--files <TARGET_FILES_PATH>", "Run process against target files.", CommandOptionType.MultipleValue);

            // Define help option
            commandLine.HelpOption("--help");
            
            commandLine.OnExecute(() =>
			{
    			// Hande Default Config
    			if (optionConfigDefault.HasValue())
    			{
    				if (System.IO.File.Exists(optionConfigDefault.Value()))
    				{
    					try
    					{
    						Settings.DefaultConfig = JsonConvert.DeserializeObject<Core.HunterConfig>(System.IO.File.ReadAllText(optionConfigDefault.Value()));
    					}
    					catch (Exception e)
    					{
    						Log.Session.Add("Client.CommandLineOptions", "Invalid options file: " + e.Message);
    					}
    				}
    				else
    				{
    					Log.Session.Add("Client.CommandLineOptions", "Unable to find options file: " + optionConfigDefault.Value());
    				}
    			}

    			// Locale Setting
    			if (optionLocale.HasValue())
				{
					Localization.LocalizationProvider.SetLocale(optionLocale.Value());
					Settings.Localization = Localization.LocalizationProvider.GetCulture();
				}

                if (optionResetWindow.HasValue())
                {
                    Settings.RemoveWindowItems();
                }

                if (optionQuit.HasValue())
                {
                    Log.Session.Add("Client.CommandLineOptions", "Quiting ...");
                    ShouldQuit = true;
                }


				if (!commandLine.OptionHelp.HasValue()) return 0;
				
				Log.Session.Add("Client.CommandLineOptions", "Help Requested ...");
				HelpRequested = true;

				return 0;
            });

            // Parse Arguments
            commandLine.Execute(args);
        }

        #region Properties

        /// <summary>
        /// Has help been requested? 
        /// </summary>
        /// <value><c>true</c> if help was requested; otherwise, <c>false</c>.</value>
        public bool HelpRequested { get; private set; }

        /// <summary>
        /// Should Galileo exit immediately after processing the command-line options.
        /// </summary>
        /// <value><c>true</c> if it should quit; otherwise, <c>false</c>.</value>
        public bool ShouldQuit { get; private set; }

        #endregion
    }
}
