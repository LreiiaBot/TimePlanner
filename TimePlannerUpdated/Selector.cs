using System;
using System.Collections.Generic;
using System.Timers;

namespace TimePlannerUpdated
{
    internal class Selector<T> : IDisposable where T : class, IPrintableElement
    {
        public event EventHandler OnSelected;

        private List<T> elements;

        private bool run = false;

        private Timer timer;

        private int selectedRow = 0;
        private int startingIndex = 0;
        private int printableElementsCount;
        private int consoleHeight;

        public Selector(List<T> elements)
        {
            this.elements = elements;
            Console.CursorVisible = false;

            // save consoleheight
            consoleHeight = Console.WindowHeight;
            printableElementsCount = consoleHeight - 1;

            SetupTimer();
        }

        public void Start()
        {
            run = true;
            Print();

            do
            {
                ReactToInput();
            } while (run);
        }

        private void ReactToInput()
        {
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.J:
                    CursorDown();
                    break;
                case ConsoleKey.K:
                    CursorUp();
                    break;
                case ConsoleKey.H:
                    ScrollUp();
                    break;
                case ConsoleKey.L:
                    selectedRow = 0;
                    ScrollDown();
                    break;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    Selected();
                    run = false;
                    break;
                default:
                    break;
            }
            Print();
        }

        public void Stop()
        {
            timer.Stop();

            // end selection
            run = false;
        }

        private void SetupTimer()
        {
            timer = new Timer(10000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Print();
        }

        ~Selector()
        {
            // check if this is a destructor but i think so
            // difference to dispose?
        }

        private void CursorUp()
        {
            if (selectedRow - 1 >= 0)
            {
                selectedRow--;
            }
        }

        private void CursorDown()
        {
            if (selectedRow + 1 < printableElementsCount && startingIndex + selectedRow + 1 < elements.Count)
            {
                selectedRow++;
            }
        }

        private void ScrollUp()
        {
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
        }

        private void ScrollDown()
        {
            // ToDo check if this works on every elemntscount
            // if there are further elements...
            if (startingIndex + printableElementsCount < elements.Count)
            {
                // specify to show the next elements
                startingIndex += printableElementsCount;

                // print elments
                Print();
            }
        }

        private void Selected()
        {
            // if clicked row is valid...
            if (selectedRow >= 0 && selectedRow + startingIndex < elements.Count && selectedRow < printableElementsCount)
            {
                // identify clicked element
                var selected = elements[selectedRow + startingIndex];
                // stop everything that is running
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

                if (selectedRow + startingIndex == i)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }

                // print line number
                Console.Write((i + 1).ToString().PadRight(5));
                Console.ResetColor();
                Console.Write(" ");

                // print the element with linebreak
                elements[i].Print(true);
            }

            // print the count of all elements
            Console.WriteLine($"There are {elements.Count} Elements");
        }

        public void Dispose()
        {

        }
    }
}
