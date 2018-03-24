using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using Windows.UI.Xaml.Input;

namespace Goals
{
    public sealed partial class Finances : Page
    {
        public Finances()
        {
            this.InitializeComponent();
        }
        private bool attainable;

        public string financeCategory;
        private int monthlyIncome;
        private int totalPlannedExpenditure = 0;
        private int counter = 0;
        private int percentCount = 0;
        private int percentChance;
        List<int> categoryPlans = new List<int>();
        Dictionary<string, bool> smartObjectives = new Dictionary<string, bool>();

        //List holding all the different types of categories. These are default options.
        List<string> categories = new List<string>() { "Gifts", "Clothes", "Shopping", "Eating out", "Sports and leisure" };

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //All the categories in the list are added to the Menu Flyout as new Menu Flyout Items when the page is navigated to.
            foreach (string category in categories)
            {
                MenuFlyoutItem item = new MenuFlyoutItem();
                item.Text = category;
                item.Click += new RoutedEventHandler(MenuFlyoutItem_Click);
                categoryMenuFlyout.Items.Add(item);
            }
        }

        public void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem selectedItem = sender as MenuFlyoutItem;

            if (selectedItem != null)
            {

                //Creating a new text block for the category name.
                TextBlock categoryName = new TextBlock();
                categoryName.Text = selectedItem.Text.ToString();
                categoryName.FontSize = 18;

                //Creating a new text box where the user will input their budget for that category.
                TextBox setBudgetBox = new TextBox();
                setBudgetBox.Width = 100;
                setBudgetBox.Text = 0.ToString();
                setBudgetBox.Name = "setBudgetBox" + counter.ToString();

                //Making it so the user can only input numbers. Add 1 to the counter keeping track of the number
                //of text boxes there are.
                InputScope scope = new InputScope();
                InputScopeName name = new InputScopeName();
                name.NameValue = InputScopeNameValue.Number;
                scope.Names.Add(name);
                setBudgetBox.InputScope = scope;
                counter += 1;

                //Displaying these on the page.
                TextBoxGrid.Children.Add(categoryName);
                TextBoxGrid.Children.Add(setBudgetBox);

                //Remove the selected item from the list of categories.
                categories.Remove(selectedItem.Text.ToString());

                //Clear all the existing items from the Menu Flyout, keeping the items in the list though This is to remove the
                //one that has been selected, as you cannot remove them individually.
                categoryMenuFlyout.Items.Clear();

                //Re-add the categories to the Menu Flyout, minus the one selected previously which is no longer in the list.
                foreach (string category in categories)
                {
                    MenuFlyoutItem item = new MenuFlyoutItem();
                    item.Text = category;
                    item.Click += new RoutedEventHandler(MenuFlyoutItem_Click);
                    categoryMenuFlyout.Items.Add(item);
                }
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            monthlyIncome = int.Parse(monthlyIncomeBox.Text);
            //Reset/clear all the lists, arrays, dictionaries, vairables in case this is not the first time the button has been pressed.
            smartObjectives.Clear();
            categoryPlans.Clear();
            totalPlannedExpenditure = 0;
            percentCount = 0;
            smartPanel.Children.Clear();

            foreach (UIElement child in TextBoxGrid.Children)
            {
                if (child is TextBox)
                {
                    categoryPlans.Add(int.Parse(((TextBox)child).Text));
                }
            }

            //Get the sum of the numbers in the array.
            foreach (int number in categoryPlans)
            {
                totalPlannedExpenditure += number;
            }

            //If the user is not accounting for at least 2 aspects of their life, their goal is not specific enough.
            if (counter < 2)
                smartObjectives.Add("Specific", false);
            else
                smartObjectives.Add("Specific", true);

            //Since the values are all integers, the goal will be measureable.
            smartObjectives.Add("Measurable", true);

            //If they want to spend more than they have, their goal is not attainable.
            if (totalPlannedExpenditure > monthlyIncome)
            {
                smartObjectives.Add("Attainable", false);
                attainable = false;
            }          
            else
            {
                smartObjectives.Add("Attainable", true);
                attainable = true;
            }

            //Realistic is slightly different. Assuming that 10% of the user's income will go towards unforseen costs like car repairs, 
            //if the planned expenditure is more than 90% of the income, it is not considered to be realistic.
            if (totalPlannedExpenditure > (monthlyIncome * 0.9))
                smartObjectives.Add("Realistic", false);
            else
                smartObjectives.Add("Realistic", true);

            //If the goal is attainable within the month, it is therefore time-bound.
            if (attainable == true)
                smartObjectives.Add("Time bound", true);
            else
                smartObjectives.Add("Time bound", false);

            Button closeButton = new Button();
            closeButton.Click += new RoutedEventHandler(closeButton_Click);
            closeButton.Content = "Close";

            foreach (KeyValuePair<string, bool> pair in smartObjectives)
            {
                if (pair.Value == false)
                {
                    //This text block will say which objective hasn't been met.
                    TextBlock feedback = new TextBlock();
                    feedback.Width = 200;
                    feedback.HorizontalAlignment = HorizontalAlignment.Left;
                    feedback.FontSize = 14;

                    //This text box will detail how to meet that objective.
                    TextBlock feedbackDetails = new TextBlock();
                    feedbackDetails.Width = 200;
                    feedbackDetails.TextAlignment = TextAlignment.Left;
                    feedbackDetails.TextWrapping = TextWrapping.Wrap;
                    feedbackDetails.FontSize = 14;

                    //This box will ensure that there is a gap between all the text boxes.
                    TextBlock blankBox = new TextBlock();
                    blankBox.Height = 20;
                    blankBox.Width = 380;

                    feedback.Text = pair.Key.ToString() + " = " + pair.Value.ToString();
                    smartPanel.Children.Add(feedback);
                    if (pair.Key == "Specific")
                    {
                        feedbackDetails.Text = "To make the goal specific, add budget plans for at least 2 categories.";
                        smartPanel.Children.Add(feedbackDetails);
                        smartPanel.Children.Add(blankBox);
                    }
                    else if (pair.Key == "Attainable")
                    {
                        feedbackDetails.Text = "To make the goal attainable, make sure the planned expenditure is"+
                            " not more than your monthly income.";
                        smartPanel.Children.Add(feedbackDetails);
                        smartPanel.Children.Add(blankBox);
                    }
                    else if (pair.Key == "Realistic")
                    {
                        feedbackDetails.Text = "To make the goal realistic, make sure the planned expenditure is less than" +
                            " 90% of your income to help account for unexpected costs like car repairs.";
                        smartPanel.Children.Add(feedbackDetails);
                        smartPanel.Children.Add(blankBox);
                    }
                    else if (pair.Key == "Time bound")
                    {
                        feedbackDetails.Text = "To make your goal time bound, make sure it is attainable.";
                        smartPanel.Children.Add(feedbackDetails);
                        smartPanel.Children.Add(blankBox);
                    }
                }               
                else
                    percentCount += 1;
            }

            percentChance = (percentCount * 20);
            if (percentChance == 100)
                Frame.Navigate(typeof(MainPage));
            else
            {
                smartPanel.Children.Add(closeButton);
                smartPopup.Visibility = Visibility.Visible;
            }
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            newCategoryGrid.Visibility = Visibility.Visible;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            smartPopup.Visibility = Visibility.Collapsed;
        }

        private void newCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlock categoryName = new TextBlock();
            categoryName.Text = newCategoryTextBox.Text.ToString();
            categoryName.FontSize = 18;

            TextBox setBudgetBox = new TextBox();
            setBudgetBox.Width = 100;
            setBudgetBox.Name = "setBudgetBox" + counter.ToString();

            InputScope scope = new InputScope();
            InputScopeName name = new InputScopeName();
            name.NameValue = InputScopeNameValue.Number;
            scope.Names.Add(name);
            setBudgetBox.InputScope = scope;
            counter += 1;

            TextBoxGrid.Children.Add(categoryName);
            TextBoxGrid.Children.Add(setBudgetBox);

            newCategoryGrid.Visibility = Visibility.Collapsed;
        }
    }
}