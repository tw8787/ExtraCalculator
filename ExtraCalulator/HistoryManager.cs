using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraCalulator
{
    // External Source: https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files

    /// <summary>
    /// Class <c>HistoryManager</c> stores, reads, and writes the current user's calculation
    /// history along with the date, time, and location.
    /// <c>LoadHistory()</c> must be called before called once before <c>GetHistory()</c> for an 
    /// accurate current list of accounts.
    /// </summary>
    class HistoryManager
    {
        /// <summary>
        /// Folder that contains <c>historyFile</c>.
        /// </summary>
        private readonly Windows.Storage.StorageFolder storageFolder;
        /// <summary>
        /// File containing the current user's calculation history.
        /// </summary>
        private Windows.Storage.StorageFile historyFile;
        /// <summary>
        /// Current list of the user's calculation history.
        /// </summary>
        private string historyList;
        /// <summary>
        /// Current user's username.
        /// </summary>
        private string username;

        /// <summary>
        /// Initializes <c>HistoryManager</c>.
        /// </summary>
        /// <param name="user">Username of the current user</param>
        public HistoryManager(string user)
        {
            username = user;
            storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        }

        /// <summary>
        /// Appends the history of a calculation to the end of the calculation history list. 
        /// </summary>
        /// <param name="value1">First value of the operation</param>
        /// <param name="op">Operator of the operation</param>
        /// <param name="value2">Second value of the operation</param>
        /// <param name="result">Result of the operation</param>
        /// <param name="lon">Longitude of the user</param>
        /// <param name="lat">Latitude of the user</param>
        public async Task AddHistory(Double value1, string op, Double value2, string result, 
            string lon, string lat)
        {
            await Windows.Storage.FileIO.WriteTextAsync(historyFile, GetHistory() + value1 + " " 
                + op + " " + value2 + " = " + result + " | " + DateTime.Now.ToString() 
                + " | Longitude: " + lon + " Latitude: " + lat + "~");
            await LoadHistory();
        }

        /// <summary>
        /// Clears all the user's history.
        /// </summary>
        public async Task Clear()
        {
            historyFile = await storageFolder.CreateFileAsync(username + ".txt", 
                Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await LoadHistory();
        }

        /// <summary>
        /// Loads the current calculation history list of the user and updates <c>historyList</c>.
        /// </summary>
        public async Task LoadHistory()
        {
            historyFile = await storageFolder.CreateFileAsync(username + ".txt", 
                Windows.Storage.CreationCollisionOption.OpenIfExists);
            Windows.Storage.StorageFile sampleFile 
                = await storageFolder.GetFileAsync(username + ".txt");
            historyList = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
        }

        /// <summary>
        /// Returns the current calculation history.
        /// </summary>
        /// <returns>Current calculation history</returns>
        public string GetHistory()
        {
            return historyList;
        }
    }
}
