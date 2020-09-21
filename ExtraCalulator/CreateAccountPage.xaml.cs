using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ExtraCalulator
{
    /// <summary>
    /// Class <c>CreateAccountPage</c> initializes the local files of each newly created account.
    /// Account usernames must be unique, cannot be "accounts", and cannot contain "~".
    /// </summary>
    public sealed partial class CreateAccountPage : Page
    {
        /// <summary>
        /// Initializes <c>CreateAccountPage</c>.
        /// </summary>
        public CreateAccountPage()
        {
            this.InitializeComponent();
        }
        
        /// <summary>
        /// If given a proper username and password, initializes the account's files, and navigates
        /// to <c>LoginPage</c>. If not, dispays "Username in Use or Contains '~'".
        /// </summary>
        private async void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AccountManager accountManager = new AccountManager();
            await accountManager.LoadAccounts();
            if (UsernameTextBox.Text.Length > 0 && PasswordTextBox.Password.Length > 0)
            {
                string[] userData = accountManager.GetAccounts().Split('~');
                string[] userDataSplit;
                Boolean usernameUsed = false;
                // Checks that the username hasn't been used yet
                foreach (string s in userData)
                {
                    userDataSplit = s.Split('|');
                    if (UsernameTextBox.Text == userDataSplit[0])
                    {
                        usernameUsed = true;
                    }
                }
                if (UsernameTextBox.Text == "accounts" || UsernameTextBox.Text.Contains('~'))
                {
                    usernameUsed = true;
                }
                if (!usernameUsed)
                {
                    await accountManager.AddAccount(UsernameTextBox.Text, 
                        PasswordTextBox.Password);
                    this.Frame.Navigate(typeof(LoginPage));
                }
                else
                {
                    UsernameUsed.Text = "Username in Use or Contains \"~\"";
                }
            }
        }

        /// <summary>
        /// Navigates back to <c>LoginPage</c>.
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
