using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;
        public override string ToString()
        {
            return $"\n{nameof(Name)}: {Name}, \n{nameof(Position)}: {Position}";
        }
    }
    public class PersonBuilder
    {
        public readonly List<Action<Person>> Actions = new List<Action<Person>>();
        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;

        }
        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAsA
            (this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p => { p.Position = position; });
            return builder;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Lokesh")
                            .WorksAsA("Developer")
                            .Build();
            Console.WriteLine(person);
        }
    }
}
