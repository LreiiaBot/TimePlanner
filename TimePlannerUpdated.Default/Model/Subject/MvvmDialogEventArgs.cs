using System;

namespace TimePlannerUpdated.Default
{
    public class MvvmDialogEventArgs : EventArgs
    {
        public DialogType type;
        public Action actionAfterCreate;
        public MvvmDialogEventArgs(DialogType type, Action actionAfterCreate = null)
        {
            this.type = type;
            this.actionAfterCreate = actionAfterCreate;
        }
    }
}
