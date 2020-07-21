using System.Collections.Generic;

namespace TimePlannerUpdated.Default
{
    public class TaskList : TimeControlledElement
    {
        public List<UserTask> Tasks { get; set; } = new List<UserTask>();
        public string Description { get; set; }

        public TaskList(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public override List<Reminder> GetAllReminders(bool withDone, bool sort = true)
        {
            var reminders = base.GetAllReminders(withDone);
            foreach (var task in Tasks)
            {
                foreach (var reminder in task.GetAllReminders(withDone))
                {
                    reminders.Add(reminder);
                }
            }
            if (sort)
            {
                reminders.Sort(new ReminderSorter());
            }
            return reminders;
        }

        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
        }
    }
}
