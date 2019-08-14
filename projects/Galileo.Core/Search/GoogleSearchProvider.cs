using System;
namespace Galileo.Core.Search
{
    // https://developers.google.com/web/updates/2017/04/headless-chrome
    // 
    // 
    // https://cs.chromium.org/chromium/src/headless/app/headless_shell_switches.cc

    class GoogleSearchProvider
    {
        // random size'
        // random agent
        // timeout is miliseconds
        //  --headless --disable-gpu --timeout=10000 --window-size=800,600 --dump-dom https://www.google.ca/search?q=test >> test.log
        //-user-agent=""

        static string ChromeExecutablePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
        static string BaseArguments = "--headless --disable-gpu --dump-dom";
        static int Timeout = 10000;

        static readonly string[] UserAgents = {
            // Linux Chrome
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41",

            // Windows Chrome
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
            "Mozilla/5.0 (Windows NT 5.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36",

            // Windows FireFox
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0",
           
            // macOS Firefox
            "Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:42.0) Gecko/20100101 Firefox/42.0",

            // Mobile
            "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1",
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)"
        };

        static readonly string[] WindowSizes = {
            "800,600",
            "1024,768",
            "1920,1080",
            "1280,720"
        };


        /*
         * 
         * Core.SearchEngines.Google test = new Core.SearchEngines.Google();
            test.GetResults("happy");*/



        // OPTION: UseCurl vs Use ChromeHeadless
        // OPTION: Use API vs scraper


        // Cache results because no sense researching, 1 day

        internal void GetResults(string query)
        {
            string buildArguments = BaseArguments;
            buildArguments += " --timeout=" + Timeout.ToString();

            buildArguments += " --window-size=" + WindowSizes[Compare.RandomNumber.Next(0, WindowSizes.Length - 1)];

            buildArguments += " --user-agent=\"" + UserAgents[Compare.RandomNumber.Next(0, UserAgents.Length - 1)] + "\"";


            buildArguments += " https://www.google.ca/search?q=" + query;


            System.Diagnostics.Process proc = new System.Diagnostics.Process();


            proc.StartInfo.FileName = ChromeExecutablePath;
            proc.StartInfo.Arguments = buildArguments;


            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.StartInfo.CreateNoWindow = true;

            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();
            //proc.WaitForExit();

            Core.Logging.Log.Session.Add("Google", output);




            // Limit query

            // Open New Browser (save file)
            // this allows for captcha

            // Parse file
        }
    }
}
