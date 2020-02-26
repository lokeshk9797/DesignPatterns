using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.FacetedBuilder
{
    public class Person
    {
        public string Name;

        //Address
        public string StreetAddress, PostalCode, City;

        //emploment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"\n{nameof(Name)} : {Name} \n{nameof(StreetAddress)} : {StreetAddress} \n{nameof(PostalCode)} : {PostalCode}" +
                $" \n{nameof(City)} : {City} \n{nameof(CompanyName)} : {CompanyName} \n{nameof(Position)} : {Position} " +
                $"\n{nameof(AnnualIncome)} : {AnnualIncome} ";
        }

    }
    //facade for other builders, keeps a referance of the builder
    public class PersonBuilder 
    {
        //referance object
        protected Person person = new Person();

        //Referance to other builders
        public PersonIdentityBuilder Identifies => new PersonIdentityBuilder(person);
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        //Implicit type conversion
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonIdentityBuilder :PersonBuilder
    {
        public PersonIdentityBuilder(Person person)
        {
            this.person = person;
        }
        public PersonIdentityBuilder KnownAs(string name)
        {
            person.Name = name;
            return this;
        }
    }

    public class PersonAddressBuilder: PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAddressBuilder AtStreet(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }
        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
        public PersonAddressBuilder WithPostCode(string postalCode)
        {
            person.PostalCode = postalCode;
            return this;
        }
    }

    public class PersonJobBuilder :PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earns(int annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Identifies
                    .KnownAs("Lokesh")
                .Lives
                    .AtStreet("Street no 24")
                    .In("Nagpur")
                    .WithPostCode("441108")
                .Works
                    .AsA("Developer")
                    .At("Google")
                    .Earns(100000);
            Console.WriteLine(person);
        }
    }
}
