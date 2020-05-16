using System;

namespace TimePlannerUpdated
{
    class Reminder
    {
        public bool Done { get; set; }
        public DateTime Deadline { get; set; }

        public Reminder()
        {
            Done = false;
        }
    }
}
