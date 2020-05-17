using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class ReminderSorter : IComparer<Reminder>
    {
        public int Compare(Reminder x, Reminder y)
        {
            // This needs work
            int ret = -1;

            if (x.Deadline == null && y.Deadline == null)
            {
                ret = 0;
            }
            if (x.Deadline == null)
            {
                ret = 1;
            }
            else if (y.Deadline == null)
            {
                ret = -1;
            }
            else if (x.Deadline < y.Deadline)
            {
                ret = -1;
            }
            else if (x.Deadline > y.Deadline)
            {
                ret = 1;
            }
            else if (x.Deadline == y.Deadline)
            {
                ret = 0;
            }
            return ret;
        }
    }
}
