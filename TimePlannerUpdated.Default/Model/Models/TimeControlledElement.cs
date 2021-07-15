using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TimePlannerUpdated.Default
{
    // ToDo maybe console version can modify this class by adding print method - hope this wont change the wpf solution
    public abstract partial class TimeControlledElement : IPersistence
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartingTime { get; set; }
        public TimeSpan AutoOffset { get; set; } = TimeSpan.Zero;

        public int MinimalAutoRemindersCount { get; set; }

        public List<Reminder> AutoReminders { get; set; } = new List<Reminder>();
        public List<Reminder> AutoOffsetReminders { get; set; } = new List<Reminder>();
        public ObservableCollection<Reminder> CustomReminders { get; set; } = new ObservableCollection<Reminder>();

        public int AutoAddMinutes { get; set; }
        public int AutoAddHours { get; set; }
        public int AutoAddDays { get; set; }
        public int AutoAddMonths { get; set; }
        public int AutoAddYears { get; set; }
        // ToDo maybe in setter of autoadd... automatically call addnewAutoreminders

        public List<Reminder> PendingReminders
        {
            get
            {
                return GetAllReminders(false);
            }
        }


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
            if (AutoAddMinutes > 0 || AutoAddHours > 0 || AutoAddDays > 0 || AutoAddMonths > 0 || AutoAddYears > 0)
            {
                while (AutoReminders.Count < MinimalAutoRemindersCount)
                {
                    var nextTime = GetNextRemindTime();

                    // AutoReminder
                    var reminder = new Reminder(this, nextTime);
                    AutoReminders.Add(reminder);

                    // AutoReminderWithOffset
                    if (!AutoOffset.Equals(TimeSpan.Zero))
                    {
                        var offsetReminder = new Reminder(this, nextTime.Add(AutoOffset));
                        offsetReminder.IsOffsetReminder = true;
                        AutoOffsetReminders.Add(offsetReminder);
                    }

                    StartingTime = nextTime;
                }
            }
        }

        public void SetAutoAddTimes(int minutes, int hours, int days, int months, int years)
        {
            AutoAddMinutes = minutes;
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
            var reminders = AutoReminders.Concat(AutoOffsetReminders).Concat(CustomReminders).ToList();
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
            SetAutoAddTimes(0, 0, 1, 0, 0);
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

        public override string ToString()
        {
            string str = Title;
            if (String.IsNullOrWhiteSpace(str))
            {
                str = Description;
            }
            return str;
        }

        public virtual string ToCsv()
        {
            return $"{Title}"; // TODO
            // Children overrwrite this method by using base() and then adding things maybe
        }

        public virtual void ToObject(string csv)
        {
            throw new NotImplementedException(); // TODO
        }
    }
}
