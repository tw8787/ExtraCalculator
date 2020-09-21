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
    /// Class <c>LoginPage</c> determines valid local username/password combinations. If so, 
    /// declares the current user and navigates to the <c>History</c> page. If not, displays 
    /// "Username or Password is Wrong".
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        /// <summary>
        /// Initializes <c>LoginPage</c>.
        /// </summary>
        public LoginPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Checks the current username and password against the current stored list of username/
        /// password combinations and determines if they are valid. 
        /// </summary>
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            AccountManager accountManager = new AccountManager();
            await accountManager.LoadAccounts();
            string[] userData = accountManager.GetAccounts().Split('~');
            string[] userDataSplit;
            foreach (string s in userData)
            {
                userDataSplit = s.Split('|');
                if (UsernameTextBox.Text == userDataSplit[0] && PasswordTextBox.Password == userDataSplit[1])
                {
                    CurrentUser currentUser = new CurrentUser();
                    currentUser.username = UsernameTextBox.Text;
                    this.Frame.Navigate(typeof(HomePage), currentUser);
                    break;
                }
                else
                {
                    PasswordWrong.Text = "Username or Password is Wrong";
                }
            }
        }

        /// <summary>
        /// Navigates to <c>CreateAccountPage</c>.
        /// </summary>
        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateAccountPage));
        }
    }
}
