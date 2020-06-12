using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class MainMenu
    {
        private Menu menu;
        public MainMenu()
        {
            List<MenuPoint> points = new List<MenuPoint>()
            {
                new MenuPoint("", null)
            };
            menu = new Menu("Selection", points);
        }

        public void Run()
        {
            menu.Run();
        }
    }
}
