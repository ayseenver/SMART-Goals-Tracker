using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Goals
{ 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var goals = await App.DataModel.GetGoals();
            this.DataContext = goals;
        }

        private void AddGoal_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddGoal));
        }

        private void updateDetails_Click(object sender, RoutedEventArgs e)
        {
            //Make this so it takes you to the correct update details page.
        }

        private async void deleteGoal_Click(object parameter)
        {
            Grid2.Children.Clear();
            var goals = await App.DataModel.GetGoals();
            this.DataContext = goals;
        }
    }
}
