using System.Net;

namespace Galileo.Core.Analytics
{
	class GoogleAnalytics : IAnalytics
    {
        // https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#t
        const string URI = "http://www.google-analytics.com/collect";
        const string ProtocolVersion = "1";
        const string TID = "UA-11332795-13";

        public bool Enabled { get; private set; }
        string _packageVersion;

        System.Net.Http.HttpClient _client;


        public GoogleAnalytics(string packageVersion, bool isEnabled)
        {
            Enabled = isEnabled;
            _packageVersion = WebUtility.UrlEncode(packageVersion);
            _client = new System.Net.Http.HttpClient();
        }

        public void PageView()
        {
            if (!Enabled) return;
            Post("pageview");
        }

        public void ScreenView(string screenName)
        {
            if (!Enabled) return;
            Post("screenview", "&cd=" + WebUtility.UrlEncode(screenName));
        }
        public void Transaction(string transactionID)
        {
            if (!Enabled) return;
            Post("transaction",
                 "&ti=" + WebUtility.UrlEncode(transactionID));
        }
        public void Item(string transactionID, string itemName)
        {
            if (!Enabled) return;
            Post("item",
                 "&ti=" + WebUtility.UrlEncode(transactionID) +
                 "&in=" + WebUtility.UrlEncode(itemName));
        }
        public void Social(string network, string action, string target)
        {
            if (!Enabled) return;
            Post("social",
                 "&sn=" + WebUtility.UrlEncode(network) + 
                 "&sa=" + WebUtility.UrlEncode(action) + 
                 "&st=" + WebUtility.UrlEncode(target));
        }

        public void Exception(string exceptionCode, bool fatal = false)
        {
            if (!Enabled) return;
            if ( fatal ) 
            {
                Post("exception",
                 "&exd=" + WebUtility.UrlEncode(exceptionCode) +
                 "&exf=1");
            }
            else 
            {
                Post("exception",
                 "&exd=" + WebUtility.UrlEncode(exceptionCode) +
                 "&exf=0");
            }

        }
        public void Timing(string timeCategory, string timeVariable, string timeLabel, int timeMilliseconds)
        {
            if (!Enabled) return;
            Post("timing",
                 "&utc=" + WebUtility.UrlEncode(timeCategory) + 
                 "&utv=" + WebUtility.UrlEncode(timeVariable) + 
                 "&utl=" + WebUtility.UrlEncode(timeLabel) + 
                 "&utt=" + timeMilliseconds);
        }

        public void Event(string eventCategory, string eventAction)
        {
            if (!Enabled) return;
            Post("event",
                 "&ec=" + WebUtility.UrlEncode(eventCategory) +
                 "&ea=" + WebUtility.UrlEncode(eventAction));
        }
        public void Event(string eventCategory, string eventAction, int eventValue)
        {
            if (!Enabled) return;
            Post("event",
                 "&ec=" + WebUtility.UrlEncode(eventCategory) +
                 "&ea=" + WebUtility.UrlEncode(eventAction) +
                 "&ev=" + WebUtility.UrlEncode(eventValue.ToString()));
        }
        public void Event(string eventCategory, string eventAction, string eventLabel, int eventValue)
        {
            if (!Enabled) return;
            Post("event",
                 "&ec=" + WebUtility.UrlEncode(eventCategory) +
                 "&ea=" + WebUtility.UrlEncode(eventAction) +
                 "&el=" + WebUtility.UrlEncode(eventLabel) +
                 "&ev=" + WebUtility.UrlEncode(eventValue.ToString()));
        }

        void Post(string hitType, string hitSpecificItems = "")
        {
            if (!Enabled) return;

            string payload = "v=" + ProtocolVersion +
                "&an=Galileo" + 
                "&cid=OpenSource" + 
                "&tid=" + TID +
                "&av=" + _packageVersion +
                "&t=" + hitType + hitSpecificItems;
            System.Net.Http.StringContent postContent = new System.Net.Http.StringContent(payload);
            _client.PostAsync(URI, postContent);
        }
    }
}
