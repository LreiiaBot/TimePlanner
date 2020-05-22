using System;

namespace TimePlannerUpdated
{
    class UserTask : TimeControlledElement
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; }

        public int SpaceForTitle { get; set; }
        public int SpaceBetween { get; set; }
        public int SpaceForTitleAndDescription { get; set; }

        public UserTask(string description) : base()
        {
            Description = description;
        }

        public UserTask(string title, string description) : this(description)
        {
            Title = title;
        }

        public UserTask(string title, string description, DateTimeOffset date) : this(title, description)
        {
            StartingTime = date;
        }

        public UserTask(string title, string description, DateTimeOffset date, int hours, int days, int months, int years) : this(title, description, date)
        {
            StartingTime = date;
            SetAutoAddTimes(hours, days, months, years);
        }

        public override void Print(bool enter)
        {
            var hasTitle = false;
            var spaceForDescription = SpaceForTitleAndDescription;

            // If there is a title
            if (!String.IsNullOrWhiteSpace(Title))
            {
                spaceForDescription -= SpaceForTitle;
                spaceForDescription -= SpaceBetween;
                hasTitle = true;
            }
            if (hasTitle)
            {
                var resultTitle = Title.ToSetLength(SpaceForTitle);
                SetConsoleColor(resultTitle.Item2);
                Console.Write(resultTitle.Item1);

                SetConsoleColor(true);
                string s = String.Empty;
                for (int i = 0; i < SpaceBetween; i++)
                {
                    s += " ";
                }
                Console.Write(s);
            }
            var resultDescription = Description.ToSetLength(spaceForDescription);
            SetConsoleColor(resultDescription.Item2);
            Console.Write(resultDescription.Item1);
            if (enter)
            {
                Console.WriteLine();
            }
        }

        public void PrintWithReminders(bool withDone)
        {
            Print(true);
            PrintReminders(withDone);
        }

        private void SetConsoleColor(bool isOk)
        {
            Console.ResetColor();
            if (isOk)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
        }

        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
            SpaceForTitle = 10;
            SpaceForTitleAndDescription = 30;
            SpaceBetween = 1;
        }
    }
}
