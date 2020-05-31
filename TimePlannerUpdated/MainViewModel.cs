using System;
using System.Collections.Generic;
using System.Timers;

namespace TimePlannerUpdated
{
    class MainViewModel
    {
        private List<TimeControlledElement> liste = new List<TimeControlledElement>();
        private Timer timer = new Timer();
        public MainViewModel()
        {

            ConsoleListener.Setup();

            // https://stackoverflow.com/questions/31750770/difference-between-datetimeoffsetnullable-and-datetimeoffset-now

            var task = new UserTask("kartoffeln kaufen");
            liste.Add(task);

            task.AddReminder(new Reminder(DateTimeOffset.Parse("18.05.2020")));
            task.AddReminder(new Reminder(DateTimeOffset.Parse("17.05.2020")));
            task.AddReminder(new Reminder(DateTimeOffset.Parse("19.05.2020")));

            task.AddReminder(new Reminder(DateTimeOffset.Parse("18.05.2020")) { Done = true });
            task.AddReminder(new Reminder(DateTimeOffset.Parse("18.05.2020")) { Done = true });
            task.AddReminder(new Reminder(DateTimeOffset.Parse("18.05.2020")) { Done = true });

            task.AddReminder(new Reminder() { Done = true });
            task.AddReminder(new Reminder() { Done = false });

            task.AddReminder(new Reminder(DateTimeOffset.Parse("18.05.2020")) { Done = true });

            liste.Add(new UserTask("Einkauf", "lecker schmecker süß und saftig"));
            liste.Add(new UserTask("beschreibung yikes"));
            liste.Add(new UserTask("Essen", "nom nom nom nom nom nom nom nom nom nom nom nom"));


            liste.Add(new UserTask("Butterbrot", "schmieren"));
            liste.Add(new UserTask("Cornflakes", "einschenken"));
            liste.Add(new UserTask("Pizza", "reinschütteln"));

            //liste.Sort(new TimeControlledElementSorter());

            var reminderList = new List<Reminder>();
            foreach (var item in liste)
            {
                foreach (var reminder in item.GetAllReminders())
                {
                    reminderList.Add(reminder);
                }
            }
            reminderList.Sort(new ReminderSorter());

            using (Selector<Reminder> test = new Selector<Reminder>(reminderList, DoSomething))
            {
                test.Start();
            }
            //Selector<TimeControlledElement> test = new Selector<TimeControlledElement>(liste);
        }
        private void DoSomething(Reminder r)
        {
            r.Print(false);
        }
    }
}
