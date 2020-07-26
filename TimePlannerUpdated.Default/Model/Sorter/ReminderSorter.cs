using System.Collections.Generic;

namespace TimePlannerUpdated.Default
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
            //else if (!x.Deadline.HasValue || !y.Deadline.HasValue)
            //{
            //    if (!x.Deadline.HasValue)
            //    {
            //        ret = 1;
            //    }
            //    else if (!y.Deadline.HasValue)
            //    {
            //        ret = -1;
            //    }
            //    else
            //    {
            //        ret = 0;
            //    }
            //}
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
