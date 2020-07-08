using System;

namespace TimePlannerUpdated
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
