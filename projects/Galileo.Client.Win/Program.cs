using System;

using System.Reflection;

using System.Threading;

using System.Windows.Forms;

#if __PACKAGE__

using System.IO;

#endif



namespace Galileo.Client.Win

{

    /// <summary>

    /// Galileo Program

    /// </summary>

    static class Program

    {

        /// <summary>

        /// Reference to the main window used by the application

        /// </summary>

        static MainForm _mainWindow;



        /// <summary>

        /// Updates Provider

        /// </summary>

        static Update.UpdateProvider _updates;



        /// <summary>

        /// Check for updates to the application

        /// </summary>

        /// <param name="useIgnore">Should we ignore a previously ignored version?</param>

        public static void CheckForUpdates(bool useIgnore = true, bool force = false)

        {

            if (_updates == null)

            {

                _updates = new Update.UpdateProvider();

                _updates.OnUpdateFound += OnUpdateFound;

            }



            ThreadStart start = () =>

               {

                   // Check for update

                   if (useIgnore)

                   {

                       _updates.Check(Settings.IgnoreVersion, force);

                   }

                   else

                   {

                       _updates.Check(string.Empty, force);

                   }

               };

            Thread thread = new Thread(start);

            thread.Start();

        }



        /// <summary>

        /// The main entry point for the application.

        /// </summary>

        [STAThread]

        static void Main(string[] args)

        {

            // Grab the version from the embedded resource, defaulting to 0.0.0.0 to indicate an editor build

            string packageVersion = "0.0.0.0";

#if __PACKAGE__

            using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream("Galileo.Client.Win.Resources.version.txt"))

            using (StreamReader reader = new StreamReader(stream))

            {

                packageVersion = reader.ReadToEnd();

            }

#endif



            // Start up Galileo

            Instance.Initialize(

                new Core.HunterProfile(

                    packageVersion,

                    Assembly.GetAssembly(typeof(Core.HunterConfig)).GetName().Version,

                    Assembly.GetAssembly(typeof(Settings)).GetName().Version,

                    Assembly.GetEntryAssembly().GetName().Version,

                    Settings.Localization));



            // Handle command line arguments

            CommandLineOptions commandLine = new CommandLineOptions(args);



            if (commandLine.HelpRequested || commandLine.ShouldQuit)

            {

                // We can use this because it would have been launched via the command line

                Application.Exit();

                return;

            }



            // Platform specific styling

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);



            // Create a new window

            _mainWindow = new MainForm();



            // Initialize update backend

            _mainWindow.OnAwake += OnMainWindowAwake;



            Instance.Log("Client.Win.Program.Main", "Initializing Window ...");



            Application.Run(_mainWindow);



            Instance.Shutdown();

        }



        /// <summary>

        /// Triggered when the main window has loaded

        /// </summary>

        static void OnMainWindowAwake()

        {

            CheckForUpdates();

        }



        /// <summary>

        /// Update Found Callback

        /// </summary>

        static void OnUpdateFound()

        {

            _mainWindow.SetUpdateState(true, _updates);

        }

    }

}

