using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraCalulator
{
    // External Source: https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files

    /// <summary>
    /// Class <c>AccountManager</c> stores, reads, and writes the current list of local usernames
    /// and passwords.
    /// <c>LoadAccounts()</c> must be called once before called before <c>GetAccounts()</c> for an 
    /// accurate current list of accounts.
    /// </summary>
    class AccountManager
    {
        /// <summary>
        /// Folder that contains <c>accountFile</c>.
        /// </summary>
        private readonly Windows.Storage.StorageFolder storageFolder;
        /// <summary>
        /// File containing the account information.
        /// </summary>
        private Windows.Storage.StorageFile accountFile;
        /// <summary>
        /// Current list of account information.
        /// </summary>
        private string accountList;

        /// <summary>
        /// Initializes <c>AccountManager</c>.
        /// </summary>
        public AccountManager()
        {
            storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            accountList = "";
        }

        /// <summary>
        /// Appends an account to the end of the list with username <paramref name="user"/> and 
        /// password <paramref name="pass"/>.
        /// </summary>
        /// <param name="user">Username of the new account</param>
        /// <param name="pass">Password of the new account</param>
        public async Task AddAccount(string user, string pass)
        {
            await Windows.Storage.FileIO.WriteTextAsync(accountFile, GetAccounts() + user + "|"
                        + pass + "~");
            await LoadAccounts();
        }

        /// <summary>
        /// Clears all local account information.
        /// </summary>
        public async Task Clear()
        {
            accountFile = await storageFolder.CreateFileAsync("accounts.txt", 
                Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await LoadAccounts();
        }

        /// <summary>
        /// Loads the current list of account information and updates <c>accountList</c>.
        /// </summary>
        public async Task LoadAccounts()
        {
            accountFile = await storageFolder.CreateFileAsync("accounts.txt", 
                Windows.Storage.CreationCollisionOption.OpenIfExists);
            Windows.Storage.StorageFile sampleFile 
                = await storageFolder.GetFileAsync("accounts.txt");
            accountList = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
        }

        /// <summary>
        /// Returns the current list of account information.
        /// </summary>
        /// <returns>Current list of account information</returns>
        public string GetAccounts()
        {
            return accountList;
        }
    }
}
