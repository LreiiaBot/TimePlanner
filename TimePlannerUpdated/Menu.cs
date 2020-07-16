using System;
using System.Collections.Generic;

namespace TimePlannerUpdated.Terminal
{
    class Menu
    {
        public string Title { get; set; }
        public List<MenuPoint> Points { get; set; }
        public Menu(string title, List<MenuPoint> points)
        {
            Title = title;
            Points = points;
        }

        public void Run()
        {
            Console.WriteLine(Title);
            Console.ReadKey(true);
            var selector = new Selector<MenuPoint>(Points);
            selector.OnSelected += MenuPoint_OnSelected;
            selector.Start();
        }

        private void MenuPoint_OnSelected(object sender, EventArgs e)
        {
            var selectedPoint = (MenuPoint)sender;
            selectedPoint.SelectedReference?.Invoke();
        }
    }
}
