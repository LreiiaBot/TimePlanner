using System.Collections.Generic;

namespace TimePlannerUpdated.Default
{
    class ReminderSorter : IComparer<Reminder>
    {
        public int Compare(Reminder x, Reminder y)
        {
            int comp = x.Done.CompareTo(y.Done);
            if (comp == 0)
            {
                comp = x.Deadline.CompareTo(y.Deadline);
            }
            return comp;
        }
    }
}
