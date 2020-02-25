using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SOLID.SRP
{
    class Program
    {
        //class stroes a couple of journal entries
        public class Journal
        {
            private readonly List<string> entries = new List<string>();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{ ++count}.{text}");
                return count;//memento Patterm
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }
            public void DeleteEntry(int index)
            {
                entries.RemoveAt(index);
            }

            //    // breaks single responsibility principle
            //    public void Save(string filename, bool overwrite = false)
            //    {
            //        File.WriteAllText(filename, ToString());
            //    }

            //    public void Load(string filename)
            //    {

            //    }

            //    public void Load(Uri uri)
            //    {

            //    }
        }

        // handles the responsibility of persisting objects
        public class Persistence
        {
            public void SaveToFile(Journal journal, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                    File.WriteAllText(filename, journal.ToString());
            }
        }
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I won today");
            j.AddEntry("Bought new shoes today");
            WriteLine(j);


            var p = new Persistence();
            var filename = @"d:\temp\journal.txt";
            p.SaveToFile(j, filename);
            Process.Start(filename);
        }




       
    }
}
