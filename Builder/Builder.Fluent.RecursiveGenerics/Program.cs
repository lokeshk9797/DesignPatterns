using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Fluent.RecursiveGenerics
{
    class Program
    {
        public class Person
        {
            public string Name;
            public DateTime DateOfBirth;
            public string Position;

            public class Builder : PersonBirthDateBuilder<Builder>
            {
                internal Builder() { }
            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name},\n{nameof(Position)}: {Position},\n{nameof(DateOfBirth)}:{DateOfBirth.ToString("dd/MM/yyyy")}";
            }
        }
        public abstract class PersonBuilder
        {
            protected Person person= new Person();
            public Person Build()
            {
                return person;
            }
        }

        public class PersonInfoBuilder<Self> :PersonBuilder where Self : PersonInfoBuilder<Self>
        {
            public Self Called(string name)
            {
                person.Name = name;
                return (Self)this;
            }
        }

        public class PersonJobBuilder<Self>: PersonInfoBuilder<PersonJobBuilder<Self>>
            where Self : PersonJobBuilder<Self>
        {
            public Self WorksAs(string position)
            {
                person.Position = position;
                return (Self)this;
            }
        }

        // here's another inheritance level
        // note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

        public class PersonBirthDateBuilder<Self>: PersonJobBuilder<PersonBirthDateBuilder<Self>>
            where Self :PersonBirthDateBuilder<Self>
        {
            public Self BornOn(DateTime dateOfBirth)
            {
                person.DateOfBirth = dateOfBirth;
                return (Self)this;

            }
        }

        static void Main(string[] args)
        {
            var me = Person.New.Called("Lokesh").WorksAs("Software developer").BornOn(new DateTime(1997,07,09).Date).Build();
            Console.WriteLine(me);
        }
    }
}
