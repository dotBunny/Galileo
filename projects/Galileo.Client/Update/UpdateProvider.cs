using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Galileo.Core.Logging;

namespace Galileo.Client.Update
{
    /// <summary>
    /// Galileo's Update Logic
    /// </summary>
    public class UpdateProvider
    {
        #region Fields

        /// <summary>
        /// Cached version to ignore when checking for updates.
        /// </summary>
        private string _cachedIgnoreVersion;

        #endregion

        #region Events

        /// <summary>
        /// The fired action if an update is found.
        /// </summary>
        public Action OnUpdateFound;

        #endregion

        #region Enums

        /// <summary>
        /// Update Channels
        /// </summary>
        public enum Channel
        {
            /// <summary>
            /// The default channel.
            /// </summary>
            Release,
            /// <summary>
            /// The beta tester channel.
            /// </summary>
            Beta
        }

        /// <summary>
        /// Update Frequencies
        /// </summary>
        public enum Frequency
        {
            /// <summary>
            /// Daily Check
            /// </summary>
            Daily,
            /// <summary>
            /// Weekly Check
            /// </summary>
            Weekly,
            /// <summary>
            /// Monthly Check
            /// </summary>
            Monthly
        }

        #endregion

        #region Properties

        /// <summary>
        /// The current changelog if available.
        /// </summary>
        /// <value>The changelog.</value>
        public string Changelog
        {
            get; private set;
        }

        /// <summary>
        /// A reference to the current update information retrieved.
        /// </summary>
        /// <value>The current update.</value>
        public UpdateCurrent Current 
        { 
            get; private set; 
        }

