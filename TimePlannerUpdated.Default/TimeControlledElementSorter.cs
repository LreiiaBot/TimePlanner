using System.Collections.Generic;

namespace TimePlannerUpdated.Default
{
    class TimeControlledElementSorter : IComparer<TimeControlledElement>
    {
        public int Compare(TimeControlledElement x, TimeControlledElement y)
        {
            var reminderX = GetNextUndoneReminder(x);
            var reminderY = GetNextUndoneReminder(y);

            var ret = 0;

            if (reminderX == null || reminderY == null)
            {
                if (reminderX != null)
                {
                    ret = -1;
                }
                else if (reminderY != null)
                {
                    ret = 1;
                }
            }
            else if (reminderX.Done == reminderY.Done)
            {
                var deadlineX = reminderX.Deadline;
                var deadlineY = reminderY.Deadline;
                if (deadlineX < deadlineY)
                {
                    ret = -1;
                }
                // would == check the referece?
                else if (deadlineX > deadlineY)
                {
                    ret = 1;
                }
            }
            else if (reminderX.Done)
            {
                ret = 1;
            }
            else if (reminderY.Done)
            {
                ret = -1;
            }

            return ret;
        }

        private Reminder GetNextUndoneReminder(TimeControlledElement element)
        {
            Reminder min = null;
            var reminders = element.GetAllReminders(false);

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
