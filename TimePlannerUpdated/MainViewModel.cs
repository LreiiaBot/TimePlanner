using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class MainViewModel
    {
        public TaskList SelectedList { get; set; }
        private List<TimeControlledElement> liste = new List<TimeControlledElement>();

        public string CommandSign { get; set; } = "/";
        public string LineDelimiter { get; set; } = "$ ";

        public bool End { get; set; } = false;

        public MainViewModel()
        {
            LoadList();
        }

        private void LoadList()
        {
            liste = new List<TimeControlledElement>();
        }

        public void ReactToInput(string input)
        {
            input = input.Trim();
            if (IsCommand(input))
            {
                // react to command
                foreach (var cmd in Command.Commands)
                {
                    if (cmd.Name.Equals(input.Substring(CommandSign.Length)))
                    {
                        // normally i would somehow check arguments count and execute
                        System.Console.WriteLine(cmd.Definition);
                    }
                }
            }
            else
            {
                // insert to sellist
            }
        }
        private bool IsCommand(string input)
        {
            return input.StartsWith(CommandSign);
        }
    }
}
