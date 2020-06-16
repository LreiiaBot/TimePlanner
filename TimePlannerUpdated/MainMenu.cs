using System;

namespace TimePlannerUpdated
{
    class MainMenu
    {
        private MainViewModel mvm;
        public MainMenu()
        {
            // maybe somehow as ressouce?
            mvm = new MainViewModel();

            mvm.SelectedList = new TaskList("inbox", "all new tasks are safed here");
        }

        public void Run()
        {
            do
            {
                PrintLineStart();
                Console.ForegroundColor = ConsoleColor.White;
                var input = Console.ReadLine();
                mvm.ReactToInput(input);
            } while (!mvm.End);
        }

        private void PrintLineStart()
        {
            Helper.Write(mvm.SelectedList.Title, ConsoleColor.DarkYellow);
            Helper.Write(mvm.LineDelimiter, ConsoleColor.Green);
        }
    }
}
