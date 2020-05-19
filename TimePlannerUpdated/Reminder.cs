using System;

namespace TimePlannerUpdated
{
    class Reminder : IPrintableElement
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
            PrintDeadline();
            PrintRemainingTime();
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        protected virtual void PrintDeadline()
        {
            Console.Write(Deadline.Format() + String.Empty.PadRight(3));
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
            Console.Write(print.PadRight(12));
        }

        protected virtual void PrintRemainingTime()
        {
            var timeLeft = Deadline.GetTimeLeft();
            Console.Write(timeLeft.Format());
        }
    }
}
