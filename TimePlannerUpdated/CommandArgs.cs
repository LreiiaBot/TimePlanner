using System;

namespace TimePlannerUpdated
{
    class CommandArgs : EventArgs
    {
        public string[] Arguments { get; set; }
        public CommandArgs(string[] arguments)
        {
            Arguments = arguments;
        }
    }
}
