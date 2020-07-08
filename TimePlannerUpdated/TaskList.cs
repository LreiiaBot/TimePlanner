using System;
using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class TaskList : TimeControlledElement
    {
        public List<UserTask> Tasks { get; set; } = new List<UserTask>();
        public string Title { get; set; }
        public string Description { get; set; }

        public TaskList(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public List<Reminder> GetAllTaskReminders(bool withDone)
        {
            var reminderList = new List<Reminder>();
            foreach (var item in Tasks)
            {
                foreach (var reminder in item.GetAllReminders(withDone))
                {
                    reminderList.Add(reminder);
                }
            }
            reminderList.Sort(new ReminderSorter());
            return reminderList;
        }

        public override void Print(bool enter)
        {
            var spaceForDescription = SpaceForTitleAndDescription;

            // If there is a title
            if (!String.IsNullOrWhiteSpace(Title))
            {
                spaceForDescription -= SpaceForTitle;
                spaceForDescription -= SpaceBetween;

                var resultTitle = Title.ToSetLength(SpaceForTitle);
                SetConsoleColor(resultTitle.Item2);
                Console.Write(resultTitle.Item1);

                SetConsoleColor(true);
                string s = String.Empty;
                for (int i = 0; i < SpaceBetween; i++)
                {
                    s += " ";
                }
                Console.Write(s);
            }
            var resultDescription = Description.ToSetLength(spaceForDescription);
            SetConsoleColor(resultDescription.Item2);
            Console.Write(resultDescription.Item1);
            if (enter)
            {
                Console.WriteLine();
            }
        }

        public void PrintWithReminders(bool withDone)
        {
            Print(true);
            PrintReminders(withDone);
        }

        public override List<Reminder> GetAllReminders(bool withDone)
        {
            var reminders = base.GetAllReminders(withDone);
            foreach (var task in Tasks)
            {
                foreach (var reminder in task.GetAllReminders(withDone))
                {
                    reminders.Add(reminder);
                }
            }
            reminders.Sort(new ReminderSorter());
            return reminders;
        }

        private void SetConsoleColor(bool isOk)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Black;
            if (isOk)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
        }

        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
        }
    }
}
