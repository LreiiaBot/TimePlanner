using System.Collections.Generic;

namespace TimePlannerUpdated.Default
{
    public class TaskList : TimeControlledElement
    {
        public List<UserTask> Tasks { get; set; } = new List<UserTask>();

        public TaskList(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public static List<TaskList> ReadAll()
        {
            return DBTaskList.ReadAll();
        }
        public static void Save(TaskList tasklist)
        {
            DBTaskList.Save(tasklist);
        }
        public static void SaveAll(IEnumerable<TaskList> tasklists)
        {
            DBTaskList.SaveAll(tasklists);
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
            return reminders;
        }

        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
        }
    }
}
