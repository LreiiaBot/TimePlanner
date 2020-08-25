using System.Collections.ObjectModel;

namespace TimePlannerUpdated.Default
{
    public class MainViewModel
    {
        public TaskList SelectedList { get; set; }
        public ObservableCollection<Reminder> Reminders { get; set; } = new ObservableCollection<Reminder>();
        public ObservableCollection<TaskList> Lists { get; set; } = new ObservableCollection<TaskList>();

        public MainViewModel()
        {
            LoadList();

            Update();
        }

        private void LoadList()
        {
            // ToDo AddReminder should maybe be moved to TimeControlledElement - then only add this way, the parent has to be right OR BETTER MAYBE OBSERVABLECOLLECTION OR SOMETHING SIMILAR!! THIS WOULD BE SOOOO COOOOL

            var tl = new TaskList("DailyToDos", "disc");
            tl.Tasks.Add(new UserTask("Essen", "Essen kaufen und dann super"));

            Lists.Add(tl);

            SelectedList = tl;
        }

        private void Update()
        {
            // ToDo check if this works
            var reminderList = SelectedList?.GetAllReminders(false);
            reminderList.Sort(new ReminderSorter());
            Reminders = reminderList.Convert();

            // ToDo maybe order lists
        }
    }
}
