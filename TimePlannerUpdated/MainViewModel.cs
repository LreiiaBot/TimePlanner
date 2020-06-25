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

        private void SelectList()
        {
            var sel = new Selector<TaskList>(lists);
            sel.OnSelected += Sel_OnSelected;
            sel.Start();
        }

        private void Sel_OnSelected(object sender, EventArgs e)
        {
            SelectedList = (TaskList)sender;
        }
    }
}
