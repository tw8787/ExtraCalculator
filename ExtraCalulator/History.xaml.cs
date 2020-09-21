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
    /// Class <c>History</c> reads and displays the user's history.
    /// </summary>
    public sealed partial class History : Page
    {
        /// <summary>
        /// Initializes <c>History</c>.
        /// </summary>
        public History()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the page is nagivated to, and initializes the current user's history.
        /// </summary>
        /// <param name="e">CurrentUser object</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentUser currentUser = (CurrentUser)e.Parameter;
            HistoryManager historyManager = new HistoryManager(currentUser.username);
            SettingsManager settingsManager = new SettingsManager(currentUser.username);
            await historyManager.LoadHistory();
            await settingsManager.LoadSettings();
            string[] calculations = historyManager.GetHistory().Split('~');
            foreach (string s in calculations)
            {
                HistoryListView.Items.Add(new TextBlock { Text = s });
            }
            string settings = settingsManager.GetSettings();
            // After logging in, users land on this page (History.xaml)
            // Therefore the style settings are loaded here
            if (settings.Contains('|'))
            {
                //External Source: https://stackoverflow.com/questions/10345166/changing-xaml-style-dynamically-in-code-behind-so-that-controls-applying-that-st
                string[] lastSettings = settings.Split('|');
                string currentBackground = lastSettings[1];
                string currentForeground = lastSettings[0];
                Style style = new Style(typeof(Button));
                style.Setters.Add(new Setter(Button.BackgroundProperty, currentBackground));
                style.Setters.Add(new Setter(Button.WidthProperty, "90"));
                style.Setters.Add(new Setter(Button.HeightProperty, "90"));
                style.Setters.Add(new Setter(Button.FontFamilyProperty, "Arial Black"));
                style.Setters.Add(new Setter(Button.FontSizeProperty, "35"));
                style.Setters.Add(new Setter(Button.ForegroundProperty, currentForeground));
                Application.Current.Resources["CalculatorButtonStyle"] = style;
                style = new Style(typeof(Button));
                style.Setters.Add(new Setter(Button.BackgroundProperty, currentBackground));
                style.Setters.Add(new Setter(Button.ForegroundProperty, currentForeground));
                Application.Current.Resources["CurrentCalculatorButtonStyle"] = style;
            }
        }
    }
}
