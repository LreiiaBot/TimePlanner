using System;
using System.Collections.Generic;

namespace TimePlannerUpdated
{
    class MainViewModel
    {
        public TaskList SelectedList { get; set; }
        private List<TaskList> lists = new List<TaskList>();

        public string CommandSign { get; set; } = "/";
        public string LineDelimiter { get; set; } = "$ ";
        public bool Hide { get; set; } = false;

        public bool End { get; set; } = false;


        public MainViewModel()
        {
            LoadList();
            LoadCommands();
        }

        private void LoadCommands()
        {
            Command.Commands[0].OnSelected += Help;
            Command.Commands[1].OnSelected += ViewLists;
            Command.Commands[2].OnSelected += CreateList;
            Command.Commands[3].OnSelected += SelectList;
            Command.Commands[4].OnSelected += SelectList;
            Command.Commands[5].OnSelected += AlterList;
            Command.Commands[6].OnSelected += AlterList;
            Command.Commands[7].OnSelected += ViewTasks;
            Command.Commands[8].OnSelected += ViewTasks;
            //Command.Commands[9].OnSelected += MoveTask;
            //Command.Commands[10].OnSelected += SelectTask;
            Command.Commands[11].OnSelected += ToggleHide;
            Command.Commands[12].OnSelected += Exit;
        }

        private void ViewTasks(Command command, CommandArgs args)
        {
            TaskList showList = SelectedList;
            if (args.Arguments.Length == 1)
            {
                foreach (var list in this.lists)
                {
                    if (list.Title == args.Arguments[0])
                    {
                        showList = list;
                    }
                }
            }

            showList.PrintWithReminders(true);
            //TaskList.Print(showList.Tasks);
        }

        private void AlterList(Command command, CommandArgs args)
        {
            Console.WriteLine("not working lol");
        }

        private void SelectList(Command command, CommandArgs args)
        {
            if (args.Arguments.Length == 1)
            {
                var selected = false;
                foreach (var list in lists)
                {
                    if (list.Title == args.Arguments[0])
                    {
                        SelectedList = list;
                        selected = true;
                        break;
                    }
                }
                if (!selected)
                {
                    Console.WriteLine($"A list with the name '{args.Arguments[0]}' doesn´t exist.");
                }
            }
            else
            {
                var selector = new Selector<TaskList>(lists);
                selector.OnSelected += Sel_OnSelected;
                selector.Start();
                Console.ResetColor();
                Console.Clear();
            }
        }

        private void Sel_OnSelected(object sender, EventArgs e)
        {
            SelectedList = (TaskList)sender;
        }

        private void LoadList()
        {
            lists = new List<TaskList>();

            SelectedList = new TaskList("inbox", "all new tasks are safed here");
            lists.Add(SelectedList);
        }

        private void Help(Command command, CommandArgs args)
        {
            Command.PrintAllCommands();
        }
        private void ViewLists(Command command, CommandArgs args)
        {
            TaskList.Print(lists);
        }
        private void CreateList(Command command, CommandArgs args)
        {
            var list = new TaskList(args.Arguments[0], String.Empty);
            lists.Add(list);
        }

        private void Exit(Command command, CommandArgs args)
        {
            End = true;
        }

        private void ToggleHide(Command command, CommandArgs args)
        {
            Hide = !Hide;
        }

        public void ReactToInput(string input)
        {
            input = input.Trim();
            if (IsCommand(input))
            {
                input = input.Substring(1);
                Command.ExecuteInsertedCommand(input);
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
