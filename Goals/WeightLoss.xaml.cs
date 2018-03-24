using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Goals
{
    public sealed partial class WeightLoss : Page
    {
        private double initialMeasurement;
        private double targetMeasurement;
        private double difference;
        private double onePercent;
        private float remainingWeeks;
        private float remainingDays;
        private string loseOrGain;
        public DateTimeOffset targetDate;

        public WeightLoss()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var passedData = e.Parameter as PassedData;
            targetDate = passedData.TargetDate;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Perform the calculations to see if it is SMART.
            //Display this information on the main page.

            //Saving the measurements.
            initialMeasurement = double.Parse(initialTextBox.Text);
            targetMeasurement = double.Parse(targetTextBox.Text);

            //Saves the difference between the initial weight and target, 
            //taking into account whether the user wants to gain or to lose weight.
            if (decendingButton.IsChecked == true)
            {
                difference = initialMeasurement - targetMeasurement;
                loseOrGain = "lose";
            }

            if (ascendingButton.IsChecked == true)
            {
                difference = targetMeasurement - initialMeasurement;
                loseOrGain = "gain";
            }

            onePercent = initialMeasurement / 100;

            //How many days and weeks are left
            remainingDays = targetDate.DateTime.Subtract(DateTime.Today).Days;
            remainingWeeks = remainingDays / 7;

            //One percent of the user's weight can be lost each week. Find how much they want to lose.
            //If this is bigger than one percent per week then it is too much.

            if (difference > (onePercent * remainingWeeks))
                tooMuchWeight.Text = "You are trying to " + loseOrGain + " too much weight. "
               + "Maximum healthy weight loss in this time span: " + (onePercent * remainingWeeks).ToString();

            else
                Frame.Navigate(typeof(MainPage));

        }
    }
}