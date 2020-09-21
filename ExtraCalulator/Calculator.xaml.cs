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
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using System.ComponentModel;

namespace ExtraCalulator
{
    /// <summary>
    /// Class <c>Calculator</c> performs 4-function calculator operations.
    /// Values being calculated cannot exceed 999,999,999, and appending a number to a value that
    /// would do so will result in no change. Results that exceed 999,999,999 will be displayed,
    /// and caluclated as 999,999,999, but will be recorded in the user's history as the actual 
    /// result. Resulting decimals with over 9 digits will be only be displayed to the 9th digit,
    /// but will otherwise calculate the same.
    /// </summary>
    public sealed partial class Calculator : Page
    {
        /// <summary>
        /// Maximum value that can be represented by any value or result.
        /// </summary>
        private readonly int MAX_VALUE = 999999999;
        /// <summary>
        /// Current operator ["+", "-", "*", "/", and "" (no operation)] between <c>value1</c> and
        /// <c>value2</c>.
        /// </summary>
        private string operators = "";
        /// <summary>
        /// Value of the first variable in a basic algebraic operation or the result if 
        /// <c>ansBeingDisplayed = true</c>.
        /// </summary>
        private double value1 = 0.0;
        /// <summary>
        /// Value of the second variable in a basic algebraic operation.
        /// </summary>
        private double value2 = 0.0;
        /// <summary>
        /// Is true if <c>value1</c> is being used in the current operation, false otherwise.
        /// </summary>
        private bool usingValue1 = false;
        /// <summary>
        /// Is true if <c>value2</c> is being used in the current operation, false otherwise.
        /// </summary>
        private bool usingValue2 = false;
        /// <summary>
        /// Is true if <c>CalulatorResultTextBox</c> is currently desplaying the result of the 
        /// previous operation, false otherwise.
        /// </summary>
        private bool resultDisplayed = false;
        /// <summary>
        /// Stores the history of each calculated operation.
        /// </summary>
        private HistoryManager historyManager;

        /// <summary>
        /// Initializes <c>Calculator</c>.
        /// </summary>
        public Calculator()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the page is nagivated to, and initializes the user's data.
        /// </summary>
        /// <param name="e">CurrentUser object</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentUser currentUser = (CurrentUser)e.Parameter;
            string username = currentUser.username;
            historyManager = new HistoryManager(username);
            await historyManager.LoadHistory();
        }

