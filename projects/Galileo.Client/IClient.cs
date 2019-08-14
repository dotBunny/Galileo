namespace Galileo.Client
{
    /// <summary>
    /// Application Interace
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Localize the client
        /// </summary>
        void Localize();

        /// <summary>
        /// Set the state of the window (the screen)
        /// </summary>
        /// <returns>Was the window state updated?</returns>
        /// <param name="newState">The state which should be changed too</param>
        /// <param name="forceRefresh">Should the update befored?</param>
        bool SetWindowState(Instance.State newState, bool forceRefresh = false);
    }
}
