using System;
using System.Collections.Generic;
using System.Timers;

namespace TimePlannerUpdated
{
    internal class Selector<T> : IDisposable where T : class, IPrintableElement
    {
        public event EventHandler OnSelected;

        private List<T> elements;
        private T selected;

        private bool run = false;

        private int clickedRow = -1;
        private int startingIndex = 0;
        private int printableElementsCount;
        private int consoleHeight;

        private Timer timer = new Timer();
        public Selector(List<T> elements)
        {
            this.elements = elements;
            Console.CursorVisible = false;
            SetupEventListeners();

            // save consoleheight
            consoleHeight = Console.WindowHeight;
            printableElementsCount = consoleHeight - 1;
        }

        public void Start()
        {
            run = true;
            ConsoleListener.Start();
            SetupTimer();
            Print();

            do
            {
                Console.ReadKey(true);
            } while (run);
        }

        public void Stop()
        {
            // stop timer
            timer.Stop();

            // stop consolelistener
            ConsoleListener.Stop();

            // end selection
            run = false;
            // not sure if i have to remove the methods from the events
        }

        ~Selector()
        {
            // check if this is a destructor but i think so
            // difference to dispose?
        }

        private void SetupEventListeners()
        {
            ConsoleListener.ClickEvent += ConsoleListener_ClickEvent;
            ConsoleListener.ScrollDownEvent += ConsoleListener_ScrollDownEvent;
            ConsoleListener.ScrollUpEvent += ConsoleListener_ScrollUpEvent;
        }

        private void ConsoleListener_ScrollUpEvent(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            // stop the timer
            timer.Stop();

            // if there are previous elements...
            if (startingIndex - printableElementsCount >= 0)
            {
                // specify to show the previous elements
                startingIndex -= printableElementsCount;
                if (startingIndex < 0)
                {
                    startingIndex = 0;
                }

                // print elements
                Print();
            }

            // start timer again
            timer.Start();
        }

        private void ConsoleListener_ScrollDownEvent(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            // stop the timer
            timer.Stop();

            // ToDo check if this works on every elemntscount
            // if there are further elements...
            if (startingIndex + printableElementsCount < elements.Count)
            {
                // specify to show the next elements
                startingIndex += printableElementsCount;

                // print elments
                Print();
            }

            // start the timer again
            timer.Start();
        }

        private void SetupTimer()
        {
            // set 10 seconds timer
            timer.Interval = 10000;

            // add method to timer elapsed method
            timer.Elapsed += Timer_Tick;

            // start timer
            timer.Start();
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            Print();
        }

        private void ConsoleListener_ClickEvent(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            // get clicked row
            clickedRow = r.dwMousePosition.Y;
            // if clicked row is valid...
            if (clickedRow >= 0 && clickedRow + startingIndex < elements.Count && clickedRow < printableElementsCount)
            {
                // identify clicked element
                selected = elements[clickedRow + startingIndex];
                // stop all timers, listeners, etc
                Stop();

                // clear the console
                Console.Clear();
                // execute the given method on the clicked element
                OnSelected?.Invoke(selected, EventArgs.Empty);
            }
        }

        public void Print()
        {
            // set windowheight like start of selection that there is enough space for everything
            Console.WindowHeight = consoleHeight;
            Console.ResetColor();
            Console.Clear();

            // for every element that is in range to be shown...
            for (int i = startingIndex; i < printableElementsCount + startingIndex && i < elements.Count; i++)
            {
                Console.ResetColor();

                // print line number
                Console.Write((i + 1).ToString().PadRight(5));

                // print the element with linebrak
                elements[i].Print(true);
            }

            // print the count of all elements
            Console.WriteLine($"There are {elements.Count} Elements");

            // place cursor at top to scroll up and all is visible
            Console.CursorTop = 0;
        }

        public void Dispose()
        {

        }
    }
}
