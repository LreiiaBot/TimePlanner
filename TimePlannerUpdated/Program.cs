using System;
using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://stackoverflow.com/questions/31750770/difference-between-datetimeoffsetnullable-and-datetimeoffset-now

            var liste = new List<TimeControlledElement>();

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
            TimeControlledElement.PrintWithReminders(liste, true);

            Console.ReadLine();
        }
    }
}
