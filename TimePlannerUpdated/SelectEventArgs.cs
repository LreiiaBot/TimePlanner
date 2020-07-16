using System;

namespace TimePlannerUpdated.Terminal
{
    public class SelectEventArgs<T> : EventArgs
    {
        public T SelectedElement { get; set; }

        public SelectEventArgs(T selectedElement)
        {
            SelectedElement = selectedElement;
        }
    }
}
