using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraCalulator
{
    // External Source: https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files

    /// <summary>
    /// Class <c>SettingsManager</c> stores, reads, and writes the current user's settings.
    /// <c>LoadSettings()</c> must be called before called once before <c>GetSettings()</c> for an 
    /// accurate current list of settings.
    /// </summary>
    class SettingsManager
    {
        /// <summary>
        /// Folder that contains <c>settingsFile</c>.
        /// </summary>
        private Windows.Storage.StorageFolder storageFolder;
        /// <summary>
        /// File containing the current user's settings.
        /// </summary>
        private Windows.Storage.StorageFile settingsFile;
        /// <summary>
        /// Current user's settings.
        /// </summary>
        private string settingsList;
        /// <summary>
        /// Current user's username.
        /// </summary>
        private string username;

        /// <summary>
        /// Initializes <c>SettingsManager</c>.
        /// </summary>
        /// <param name="user">Username of the current user</param>
        public SettingsManager(string user)
        {
            username = user;
            storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        }

        /// <summary>
        /// Replaces the current user's settings with the given settings.
        /// </summary>
        /// <param name="foreground">New button foreground</param>
        /// <param name="background">New button background</param>
        public async Task ChangeSettings(string foreground, string background)
        {
            await Windows.Storage.FileIO.WriteTextAsync(settingsFile, foreground + "|" 
                + background);
        }

        /// <summary>
        /// Clears all the user's settings.
        /// </summary>
        public async Task Clear()
        {
            settingsFile = await storageFolder.CreateFileAsync("~" + username + ".txt",
                Windows.Storage.CreationCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// Loads the current settings of the user and updates <c>settingsList</c>.
        /// </summary>
        public async Task LoadSettings()
        {
            settingsFile = await storageFolder.CreateFileAsync("~" + username + ".txt", 
                Windows.Storage.CreationCollisionOption.OpenIfExists);
            Windows.Storage.StorageFile sampleFile 
                = await storageFolder.GetFileAsync("~" + username + ".txt");
            settingsList = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
        }

        /// <summary>
        /// Returns the current settings.
        /// </summary>
        /// <returns>Current settings</returns>
        public string GetSettings()
        {
            return settingsList;
        }
    }
}
