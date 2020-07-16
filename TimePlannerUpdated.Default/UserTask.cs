using System;

namespace TimePlannerUpdated.Default
{
    public class UserTask : TimeControlledElement
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; }

        public UserTask(string title) : base()
        {
            Title = title;
        }

        public UserTask(string title, string description) : this(title)
        {
            Description = description;
        }

        public UserTask(string title, string description, DateTimeOffset date) : this(title, description)
        {
            StartingTime = date;
        }

        public UserTask(string title, string description, DateTimeOffset date, int hours, int days, int months, int years) : this(title, description, date)
        {
            SetAutoAddTimes(hours, days, months, years);
        }
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
        }
    }
}
