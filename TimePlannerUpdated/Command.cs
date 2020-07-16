using System;
using System.Collections.Generic;
using TimePlannerUpdated.Default;

namespace TimePlannerUpdated.Terminal
{
    class Command
    {
        public static List<Command> Commands { get; set; }
        public delegate void CommandExecuteHandler(Command command, CommandArgs args);
        public event CommandExecuteHandler OnSelected;
        public string Name { get; set; } = "none";
        public string Definition { get; set; } = "none";
        public string[] Arguments { get; set; } = new string[0];

        public static void SetupCommands(MainViewModel mvm)
        {
            Command command;
            command = new Command("help", "shows this list of helpcommands");
            command.OnSelected += mvm.Help;
            Commands.Add(command);

            command = new Command("view lists", "lists all available lists");
            Commands.Add(command);

            command = new Command("create list", "creates a list", "listname")
            Commands.Add(command);

            command = new Command("select list", "lets you select a list")
            Commands.Add(command);

            command = new Command("select list", "lets you select a list", "listname")
            Commands.Add(command);

            command = new Command("alter list", "makes you able to change the selected list")
            Commands.Add(command);

            command = new Command("alter list", "makes you able to change a list", "listname")
            Commands.Add(command);

            command = new Command("view tasks", "shows the tasks of the currently selected list")
            Commands.Add(command);

            command = new Command("view tasks", "shows the tasks of the given list", "listname")
            Commands.Add(command);

            command = new Command("move task", "moves the task to a list")
            Commands.Add(command);

            command = new Command("move task", "moves the task to a list", "taskid", "listname")
            Commands.Add(command);

            command = new Command("select task", "selects one task from the list and makes further actions possible")
            Commands.Add(command);

            command = new Command("hide", "makes the input to the console invisible");
            Commands.Add(command);

            command = new Command("exit", "closes the application")
            Commands.Add(command);

            command = new Command("exit", "closes the application")
            Commands.Add(command);

            command = new Command("test", "no", "arg1", "arg2", "arg3", "arg4");
            Commands.Add(command);

            /*Command.Commands[0].OnSelected += Help;
            Command.Commands[1].OnSelected += ViewLists;
            Command.Commands[2].OnSelected += CreateList;
            Command.Commands[3].OnSelected += SelectList;
            Command.Commands[4].OnSelected += SelectList;
            Command.Commands[5].OnSelected += AlterList;
            Command.Commands[6].OnSelected += AlterList;
            Command.Commands[7].OnSelected += ViewTasks;
            Command.Commands[8].OnSelected += ViewTasks;
            Command.Commands[9].OnSelected += MoveTask;
            Command.Commands[10].OnSelected += MoveTask;
            //Command.Commands[11].OnSelected += SelectTask;
            Command.Commands[12].OnSelected += ToggleHide;
            Command.Commands[13].OnSelected += Exit;*/
        }

        public Command(string name, string definition)
        {
            Name = name;
            Definition = definition;
        }

        public Command(string cmd, string definition, params string[] args) : this(cmd, definition)
        {
            Arguments = args;
        }

        private void PrintArguments(int pad)
        {
            foreach (var argument in Arguments)
            {
                Helper.Write(" <", ConsoleColor.DarkYellow);
                Helper.Write(argument, ConsoleColor.Magenta);
                Helper.Write(">", ConsoleColor.DarkYellow);

                // +3 because " <>" are 3 chars
                pad -= argument.Length + 3;
            }
            if (pad > 0)
            {
                Helper.Write(String.Empty.PadRight(pad));
            }
        }

        public static void PrintAllCommands()
        {
            int padName = 0;
            int padArgs = 0;
            foreach (var cmd in Commands)
            {
                int argsLenth = 0;
                foreach (var arg in cmd.Arguments)
                {
                    argsLenth += arg.Length + 3;
                }
                if (argsLenth > padArgs)
                {
                    padArgs = argsLenth;
                }
                if (cmd.Name.Length > padName)
                {
                    padName = cmd.Name.Length;
                }
            }
            foreach (var cmd in Commands)
            {
                Helper.Write($"{cmd.Name.PadRight(padName)} ", ConsoleColor.DarkGray);
                cmd.PrintArguments(padArgs);
                Helper.Write($" | {cmd.Definition}\n");
            }
        }

        public static void ExecuteInsertedCommand(string input)
        {
            Command selCmd = null;
            string[] args = null;
            foreach (var cmd in Commands)
            {
                if (input.StartsWith(cmd.Name))
                {
                    args = cmd.GetArguments(input);
                    if (cmd.Arguments.Length == args.Length)
                    {
                        selCmd = cmd;
                        break;
                    }
                    else
                    {
                        args = null;
                    }
                }
            }

            if (selCmd != null)
            {
                selCmd.OnSelected?.Invoke(selCmd, new CommandArgs(args));
            }
            else
            {
                Console.WriteLine("invalid command, maybe show this message differently");
            }
        }

        public string[] GetArguments(string input)
        {
            var argsStr = input.Substring(Name.Length).Trim();
            string[] args = new string[0];
            if (!String.IsNullOrWhiteSpace(argsStr))
            {
                args = argsStr.Split(' ');
            }
            return args;
        }
    }
}
