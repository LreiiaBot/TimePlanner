using System;
using System.Collections.Generic;
using System.Timers;

namespace TimePlannerUpdated
{
    class Program
    {
        private List<TimeControlledElement> liste = new List<TimeControlledElement>();
        private Timer timer = new Timer();
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
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

            //TimeControlledElement.Print(liste);
            liste.Sort(new TimeControlledElementSorter());

            Selector<TimeControlledElement> test = new Selector<TimeControlledElement>(liste);
        }
    }
}
