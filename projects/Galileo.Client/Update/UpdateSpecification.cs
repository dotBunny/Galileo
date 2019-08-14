using System;
using Newtonsoft.Json;

namespace Galileo.Client.Update
{
    /// <summary>
    /// Detailed information on an update.
    /// </summary>
    [JsonObject]
    public class UpdateSpecification
    {
        /// <summary>
        /// Link to the changelog for the particular release.
        /// </summary>
        [JsonProperty("changelog")]
        public string ChangelogLink = "?";

        /// <summary>
        /// The link to the macOS download.
        /// </summary>
        [JsonProperty("mac")]
        public string MacLink = "?";

        /// <summary>
        /// The size of the macOS download.
        /// </summary>
        [JsonProperty("mac-size")]
        public string MacSize = "?";

        /// <summary>
        /// A message to display to the user in the update window.
        /// </summary>
        [JsonProperty("message")]
        public string Message = "?";

        /// <summary>
        /// The release date as told by the specification.
        /// </summary>
        [JsonProperty("date")]
        public string ReleaseDate = "?";

        /// <summary>
        /// The link to the Windows download.
        /// </summary>
        [JsonProperty("win")]
        public string WindowsLink = "?";

        /// <summary>
        /// The size of the Windows download.
        /// </summary>
        [JsonProperty("win-size")]
        public string WindowsSize = "?";

        /// <summary>
        /// Was the pull of information successful?
        /// </summary>
        [JsonIgnore]
        public bool WasSuccessful = false;
    }
}