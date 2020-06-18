using System;
using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class MainViewModel
    {
        public TaskList SelectedList { get; set; }
        private List<TaskList> liste = new List<TaskList>();

        public string CommandSign { get; set; } = "/";
        public string LineDelimiter { get; set; } = "$ ";
        public bool Hide { get; set; } = false;

        public bool End { get; set; } = false;

        public MainViewModel()
        {
            LoadList();
        }

        private void LoadList()
        {
            liste = new List<TaskList>();

            SelectedList = new TaskList("inbox", "all new tasks are safed here");
            liste.Add(SelectedList);
        }

        public void ReactToInput(string input)
        {
            input = input.Trim();
            bool isKnownCommand = false;
            if (IsCommand(input))
            {
                foreach (var cmd in Command.Commands)
                {
                    if (input.Substring(CommandSign.Length).StartsWith(cmd.Name))
                    {
                        // if there are no args (removed cmd-name from input and checked if there is something left)
                        if (String.IsNullOrWhiteSpace(input.Substring(CommandSign.Length).Substring(cmd.Name.Length)))
                        {
                            if (cmd.Arguments.Length == 0)
                            {
                                switch (cmd.Name)
                                {
                                    case "help":
                                        Command.PrintAllCommands();
                                        break;
                                    case "view lists":
                                        TaskList.Print(liste);
                                        Console.WriteLine();
                                        break;
                                    case "select list":
                                        Console.WriteLine("maybe only with parameter or now use selector");
                                        break;
                                    case "alter list":
                                        break;
                                    case "view tasks":
                                        break;
                                    case "move task":
                                        Console.WriteLine("mabye only with parameters or now selector");
                                        break;
                                    case "hide":
                                        Hide = !Hide;
                                        break;
                                    case "exit":
                                        End = true;
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("This command requires arguments!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("not working yet");
                            // switch case
                        }
                        isKnownCommand = true;
                        break;
                    }
                }
                if (!isKnownCommand)
                {
                    Console.WriteLine(">>unknown command<<");
                }
            }
            else
            {
                if (SelectedList != null)
                {
                    var task = new UserTask(input);
                    SelectedList.Tasks.Add(task);
                }
            }
        }
        private bool IsCommand(string input)
        {
            return input.StartsWith(CommandSign);
        }
    }
}
