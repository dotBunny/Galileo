using Newtonsoft.Json;

namespace Galileo.Client.Update
{
    /// <summary>
    /// The current update information retrieved.
    /// </summary>
    [JsonObject]
    public class UpdateCurrent
    {
        /// <summary>
        /// The current version as returned by our servers.
        /// </summary>
        [JsonProperty("v")]
        public string Version = "?";

        /// <summary>
        /// The current versions specification as returned by our servers.
        /// </summary>
        [JsonProperty("s")]
        public string SpecUrl = "?";

        /// <summary>
        /// Was the pull of information successful?
        /// </summary>
        [JsonIgnore]
        public bool WasSuccessful = false;
    }
}
