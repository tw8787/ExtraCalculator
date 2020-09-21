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
    /// Class <c>Settings</c> changes the foreground and background of the calculator buttons, and
    /// clears the history when prompted.
    /// </summary>
    public sealed partial class Settings : Page
    {
        /// <summary>
        /// Current style of the calculator buttons' foreground.
        /// </summary>
        private string currentForeground;
        /// <summary>
        /// Current style of the calculator buttons' background.
        /// </summary>
        private string currentBackground;
        /// <summary>
        /// Current user's username.
        /// </summary>
        CurrentUser currentUser;
        /// <summary>
        /// Reads and writes the user's settings.
        /// </summary>
        SettingsManager settingsManager;

        /// <summary>
        /// Initializes <c>Settings</c>.
        /// </summary>
        public Settings()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the style of the "Previous Style" button to the previous style.
        /// </summary>
        /// <param name="e">CurrentUser object</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentUser = (CurrentUser)e.Parameter;
            string username = currentUser.username;
            settingsManager = new SettingsManager(username);
            await settingsManager.LoadSettings();
            currentForeground 
                = NumberToColor(((SolidColorBrush)TestButton.Foreground).Color.ToString());
            currentBackground 
                = NumberToColor(((SolidColorBrush)TestButton.Background).Color.ToString());
            await settingsManager.ChangeSettings(currentForeground, currentBackground);
            ForegroundFlyout.Content = currentForeground;
            CalculatorFlyout.Content = currentBackground;
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to black.
        /// </summary>
        private void BlackForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Black");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to red.
        /// </summary>
        private void RedForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Red");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to orange.
        /// </summary>
        private void OrangeForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Orange");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to yellow.
        /// </summary>
        private void YellowForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Yellow");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to green.
        /// </summary>
        private void GreenForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Green");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to blue.
        /// </summary>
        private void BlueForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Blue");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to indigo.
        /// </summary>
        private void IndigoForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Indigo");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground color to violet.
        /// </summary>
        private void VioletForeground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Foreground", "Violet");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to gray.
        /// </summary>
        private void GrayBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "LightGray");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to red.
        /// </summary>
        private void RedBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "Red");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to orange.
        /// </summary>
        private void OrangeBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "Orange");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to yellow.
        /// </summary>
        private void YellowBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "Yellow");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to green.
        /// </summary>
        private void GreenBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "Green");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to blue.
        /// </summary>
        private void BlueBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "Blue");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to indigo.
        /// </summary>
        private void IndigoBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "Indigo");
        }

        /// <summary>
        /// Changes the calculator buttons' background color to violet.
        /// </summary>
        private void VioletBackground_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange("Background", "Violet");
        }

        /// <summary>
        /// Changes the calculator buttons' foreground and background colors to their new colors,
        /// and saves the color settings. Also states "Color Changed to <paramref name="color"/>".
        /// </summary>
        /// <param name="color">Recent foreground or background color change</param>
        private async void CalculatorStyleChanger(string color)
        {
            //External Source: https://stackoverflow.com/questions/10345166/changing-xaml-style-dynamically-in-code-behind-so-that-controls-applying-that-st
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
            ResultTextBlock.Text = "Color Changed to " + color;
            ForegroundFlyout.Content = currentForeground;
            CalculatorFlyout.Content = currentBackground;
            await settingsManager.ChangeSettings(currentForeground, currentBackground);
        }

        /// <summary>
        /// Clears the user's history.
        /// </summary>
        private async void ClearYesButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryManager historyManager = new HistoryManager(currentUser.username);
            await historyManager.Clear();
            ClearFlyout.Hide();
        }

        /// <summary>
        /// Does not clear the user's history.
        /// </summary>
        private void ClearNoButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFlyout.Hide();
        }

        /// <summary>
        /// Converts the given hexcode to a color name.
        /// </summary>
        /// <param name="code">Hexcode</param>
        /// <returns>Color name</returns>
        private string NumberToColor(string code)
        {
            if (code == "#FF000000")
            {
                return "Black";
            }
            else if (code == "#FFD3D3D3")
            {
                return "LightGray";
            }
            else if (code == "#FFFF0000")
            {
                return "Red";
            }
            else if (code == "#FFFFA500")
            {
                return "Orange";
            }
            else if (code == "#FFFFFF00")
            {
                return "Yellow";
            }
            else if (code == "#FF008000")
            {
                return "Green";
            }
            else if (code == "#FF0000FF")
            {
                return "Blue";
            }
            else if (code == "#FF4B0082")
            {
                return "Indigo";
            }
            else if (code == "#FFEE82EE")
            {
                return "Violet";
            }
            return code;
        }

        /// <summary>
        /// Changes the calculator buttons' foreground or background to the given color.
        /// </summary>
        /// <param name="location">Foreground or background</param>
        /// <param name="color">Color to change to</param>
        private void SettingsChange(string location, string color)
        {
            if (location == "Foreground")
            {
                currentForeground = color;
            }
            else
            {
                currentBackground = color;
            }
            CalculatorStyleChanger(color);
        }
    }
}
