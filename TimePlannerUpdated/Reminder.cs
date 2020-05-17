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

        public Reminder(DateTime deadline) : this()
        {
            Deadline = deadline;
        }
    }
}
