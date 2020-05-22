using System;
using System.Collections.Generic;
using System.Timers;

namespace TimePlannerUpdated
{
    class Selector<T> where T : IPrintableElement
    {
        private List<T> elements;
        private T selected;

        private int clickedRow = -1;

        private Timer timer = new Timer();
        public Selector(List<T> elements)
        {
            this.elements = elements;
            //this.selected = el;

            ConsoleListener.ClickEvent += ConsoleListener_ClickEvent;
            // only print elements based on consolehight
            // when scrolling show other things

            // maybe delegate, what should be done when a element is selceted

            // vielleicht nicht das ganze zeug mit hintergrundfarbe sondern nur am anfang paar leerzeichen oder so, das verbessert die lesbarkeit
            ConsoleListener.Start();
            SetupTimer();
            Print();
        }

        private void SetupTimer()
        {
            timer.Interval = 10000; // 10 sec
            timer.Elapsed += Timer_Tick;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            Print();
        }

        private void ConsoleListener_ClickEvent(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            clickedRow = r.dwMousePosition.Y;
            try
            {
                if (clickedRow != -1)
                {
                    selected = elements[clickedRow];
                }
            }
            catch (Exception)
            {

            }
            Print();
            //ConsoleListener.Stop();
        }

        public void Print()
        {
            Console.ResetColor();
            Console.Clear();
            foreach (var element in elements)
            {
                if (element != null && (object)element == (object)selected)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                Console.Write("  ");
                Console.ResetColor();
                Console.Write(" ");
                element.Print(true);
            }
        }
    }
}
