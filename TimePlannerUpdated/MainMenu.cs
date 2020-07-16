using System;

namespace TimePlannerUpdated.Terminal
{
    class MainMenu
    {
        private MainViewModel mvm;
        public MainMenu()
        {
            // maybe somehow as ressouce?
            mvm = new MainViewModel();
        }

        public void Run()
        {
            do
            {
                PrintLineStart();
                SetInputColor();
                var input = Console.ReadLine();
                mvm.ReactToInput(input);
            } while (!mvm.End);
        }

        private void PrintLineStart()
        {
            Console.ResetColor();
            Helper.Write(mvm.SelectedList.Title, ConsoleColor.DarkYellow);
            Helper.Write(mvm.LineDelimiter, ConsoleColor.Green);
        }

        private void SetInputColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            if (mvm.Hide)
            {
                Console.ForegroundColor = Console.BackgroundColor;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
