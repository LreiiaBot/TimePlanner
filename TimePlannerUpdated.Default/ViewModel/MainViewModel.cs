using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace TimePlannerUpdated.Default
{
    public class MainViewModel : BaseModel
    {
        #region Mvvm Events

        public event EventHandler<MvvmMessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<MvvmDialogEventArgs> DialogCreateRequest;
        public event EventHandler<EventArgs> DialogCloseRequest;

        public event EventHandler<bool> EnableInputRequest;

        #endregion

        #region Mvvm Commands

        public ICommand OnMouseDoubleClickCommand { get; set; }
        public ICommand OnTitleAddCommand { get; set; }
        public ICommand OnOpenListsCommand { get; set; }
        public ICommand OnButtonAddListCommand { get; set; }

        #endregion

        #region Members

        private TaskList selectedList;
        public TaskList SelectedList
        {
            get
            {
                return selectedList;
            }
            set
            {
                selectedList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Reminder> reminders = new ObservableCollection<Reminder>();
        public ObservableCollection<Reminder> Reminders
        {
            get
            {
                return reminders;
            }
            set
            {
                reminders = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<TaskList> lists = new ObservableCollection<TaskList>();
        public ObservableCollection<TaskList> Lists
        {
            get
            {
                return lists;
            }
            set
            {
                lists = value;
                OnPropertyChanged();
            }
        }
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }


        public bool inOverViewWindow = true;

        public TimeControlledElement Selected { get; set; } = new TaskList("Food", "Yummy");

        #endregion

        public MainViewModel()
        {
            SetupCommands();
            LoadList();

            Update();
        }

        private void SetupCommands()
        {
            OnMouseDoubleClickCommand = new ActionCommand(OnMouseDoubleClick);
            OnTitleAddCommand = new ActionCommand(OnTitleAdd);
            OnOpenListsCommand = new ActionCommand(OnOpenLists);
            OnButtonAddListCommand = new ActionCommand(OnButtonAddList);
        }

        #region Mvvm Command Methods
        private void OnButtonAddList(object obj)
        {
            TaskList createdList = new TaskList("new List", "description");
            Lists.Add(createdList);
            SelectedList = createdList;

            EnableInputRequest(this, true);
        }
        private void OnOpenLists(object obj)
        {
            inOverViewWindow = false;
            RequestDialog(DialogType.ViewLists);
        }

        private void OnTitleAdd(object obj)
        {
            if (!String.IsNullOrWhiteSpace(Title))
            {
                SelectedList.Tasks.Add(new UserTask(Title.Trim()));

                Title = String.Empty;
                Update();
            }
        }

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
            Lists = TaskList.ReadAll().Convert();
            SelectedList = Lists.FirstOrDefault();
        }

        private void Update()
        {
            // ToDo check if this works
            List<Reminder> reminderList = new List<Reminder>();// = SelectedList?.GetAllReminders(false);
            foreach (var list in Lists)
            {
                reminderList.AddRange(list.GetAllReminders(false));
            }
            reminderList.Sort(new ReminderSorter());

            Reminders = reminderList.Convert();

            // ToDo maybe order lists
            TaskList.SaveAll(Lists);
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
