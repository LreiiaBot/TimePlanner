using System;

namespace TimePlannerUpdated
{
    class Reminder : PrintableElement
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

        public void Print(bool enter)
        {
            // ToDo doesnt work yet
            PrintDone();
            SetTimeColor();
            PrintRemainingTime();
            PrintDeadline();
            if (enter)
            {
                Console.WriteLine();
            }
        }

        public void PrintWithParent()
        {
            // Not ready yet
            this.Print(false);
            Parent.Print(true);
        }

        protected virtual void SetTimeColor()
        {
            Console.ResetColor();
            if (Deadline.IsInThePast())
            {
                if (Done)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        protected virtual void PrintDeadline()
        {
            Console.Write(Deadline.Format());
        }

        protected virtual void PrintDone()
        {
            Console.ResetColor();
            string print = String.Empty;
            if (Done)
            {
                print = "Done";
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                print = "Remaining";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(print.PadRight(10));
        }

        protected virtual void PrintRemainingTime()
        {
            var timeLeft = Deadline.GetTimeLeft();
            Console.Write(timeLeft.Format().PadRight(16));
        }
    }
}
