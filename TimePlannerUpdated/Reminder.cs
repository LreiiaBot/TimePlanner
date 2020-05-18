using System;

namespace TimePlannerUpdated
{
    class Reminder : PrintableElement
    {
        public bool Done { get; set; } = false;
        public DateTimeOffset Deadline { get; set; }


        public Reminder()
        {

        }

        public Reminder(DateTimeOffset deadline) : this()
        {
            Deadline = deadline;
        }

        public void Print()
        {
            var dateString = Deadline.Format();
            var doneString = Done ? "Done" : "NotDone";
            Console.WriteLine(dateString + " " + doneString);
        }
    }
}