        /// <summary>
        /// A reference to the current specification information retrieved.
        /// </summary>
        /// <value>The update specification.</value>
        public UpdateSpecification Specification
        {
            get; private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check for updates against the saved channel.
        /// </summary>
        /// <param name="ignoreVersion">Version string to ignore.</param>
        /// <param name="force">Ignore frequency, and check anyways?</param>
        public void Check(string ignoreVersion = "", bool force = false)
        {
            // Bailout if we are not allow to check
            if (!Settings.ShouldCheckForUpdates && !force) return;

            var hours = HoursTillUpdate(out var lastDate);
            // Is our available date in the future
            


            if ( hours > 0 && !force)
            {
                Log.Session.Add("Client.UpdateProvider.Check", "Last checked on " + lastDate + " (" + hours + " hours till next check).");
                return;
            }

            _cachedIgnoreVersion = ignoreVersion;

            Log.Session.Add("Client.UpdateProvider.Check", "Checking for updates ...");


            // Store setting in UTC
            Settings.LastUpdateCheck = DateTime.Now.ToUniversalTime();

            CheckForUpdates();
        }

        /// <summary>
        /// Checks the update frequency, how many hours are left
        /// </summary>
        /// <param name="lastStamp">out Last Checked</param>
        public static int HoursTillUpdate(out string lastStamp)
        {
            // Extract UTC to local
            var lastCheckDate = TimeZoneInfo.ConvertTimeFromUtc(Settings.LastUpdateCheck, TimeZoneInfo.Local);
			lastStamp = lastCheckDate.ToString(Localization.LocalizationCache.DateLongFormat);

            var working = lastCheckDate;

            // Check time timing
            switch (Settings.UpdateCheckFrequency)
            {
                case Frequency.Daily:
                    working = working.AddDays(1);
                    break;
                case Frequency.Weekly:
                    working = working.AddDays(7);
                    break;
                case Frequency.Monthly:
                    working = working.AddMonths(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (int)(working - DateTime.Now).TotalHours;
        }

        /// <summary>
        /// Get the appropriate update channel file.
        /// </summary>
        /// <returns>The update file.</returns>
        /// <param name="channel">The selected channel.</param>
        private static string GetUpdateFile(Channel channel)
        {
            return channel == Channel.Beta ? "beta.json" : "current.json";
        }

        /// <summary>
        /// Async call to check for updates.
        /// </summary>
        /// <returns>The for updates.</returns>
        private Task<UpdateCurrent> CheckForUpdates()
        {
            try
            {
                // Failsafe check for no DNS, causes exception if internet isn't present
                // Skips the "http://" (7 spots)
                var hostInfo = Dns.GetHostEntry(Links.Website.Substring(7));

                var request = (HttpWebRequest)WebRequest.Create(Links.Website + "/files/" + GetUpdateFile(Settings.UpdatesChannel));
                request.ContentType = "application/json";
                request.Method = WebRequestMethods.Http.Get;
                request.Timeout = 20000;
                request.Proxy = null;

                var task = Task.Factory.FromAsync(
                    request.BeginGetResponse,
                    asyncResult => request.EndGetResponse(asyncResult),
                    (object)null);

                return task.ContinueWith(t => ReadCheckStream(t.Result));
            }
            catch (Exception e)
            {
                Log.Session.Add("Client.UpdateProvider.CheckForUpdate", "An exception occured during the update check. " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Async call to get the changelog.
        /// </summary>
        /// <returns>The changelog.</returns>
        private Task<string> GetChangelog()
        {
            var request = (HttpWebRequest)WebRequest.Create(Specification.ChangelogLink);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get;
            request.Timeout = 20000;
            request.Proxy = null;

            var task = Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                (object)null);

            return task.ContinueWith(t => ReadChangelogStream(t.Result));
        }

        /// <summary>
        /// Async call to get the details of an update.
        /// </summary>
        /// <returns>The update details.</returns>
        private Task<UpdateSpecification> GetUpdateDetails()
        {
            Log.Session.Add("Client.UpdateProvider.GetUpdateDetails", "Reading update information from " + Links.Website + Current.SpecUrl);
            var request = (HttpWebRequest)WebRequest.Create(Links.Website + Current.SpecUrl);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get;
            request.Timeout = 20000;
            request.Proxy = null;

            var task = Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult), null);

            return task.ContinueWith(t => ReadSpecStream(t.Result));
        }

        /// <summary>
        /// Reads the changelog stream from a web response.
        /// </summary>
        /// <returns>The changelog as a string.</returns>
        /// <param name="response">A web response from an async call.</param>
        private string ReadChangelogStream(WebResponse response)
        {
            Changelog = string.Empty;
            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                Changelog = sr.ReadToEnd();

                if (Changelog != string.Empty)
                {
                    OnUpdateFound?.Invoke();
                }
            }
            return Changelog;
        }

        /// <summary>
        /// Reads the current check stream from a web response.
        /// </summary>
        /// <returns>The current update information.</returns>
        /// <param name="response">A web response from an async call.</param>
        private UpdateCurrent ReadCheckStream(WebResponse response)
        {
            Current = new UpdateCurrent();

            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {

                // Read JSON
                var raw = sr.ReadToEnd();
                try
                {
                    Current = JsonConvert.DeserializeObject<UpdateCurrent>(raw);
                    Current.WasSuccessful = true;
                }
                catch (Exception e)
                {
                    Current.WasSuccessful = false;
                    Log.Session.Add("Client.UpdateProvider.ReadCheckStream", "Unable to read check stream: " + e.Message);
                }

                // Check if its current, and check if its ignored
                if (Current.Version != Instance.Profile.PackageVersion && Current.Version != _cachedIgnoreVersion)
                {
                    // Get more information
                    Log.Session.Add("Client.UpdateProvider.ReadCheckStream", "Update found (" + Current.Version + ").");
                    GetUpdateDetails();
                }
                else
                {
                    Log.Session.Add("Client.UpdateProvider.ReadCheckStream", "Galileo is up-to-date.");
                }
            }
            return Current;
        }

        /// <summary>
        /// Reads the current update specification stream from a web response.
        /// </summary>
        /// <returns>The current update's specification.</returns>
        /// <param name="response">A web response from an async call.</param>
        private UpdateSpecification ReadSpecStream(WebResponse response)
        {
            Specification = new UpdateSpecification();

            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {

                // Read JSON
                var raw = sr.ReadToEnd();
                try
                {
                    Specification = JsonConvert.DeserializeObject<UpdateSpecification>(raw);
                    Specification.WasSuccessful = true;

                    GetChangelog();
                }
                catch (Exception e)
                {
                    Specification.WasSuccessful = false;
                    Log.Session.Add("Client.UpdateProvider.ReadSpecStream", "Unable to read spec stream: " + e.Message);
                }
            }
            return Specification;
        }

        #endregion
    }
}
