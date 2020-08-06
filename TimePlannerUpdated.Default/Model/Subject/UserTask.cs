using System;

namespace TimePlannerUpdated.Default
{
    public class UserTask : TimeControlledElement
    {
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

        public UserTask(string title, string description, DateTimeOffset date, int minutes, int hours, int days, int months, int years) : this(title, description, date)
        {
            SetAutoAddTimes(minutes, hours, days, months, years);
        }
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
        }
    }
}
