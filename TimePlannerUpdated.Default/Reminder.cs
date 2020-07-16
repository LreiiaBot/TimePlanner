using System;

namespace TimePlannerUpdated.Default
{
    public class Reminder
    {
        public bool Done { get; set; } = false;
        public DateTimeOffset Deadline { get; set; }
        public TimeControlledElement Parent { get; set; }

        public Reminder()
        {
            Deadline = DateTimeOffset.Now;
        }

        public Reminder(TimeControlledElement parent) : this()
        {
            Parent = parent;
        }

        public Reminder(DateTimeOffset deadline) : this()
        {
            Deadline = deadline;
        }

        public Reminder(TimeControlledElement parent, DateTimeOffset deadline) : this(parent)
        {
            Deadline = deadline;
        }

        public TimeSpan GetRemainingTime()
        {
            return Deadline.GetTimeLeft();
        }
    }
}
