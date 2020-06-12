using System;

namespace TimePlannerUpdated
{
    class Command // : MenuPoint
    {
        public static Command[] Commands { get; set; } =
        {
            new Command("help", "shows this list of helpcommands"),
            new Command("view lists", "lists all available lists"),
            new Command("create list", "creates a list", "listname"),
            new Command("select list", "selects the given list", "listname"),
            new Command("alter list", "makes you able to change the selected list"),
            new Command("alter list", "makes you able to change a list", "listname"),
            new Command("view tasks", "shows the tasks of the currently selected list"),
            new Command("view tasks", "shows the tasks of the given list", "listname"),
            new Command("move task", "moves the task to a list", "taskid", "listname"),
            new Command("select task", "selects one task from the list and makes further actions possible"),
            new Command("hide", "makes the input to the console invisible"),
            new Command("exit", "closes the application"),
            new Command("test", "no", "arg1", "arg2", "arg3", "arg4")
        };
        public string Cmd { get; set; } = "none";
        public string Definition { get; set; } = "none";
        public string[] Arguments { get; set; } = new string[0];

        public Command(string cmd, string definition)
        {
            Cmd = cmd;
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
                if (cmd.Cmd.Length > padName)
                {
                    padName = cmd.Cmd.Length;
                }
            }
            foreach (var cmd in Commands)
            {
                Helper.Write($"{cmd.Cmd.PadRight(padName)} ", ConsoleColor.DarkGray);
                cmd.PrintArguments(padArgs);
                Helper.Write($" | {cmd.Definition}\n");
            }
        }
    }
}
