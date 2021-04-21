namespace TimePlannerUpdated.Default
{
    interface IPersistence
    {
        string ToCsv();
        void ToObject(string csv);
    }
}
