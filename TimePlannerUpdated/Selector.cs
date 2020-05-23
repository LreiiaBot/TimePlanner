using System;
using System.Collections.Generic;
using System.Timers;

namespace TimePlannerUpdated
{
    internal class Selector<T> : IDisposable where T : class, IPrintableElement
    {
        public delegate void OnSelectedHandler(T element);
        private OnSelectedHandler onSelectedReference; // maybe better name

        private List<T> elements;
        private T selected;

        private bool run = false;

        private int clickedRow = -1;
        private int startingIndex = 0;
        private int printableElementsCount;
        private int consoleHeight;

        private Timer timer = new Timer();
        public Selector(List<T> elements, OnSelectedHandler onSelected)
        {
            this.elements = elements;
            this.onSelectedReference = onSelected;
            Console.CursorVisible = false;
            SetupEventListeners();

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
            timer.Stop();
            ConsoleListener.Stop();
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
            timer.Stop();
            //if (startingIndex - 1 >= 0)
            //{
            //    startingIndex--;
            //    Print();
            //}
            if (startingIndex - printableElementsCount >= 0)
            {
                startingIndex -= printableElementsCount;
                if (startingIndex < 0)
                {
                    startingIndex = 0;
                }
                Print();
            }
            timer.Start();
        }

        private void ConsoleListener_ScrollDownEvent(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            timer.Stop();
            //if (elements.Count - startingIndex > printableElementsCount)
            //{
            //    startingIndex++;
            //    Print();
            //}

            // ToDo check if this works on every elemntscount
            if (startingIndex + printableElementsCount < elements.Count)
            {
                startingIndex += printableElementsCount;
                Print();
            }
            timer.Start();
        }

        private void SetupTimer()
        {
            timer.Interval = 10000; // 10 sec
            timer.Elapsed += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            Print();
        }

        private void ConsoleListener_ClickEvent(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            clickedRow = r.dwMousePosition.Y;
            if (clickedRow >= 0 && clickedRow + startingIndex < elements.Count && clickedRow < printableElementsCount)
            {
                selected = elements[clickedRow + startingIndex];
                Stop();

                Console.Clear();
                onSelectedReference(selected);
            }
        }

        public void Print()
        {
            Console.WindowHeight = consoleHeight;
            Console.ResetColor();
            Console.Clear();
            for (int i = startingIndex; i < printableElementsCount + startingIndex && i < elements.Count; i++)
            {
                Console.ResetColor();
                Console.Write((i + 1).ToString().PadRight(5));
                elements[i].Print(true);
            }
            Console.WriteLine($"There are {elements.Count} Elements");
            Console.CursorTop = 0;
        }

        public void Dispose()
        {
            Console.WriteLine("dispose");
        }
    }
}
