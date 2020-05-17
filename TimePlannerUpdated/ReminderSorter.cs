using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class ReminderSorter : IComparer<Reminder>
    {
        public int Compare(Reminder x, Reminder y)
        {
            // -1 x is first in list

            int ret = 0;
            if (x.Done == y.Done && x.Deadline.Equals(y.Deadline))
            {
                ret = 0;
            }
            else if (x.Done && !y.Done)
            {
                ret = 1;
            }
            else if (!x.Done && y.Done)
            {
                ret = -1;
            }
            else if (x.Deadline == null || y.Deadline == null)
            {
                if (x.Deadline != null)
                {
                    ret = 1;
                }
                else if (y.Deadline != null)
                {
                    ret = -1;
                }
                else
                {
                    ret = 0;
                }
            }
            else if (x.Deadline < y.Deadline)
            {
                ret = -1;
            }
            else if (y.Deadline < x.Deadline)
            {
                ret = 1;
            }
            return ret;
        }
    }
}
