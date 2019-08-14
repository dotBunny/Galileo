using System.Collections.Generic;
using System.IO;
using Galileo.Core;
using Newtonsoft.Json;

namespace Galileo.Client.Screens
{
    /// <summary>
    /// Galileo's Process Screen Logic
    /// </summary>
    public static class ProcessScreen
    {      
        /// <summary>
        /// Triggered logic when the Process button is clicked.
        /// </summary>
        /// <returns><c>true</c>, work has started<c>false</c> if it was cancelled.</returns>
        /// <param name="index">Hunter index.</param>
        public static bool ProcessButton_Click(string index)
        {
            if (Instance.Hunters[index].IsWorking)
            {
                Instance.Hunters[index].Cancel();
                return false;
            }

            // Pre process setup
            Instance.Hunters[index].PreProcess();
            

            Instance.Hunters[index].Process();
            return true;
        }

        /// <summary>
        /// Triggered logic when the Report button is clicked.
        /// </summary>
        /// <param name="index">Hunter index.</param>
        public static void ReportButton_Click(string index)
        {
            Instance.Hunters[index].OpenReport();
        }

        public static List<string> GetFiles()
		{
			return new List<string>();
		}
    }
}
