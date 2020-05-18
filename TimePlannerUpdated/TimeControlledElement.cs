using System;
using System.Collections.Generic;
using System.Linq;

namespace TimePlannerUpdated
{
    abstract class TimeControlledElement : PrintableElement
    {
        public DateTime StartingTime { get; set; }

        public int MinimalAutoRemindersCount { get; set; }

        public List<Reminder> AutoReminders { get; set; } = new List<Reminder>();
        private List<Reminder> customReminders = new List<Reminder>();
        // int autoAddDays, autoAddMonths, autoAddYears

        public TimeControlledElement()
        {
            Init();
        }

        protected virtual void Init()
        {
            SetDefaultValues();
            AddNewAutoReminders();
        }

        public void AddNewAutoReminders()
        {
            while (AutoReminders.Count < MinimalAutoRemindersCount)
            {
                var reminder = new Reminder(this);
                // ToDo create new reminders
                AutoReminders.Add(reminder);
            }
        }

        public List<Reminder> GetAllReminders()
        {
            // I have to convince myself that the references are still valid but i guess
            var reminders = AutoReminders.Concat(customReminders).ToList();
            reminders.Sort(new ReminderSorter());
            return reminders;
        }

        protected virtual void SetDefaultValues()
        {
            // Set defaultvalues maybe of [json] file
            MinimalAutoRemindersCount = 5;
        }

        public abstract void Print(bool enter);

        protected virtual void PrintReminders()
        {
            foreach (var reminder in GetAllReminders())
            {
                reminder.Print(true);
            }
        }

        public virtual void AddReminder(Reminder reminder)
        {
            reminder.Parent = this;
            customReminders.Add(reminder);
        }

        public static void Print(IEnumerable<TimeControlledElement> elements)
        {
            foreach (var element in elements)
            {
                element.Print(false);
                Console.WriteLine(" | test");
            }
        }

        public static void PrintWithReminders(IEnumerable<TimeControlledElement> elements)
        {
            foreach (var element in elements)
            {
                element.Print(true);
                element.PrintReminders();
            }
        }
    }
}
