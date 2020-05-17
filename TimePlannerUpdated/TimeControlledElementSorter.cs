using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class TimeControlledElementSorter : IComparer<TimeControlledElement>
    {
        public int Compare(TimeControlledElement x, TimeControlledElement y)
        {
            // Check !null is before null and !done is before done
            var deadlineX = GetNextUndoneReminder(x).Deadline;
            var deadlineY = GetNextUndoneReminder(y).Deadline;

            var ret = 0;

            if (deadlineX < deadlineY)
            {
                ret = -1;
            }
            else if (deadlineX > deadlineY)
            {
                ret = 1;
            }

            return ret;
        }

        private Reminder GetNextUndoneReminder(TimeControlledElement element)
        {
            Reminder min = null;
            var reminders = element.GetAllReminders();

            if (reminders.Count != 0)
            {
                min = reminders[0];
                for (int i = 1; i < reminders.Count; i++)
                {
                    if (!reminders[i].Done && reminders[i].Deadline != null && reminders[i].Deadline < min.Deadline)
                    {
                        min = reminders[i];
                    }
                }
            }
            return min;
        }
    }
}
