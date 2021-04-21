using System;
using System.Collections.Generic;
using System.IO;

namespace TimePlannerUpdated.Default
{
    class CSVTimeControlledElement
    {
        private static string Path = "";

        public static void Save(TimeControlledElement element)
        {
            // Check if element already exists
            // if so, Update, elsewhise Insert
        }

        public static void Insert(TimeControlledElement element)
        {
            using (StreamWriter writer = new StreamWriter(Path, true))
            {
                writer.WriteLine(element.ToCsv());
            }
        }

        public static void Update(TimeControlledElement element)
        {
            // Search for Element in File
            // Update it
        }

        public static void Save(List<TimeControlledElement> elements)
        {
            using (StreamWriter writer = new StreamWriter(Path, true))
            {
                foreach (var element in elements)
                {
                    writer.WriteLine(element.ToCsv());
                }
            }
        }

        public static TimeControlledElement Read()
        {
            throw new NotImplementedException();
        }
    }
}
