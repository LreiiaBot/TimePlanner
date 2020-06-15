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
                new MenuPoint("test", null)
            };
            menu = new Menu("Selection", points);
        }

        public void Run()
        {
            menu.Run();
        }
    }
}
