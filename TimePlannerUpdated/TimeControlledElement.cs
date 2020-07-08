﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TimePlannerUpdated
{
    abstract class TimeControlledElement : IPrintableElement
    {
        public DateTimeOffset StartingTime { get; set; }

        public int MinimalAutoRemindersCount { get; set; }

        private List<Reminder> autoReminders = new List<Reminder>();
        private List<Reminder> customReminders = new List<Reminder>();

        public int AutoAddHours { get; set; }
        public int AutoAddDays { get; set; }
        public int AutoAddMonths { get; set; }
        public int AutoAddYears { get; set; }

        public int SpaceForTitle { get; set; }
        public int SpaceBetween { get; set; }
        public int SpaceForTitleAndDescription { get; set; }

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
            // only if startingtime is "set" and at least one of hours days and months years is > 0
            if (AutoAddHours > 0 || AutoAddDays > 0 || AutoAddMonths > 0 || AutoAddYears > 0)
            {
                while (autoReminders.Count < MinimalAutoRemindersCount)
                {
                    var nextTime = GetNextRemindTime();
                    var reminder = new Reminder(this, nextTime);
                    autoReminders.Add(reminder);
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
            var reminders = autoReminders.Concat(customReminders).ToList();
            if (!withDone)
            {
                reminders = reminders.Where((item) => !item.Done).ToList();
            }
            reminders.Sort(new ReminderSorter());
            return reminders;
        }

        protected virtual void SetDefaultValues()
        {
            // Set defaultvalues maybe of [json] file
            MinimalAutoRemindersCount = 3;
            SetAutoAddTimes(0, 1, 0, 0);
            StartingTime = DateTimeOffset.Now;


            SpaceForTitle = 10;
            SpaceForTitleAndDescription = 30;
            SpaceBetween = 1;
        }

        public abstract void Print(bool enter);

        protected virtual void PrintReminders(bool withDone)
        {
            foreach (var reminder in GetAllReminders(withDone))
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
                element.Print(true);
            }
        }

        public static void PrintWithReminders(IEnumerable<TimeControlledElement> elements, bool withDone = true)
        {
            foreach (var element in elements)
            {
                element.Print(true);
                element.PrintReminders(withDone);
            }
        }
    }
}
