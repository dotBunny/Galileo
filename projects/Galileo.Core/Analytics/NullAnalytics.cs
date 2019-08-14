using System;
namespace Galileo.Core.Analytics
{
	public class NullAnalytics : IAnalytics
    {
        public bool Enabled => false;

        public void Event(string eventCategory, string eventAction)
        {
        }

        public void Event(string eventCategory, string eventAction, int eventValue)
        {
        }

        public void Event(string eventCategory, string eventAction, string eventLabel, int eventValue)
        {
        }

        public void Exception(string exceptionCode, bool fatal = false)
        {
        }

        public void Item(string transactionID, string itemName)
        {
        }

        public void PageView()
        {
        }

        public void ScreenView(string screenName)
        {
        }

        public void Social(string network, string action, string target)
        {
        }

        public void Timing(string timeCategory, string timeVariable, string timeLabel, int timeMilliseconds)
        {
        }

        public void Transaction(string transactionID)
        {
        }
    }
}
