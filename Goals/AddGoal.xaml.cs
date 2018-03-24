using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Goals
{

    public sealed partial class AddGoal : Page
    {
        private string date;
        private string priority;
        private string category;
        public string name;
        public DateTimeOffset targetDate;

        public AddGoal()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
        }

        public void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Sets the priority
            if (radioButtonLow.IsChecked == true)
                priority = "Low";
            else if (radioButtonMedium.IsChecked == true)
                priority = "Medium";
            else if (radioButtonHigh.IsChecked == true)
                priority = "High";
            else
                priority = "Not set";

            //Sets the category
            category = comboBox.SelectionBoxItem.ToString();

            //Sets the target date
            date = DatePicker1.Date.ToString();
            targetDate = DatePicker1.Date.DateTime;

            //Create new instance of the AddGoal class, populated with the information the user inputted.
            App.DataModel.AddGoal(category,
                "Target date: " + DatePicker1.Date.ToString("dd-MM-yyyy"),
                "Priority: " + priority, name);

            //Navigates to the relevant page.
            if (category == "Weight loss / gain")
            {
                Frame.Navigate(typeof(WeightLoss), new PassedData { TargetDate = targetDate.DateTime });
            }

            else if (category == "Finances")
            {
                Frame.Navigate(typeof(Finances));
            }
        }

        //Doesn't allow the user to input a target date that has already passed.
        private void DatePicker1_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            int result = DateTime.Compare(DateTime.Today, DatePicker1.Date.DateTime);

            if (result > 0)
                SaveButton.IsEnabled = false;
            if (result <= 0)
                SaveButton.IsEnabled = true;
        }

        private void comboBox_DropDownOpened(object sender, object e)
        {
        }

        private void nameTextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            name = nameTextBox.Text;
        }
    }
}