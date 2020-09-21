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
    /// Class <c>MainPage</c> navigates to <c>LoginPage</c> when the app is launched.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes <c>MainPage</c> and naviages to <c>LoginPage</c>.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            InnerFrame.Navigate(typeof(LoginPage));
        }
    }
}
