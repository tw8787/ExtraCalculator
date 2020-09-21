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
    /// Class <c>HomePage</c> manages the "Hamberger Button" and navagating between 
    /// <c>Calculator</c>, <c>History</c>, and <c>Settings</c> when they are selected.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        /// <summary>
        /// Current user's username.
        /// </summary>
        CurrentUser currentUser;
        
        /// <summary>
        /// Initializes <c>HomePage</c>.
        /// </summary>
        public HomePage()
        {
            this.InitializeComponent();
            LocationTextBlock.Visibility = Visibility.Collapsed;
            LocationTextBlock3.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Invoked when the page is nagivated to, and initializes the user's data and displays
        /// the <c>History</c> page first.
        /// </summary>
        /// <param name="e">CurrentUser object</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            currentUser = (CurrentUser)e.Parameter;
            InnerFrame.Navigate(typeof(History), currentUser);

        }

        /// <summary>
        /// Expands and contracts the <c>SplitView</c> pane.
        /// </summary>
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }

        /// <summary>
        /// Navigates between pages when their corresponding item is selected.
        /// </summary>
        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalculatorListBoxItem.IsSelected)
            {
                InnerFrame.Navigate(typeof(Calculator), currentUser);
                LocationTextBlock2.Visibility = Visibility.Collapsed;
                LocationTextBlock3.Visibility = Visibility.Collapsed;
                LocationTextBlock.Visibility = Visibility.Visible;
            }
            else if (HistoryListBoxItem.IsSelected)
            {
                InnerFrame.Navigate(typeof(History), currentUser);
                LocationTextBlock2.Visibility = Visibility.Visible;
                LocationTextBlock3.Visibility = Visibility.Collapsed;
                LocationTextBlock.Visibility = Visibility.Collapsed;
            }
            else if (SettingsListBoxItem.IsSelected)
            {
                InnerFrame.Navigate(typeof(Settings), currentUser);
                LocationTextBlock2.Visibility = Visibility.Collapsed;
                LocationTextBlock3.Visibility = Visibility.Visible;
                LocationTextBlock.Visibility = Visibility.Collapsed;
            }
        }
    }
}