        /// <summary>
        /// Appends 0 to the end of the current value being displayed.
        /// <code>value = value * 10</code>
        /// </summary>
        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(0);
        }

        /// <summary>
        /// Appends 1 to the end of the current value being displayed.
        /// <code>value = value * 10 + 1</code>
        /// </summary>
        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(1);
        }

        /// <summary>
        /// Appends 2 to the end of the current value being displayed.
        /// <code>value = value * 10 + 2</code>
        /// </summary>
        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(2);
        }
        
        /// <summary>
        /// Appends 3 to the end of the current value being displayed.
        /// <code>value = value * 10 + 3</code>
        /// </summary>
        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(3);
        }

        /// <summary>
        /// Appends 4 to the end of the current value being displayed.
        /// <code>value = value * 10 + 4</code>
        /// </summary>
        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(4);
        }

        /// <summary>
        /// Appends 5 to the end of the current value being displayed.
        /// <code>value = value * 10 + 5</code>
        /// </summary>
        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(5);
        }

        /// <summary>
        /// Appends 6 to the end of the current value being displayed.
        /// <code>value = value * 10 + 6</code>
        /// </summary>
        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(6);
        }

        /// <summary>
        /// Appends 7 to the end of the current value being displayed.
        /// <code>value = value * 10 + 7</code>
        /// </summary>
        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(7);
        }

        /// <summary>
        /// Appends 8 to the end of the current value being displayed.
        /// <code>value = value * 10 + 8</code>
        /// </summary>
        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(8);
        }

        /// <summary>
        /// Appends 9 to the end of the current value being displayed.
        /// <code>value = value * 10 + 9</code>
        /// </summary>
        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumber(9);
        }

        /// <summary>
        /// If there does not exist a second value in the current operation, removes the "+" 
        /// operator if the current operator is "+", adds/changes the operator to "+" otherwise.
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewOperator("+");

        }

        /// <summary>
        /// If there does not exist a second value in the current operation, removes the "-" 
        /// operator if the current operator is "-", adds/changes the operator to "-" otherwise.
        /// </summary>
        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            NewOperator("-");
        }

        /// <summary>
        /// If there does not exist a second value in the current operation, removes the "*" 
        /// operator if the current operator is "*", adds/changes the operator to "*" otherwise.
        /// </summary>
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            NewOperator("*");
        }

        /// <summary>
        /// If there does not exist a second value in the current operation, removes the "/" 
        /// operator if the current operator is "/", adds/changes the operator to "/" otherwise.
        /// </summary>
        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            NewOperator("/");
        }

        /// <summary>
        /// Clears the newest value of the current operation, including the operator if there only
        /// exists one value.
        /// </summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // If value2 is being used (is the newest), does not clear value1
            if (!usingValue2)
            {
                value1 = 0.0;
                usingValue1 = false;
                resultDisplayed = false;
                operators = "";
                CalculatorOperatorTextBox.Text = operators;
                CalculatorPreviousNumberTextBlock.Text = "";
            }
            value2 = 0.0;
            usingValue2 = false;
            CalculatorResultTextBlock.Text = "";
        }

        /// <summary>
        /// Solves the current operation if possible, saving the operation in the user's history
        /// and setting the result as the first value of a new operation with no operator.
        /// If not possible, displayes DNE, and resets all operation values/operators.
        /// </summary>
        private async void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            // If there is an equation to solve
            if (usingValue1 && usingValue2 && operators.Length != 0)
            {
                double result = 0.0;
                // If the equation isn't a number divided by 0
                Boolean exists = true;
                if (operators == "+")
                {
                    result = value1 + value2;
                }
                else if (operators == "-")
                {
                    result = value1 - value2;
                }
                else if (operators == "*")
                {
                    result = value1 * value2;
                }
                else
                {
                    if (value2 == 0)
                    {
                        exists = false;
                    }
                    else
                    {
                        result = value1 / value2;
                    }
                }

                // Attempt at recording the user's location for thier history
                // External source: https://www.dotnetperls.com/switch
                // Currently does not work / cannot get permissions to allow location access
                var accessStatus = await Geolocator.RequestAccessAsync();
                // If device location is allowed
                bool allowed = true;
                switch (accessStatus)
                {
                    case GeolocationAccessStatus.Allowed:
                        allowed = true;
                        break;
                    case GeolocationAccessStatus.Denied:
                        allowed = false;
                        break;
                }
                string longitude = "Not Availible";
                string latitude = "Not Availible";
                if (allowed)
                {
                    // External source: 
                    // https://docs.microsoft.com/en-us/windows/uwp/maps-and-location/get-location
                    Geolocator geolocator = new Geolocator();
                    Geoposition geoposition = await geolocator.GetGeopositionAsync();
                    latitude = "" + geoposition.Coordinate.Point.Position.Latitude;
                    longitude = "" + geoposition.Coordinate.Point.Position.Longitude;
                }

                if (exists)
                {
                    await historyManager.AddHistory(value1, operators, value2, "" + result, 
                        longitude, latitude);
                    DisplayResult(result);
                }
                else
                {
                    await historyManager.AddHistory(value1, operators, value2, "Does Not Exist", longitude, latitude);
                    CalculatorResultTextBlock.Text = "DNE";
                    value1 = 0;
                    value2 = 0;
                    usingValue1 = false;
                    usingValue2 = false;
                    CalculatorPreviousNumberTextBlock.Text = "";
                    operators = "";
                    CalculatorOperatorTextBox.Text = "";
                }
            }
        }

        /// <summary>
        /// Appends <paramref name="number"/> to the current value. If a result is being displayed
        /// and there exists an operator, the result becomes the first value and 
        /// <paramref name="number"/> begins the second value. If there does not exist an operator,
        /// begin a new operation and <paramref name="number"/> begins the first value.
        /// </summary>
        /// <param name="number">Number to be appended</param>
        private void AddNumber(double number)
        {
            if (resultDisplayed)
            {
                if (operators.Length != 0)
                {
                    usingValue2 = true;
                    if (value2 * 10 + number <= MAX_VALUE)
                    {
                        CalculatorPreviousNumberTextBlock.Text = "" + value1;
                        value2 = value2 * 10 + number;
                        CalculatorResultTextBlock.Text = "" + value2;
                    }
                }
                else
                {
                    resultDisplayed = false;
                    usingValue1 = true;
                    usingValue2 = false;
                    value1 = number;
                    value2 = 0;
                    CalculatorResultTextBlock.Text = "" + value1;
                    CalculatorPreviousNumberTextBlock.Text = "";
                    operators = "";
                }
            }
            else
            {
                if (operators.Length == 0)
                {
                    usingValue1 = true;
                    if (value1 * 10 + number <= MAX_VALUE)
                    {
                        value1 = value1 * 10 + number;
                        CalculatorResultTextBlock.Text = "" + value1;
                    }
                }
                else
                {
                    usingValue2 = true;
                    if (value2 * 10 + number < MAX_VALUE)
                    {
                        CalculatorPreviousNumberTextBlock.Text = "" + value1;
                        value2 = value2 * 10 + number;
                        CalculatorResultTextBlock.Text = "" + value2;
                    }
                }
            }
        }

        /// <summary>
        /// If there does not exist a second value in the current operation, removes the 
        /// "<paramref name="newOp"/>" operator if the current operator is the same, adds/changes
        /// the operator to "<paramref name="newOp"/>" otherwise.
        /// </summary>
        /// <param name="newOp">Selected operator</param>
        private void NewOperator(string newOp)
        {
            if (usingValue1 && !usingValue2)
            {
                if (operators == newOp)
                {
                    operators = "";
                }
                else
                {
                    operators = newOp;
                }
                CalculatorOperatorTextBox.Text = operators;
            }
        }

        /// <summary>
        /// Formats <paramref name="result"/> properly and displays the result.
        /// </summary>
        /// <param name="result"></param>
        private void DisplayResult(double result)
        {
            usingValue2 = false;
            value2 = 0.0;
            CalculatorPreviousNumberTextBlock.Text = "";
            value1 = result;
            CalculatorResultTextBlock.Text = "" + value1;
            if (CalculatorResultTextBlock.Text.Length > 9)
            {
                // If the result exceeds the max value, otherwise there are too many decimals
                if (value1 >= MAX_VALUE)
                {
                    value1 = MAX_VALUE;
                    CalculatorResultTextBlock.Text = "" + value1;
                }
                else
                {
                    CalculatorResultTextBlock.Text = CalculatorResultTextBlock.Text
                        .Substring(0, 10);
                }
            }
            resultDisplayed = true;
            operators = "";
            CalculatorOperatorTextBox.Text = operators;
        }
    }
}
