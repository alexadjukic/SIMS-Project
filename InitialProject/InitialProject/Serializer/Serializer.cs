using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Data;

namespace InitialProject.Serializer
{
    public abstract class Serializer<T>
    {
        private const char Delimiter = '|';

        public void ToCSV(string fileName, List<T> objects)
        {
            StringBuilder csv = new StringBuilder();

            foreach(T obj in objects)
            {
                string line = string.Join(Delimiter.ToString(), GetCSVValues(obj));
                csv.AppendLine(line);
            }

            File.WriteAllText(fileName, csv.ToString());

        }

        public List<T> FromCSV(string fileName)
        {
            List<T> objects = new List<T>();

            foreach(string line in File.ReadLines(fileName))
            {
                string[] csvValues = line.Split(Delimiter);
                objects.Add(GetObject(csvValues));
            }

            return objects;
        }

        public abstract string[] GetCSVValues(T obj);

        public abstract T GetObject(string[] csvValues);
    }
}
