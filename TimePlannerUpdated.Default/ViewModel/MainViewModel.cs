using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TimePlannerUpdated.Default
{
    public class MainViewModel
    {
        #region Mvvm Events

        public event EventHandler<MvvmMessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<MvvmDialogEventArgs> DialogCreateRequest;
        public event EventHandler<EventArgs> DialogCloseRequest;

        #endregion

        #region Mvvm Commands

        public ICommand OnMouseDoubleClickCommand { get; set; }

        #endregion

        public TaskList SelectedList { get; set; }
        public ObservableCollection<Reminder> Reminders { get; set; } = new ObservableCollection<Reminder>();
        public ObservableCollection<TaskList> Lists { get; set; } = new ObservableCollection<TaskList>();

        public MainViewModel()
        {
            SetupCommands();
            LoadList();

            Update();
        }

        private void SetupCommands()
        {
            OnMouseDoubleClickCommand = new ActionCommand(OnMouseDoubleClick);
        }

        #region Mvvm Command Methods

        private void OnMouseDoubleClick(object obj)
        {
            RequestMessageBox(ClickedYesOrNo, "test?", String.Empty, false);
        }

        private void ClickedYesOrNo(bool ok)
        {
            string response = ok ? "ok" : "no";
            RequestMessageBox(null, response);
        }

        #endregion

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

        private void RequestMessageBox(Action<bool> resultAction, string messageBoxText, string caption = "", bool onlyOk = true)
        {
            this.MessageBoxRequest?.Invoke(this, new MvvmMessageBoxEventArgs(resultAction, messageBoxText, caption, onlyOk));
        }

        private void RequestDialog(DialogType type, Action actionAfterCreate = null)
        {
            this.DialogCreateRequest?.Invoke(this, new MvvmDialogEventArgs(type, actionAfterCreate));
        }
    }
}
