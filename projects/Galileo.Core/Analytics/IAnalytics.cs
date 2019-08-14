namespace Galileo.Core.Analytics
{
  public interface IAnalytics
  {
    bool Enabled { get; }

    void Event(string eventCategory, string eventAction);
    void Event(string eventCategory, string eventAction, int eventValue);
    void Event(string eventCategory, string eventAction, string eventLabel, int eventValue);
    void Exception(string exceptionCode, bool fatal = false);
    void Item(string transactionID, string itemName);
    void PageView();
    void ScreenView(string screenName);
    void Social(string network, string action, string target);
    void Timing(string timeCategory, string timeVariable, string timeLabel, int timeMilliseconds);
    void Transaction(string transactionID);
  }
   
}