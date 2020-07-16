namespace TimePlannerUpdated.Terminal
{
    class MenuPoint : IPrintableElement
    {
        public delegate void SelectedHandler();

        public string Name { get; set; }
        public SelectedHandler SelectedReference { get; set; }
        public MenuPoint(string name, SelectedHandler selectedReference)
        {
            Name = name;
            SelectedReference = selectedReference;
        }

        public void Print(bool enter)
        {
            Console.Write(Name);
            if (enter)
            {
                Console.WriteLine();
            }
        }
    }
}
