using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class MainViewModel
    {
        private List<TimeControlledElement> liste = new List<TimeControlledElement>();
        public MainViewModel()
        {
            LoadList();
            ConsoleListener.Setup();

            // https://stackoverflow.com/questions/31750770/difference-between-datetimeoffsetnullable-and-datetimeoffset-now

            var menuPoins = new List<MenuPoint>()
            {
                new MenuPoint("show", Show),
                new MenuPoint("end", End)
            };
            new Menu("AUSWAHL", menuPoins).Run();
        }

        private void LoadList()
        {
            liste = new List<TimeControlledElement>();
        }

        private void Show()
        {
            System.Console.WriteLine("yike show");
        }

        private void End()
        {
            System.Console.WriteLine("end");
        }
    }
}
