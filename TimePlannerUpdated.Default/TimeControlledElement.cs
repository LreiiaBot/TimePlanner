using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TimePlannerUpdated.Default
{
    // ToDo maybe console version can modify this class by adding print method - hope this wont change the wpf solution
    public abstract partial class TimeControlledElement
    {
        public string Title { get; set; }
        public DateTimeOffset StartingTime { get; set; }

        public int MinimalAutoRemindersCount { get; set; }

        public List<Reminder> AutoReminders { get; set; } = new List<Reminder>();
        public ObservableCollection<Reminder> CustomReminders { get; set; } = new ObservableCollection<Reminder>();

        public int AutoAddHours { get; set; }
        public int AutoAddDays { get; set; }
        public int AutoAddMonths { get; set; }
        public int AutoAddYears { get; set; }

        public TimeControlledElement()
        {
            Init();
        }

        protected virtual void Init()
        {
            SetupEvents();
            SetDefaultValues();
            AddNewAutoReminders();
        }

        public void AddNewAutoReminders()
        {
            // only if startingtime is "set" and at least one of hours days and months years is > 0
            if (AutoAddHours > 0 || AutoAddDays > 0 || AutoAddMonths > 0 || AutoAddYears > 0)
            {
                while (AutoReminders.Count < MinimalAutoRemindersCount)
                {
                    var nextTime = GetNextRemindTime();
                    var reminder = new Reminder(this, nextTime);
                    AutoReminders.Add(reminder);
                    StartingTime = nextTime;
                }
            }
        }

        public void SetAutoAddTimes(int hours, int days, int months, int years)
        {
            AutoAddHours = hours;
            AutoAddDays = days;
            AutoAddMonths = months;
            AutoAddYears = years;
        }

        protected virtual DateTimeOffset GetNextRemindTime()
        {
            var nextTime = StartingTime
                .AddHours(AutoAddHours)
                .AddDays(AutoAddDays)
                .AddMonths(AutoAddMonths)
                .AddYears(AutoAddYears);
            return nextTime;
        }

        public virtual List<Reminder> GetAllReminders(bool withDone)
        {
            var reminders = AutoReminders.Concat(CustomReminders).ToList();
            if (!withDone)
            {
                reminders = reminders.Where((item) => !item.Done).ToList();
            }
            return reminders;
        }

        protected virtual void SetDefaultValues()
        {
            // Set defaultvalues maybe of [json] file
            MinimalAutoRemindersCount = 3;
            SetAutoAddTimes(0, 1, 0, 0);
            StartingTime = DateTimeOffset.Now;

            Title = "unknow";
        }

        protected virtual void SetupEvents()
        {
            CustomReminders.CollectionChanged += OnCustomReminderAdded;
        }

        protected virtual void OnCustomReminderAdded(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (Reminder item in e.NewItems)
            {
                item.Parent = this;
            }
        }
    }
}
