using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Windows.Input;
using System.Runtime.Serialization;
using Goals.Commands;

namespace Goals.DataModel
{
    public class Goal
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Priority { get; set; }

        [IgnoreDataMember]
        public ICommand DeleteCommand { get; set; }

        public Goal()
        {
            DeleteCommand = new DeleteButtonClick();
        }
    }

    public class DataSource
    {
        private ObservableCollection<Goal> _goals;

        const string fileName = "goals.json";

        public static string FileName
        {
            get
            {
                return fileName;
            }
        }

        public async Task<ObservableCollection<Goal>> GetGoals()
        {
            await ensureDataLoaded();
            return _goals;
        }

        public DataSource()
        {
            _goals = new ObservableCollection<Goal>();
        }

        private async Task ensureDataLoaded()
        {
            if (_goals.Count == 0)
                await getGoalDataAsync();

            return;
        }

        private async Task getGoalDataAsync()
        {
            if (_goals.Count != 0)
                return;

            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Goal>));
            //Attempting to find the file containing saved goals. If it isn't found, create a new file.
            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileName))
                {
                    _goals = (ObservableCollection<Goal>)jsonSerializer.ReadObject(stream);
                }
            }
            catch
            {
                _goals = new ObservableCollection<Goal>();
            }
        }

        //This handles adding the goals to the json file, and giving each property it's value.
        public async void AddGoal(string category, string date, string priority, string name)
        {
            var goal = new Goal();
            goal.Category = category;
            goal.Name = name;
            goal.Date = date;
            goal.Priority = priority;

            _goals.Add(goal);
            await saveGoalDataAsync();
        }

        private async Task saveGoalDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Goal>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(fileName,
                CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _goals);
            }
        }

        public async void RemoveGoal(Goal goal)
        {
            int index = _goals.IndexOf(goal);
            _goals.Remove(goal);
            await saveGoalDataAsync();
        }
    }
}