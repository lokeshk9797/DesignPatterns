using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLID.OCP.Program;

namespace SOLID.OCP
{

    public enum Colour
    {
        red, blue, green
    }
    public enum Size
    {
        small, medium, large
    }
    public class Product
    {
        public string Name;
        public Size Size;
        public Colour Colour;
        public Product(string name, Size size, Colour colour)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Colour = colour;
            Size = size;

        }

    }

    public class ProductFilter
    {
        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                {
                    yield return p;
                }
            }
        }
        public static IEnumerable<Product> FilterByColour(IEnumerable<Product> products, Colour colour)
        {
            foreach (var p in products)
            {
                if (p.Colour == colour)
                {
                    yield return p;
                }
            }
        }

        //if we need a new filter we need to modify the class which is violiting Open Close Principal
        public static IEnumerable<Product> FilterByColourAndSize(IEnumerable<Product> products, Colour colour, Size size)
        {
            foreach (var p in products)
            {
                if (p.Colour == colour && p.Size == size)
                {
                    yield return p;
                }
            }
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColourSpecification : ISpecification<Product>
    {
        private Colour colour;
        public ColourSpecification(Colour colour)
        {
            this.colour = colour;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Colour == colour;
        }
    }
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }
    public class BetterFIlter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i))
                    yield return i;
            }

        }
    }
    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> first;
        private readonly ISpecification<T> second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(paramName: nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(paramName: nameof(second));
            }
            this.first = first;
            this.second = second;
        }
        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }

    }

    class Program
    {
        
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Size.medium, Colour.red);
            var pear = new Product("Pear", Size.medium, Colour.green);
            var melon = new Product("Watermelon", Size.large, Colour.green);
            var berry = new Product("BlueBerry", Size.small, Colour.blue);


            Product[] products = { apple, melon, berry, pear };

            Console.WriteLine("Medium Size Products (Old)");
            var resultsBySize = ProductFilter.FilterBySize(products, Size.medium);

            foreach (var result in resultsBySize)
            {
                Console.WriteLine($" -{result.Name} is medium sized");
            }
            Console.WriteLine("\n\nGreen Coloured Products (Old)");
            var resultsByColour = ProductFilter.FilterByColour(products, Colour.green);

            foreach (var result in resultsByColour)
            {
                Console.WriteLine($" -{result.Name} is Green  Coloured");
            }
            Console.WriteLine("\n\n Getting Red Coloured and Medium Product (Old)");

            foreach (var p in ProductFilter.FilterByColourAndSize(products,Colour.red,Size.medium))
            {
                Console.WriteLine($" -{p.Name} is Medium Size and Red coloured ");
            }

            //New Implementation
            var bf = new BetterFIlter();
            Console.WriteLine("\n\nGetting Green Coloured Products (New)");
            foreach (var p in bf.Filter(products, new ColourSpecification(Colour.green)))
            {
                Console.WriteLine($" -{p.Name} is Green  Coloured");
            }
            Console.WriteLine("\n\nGetting Medium Sized Products (New)");
            foreach (var p in bf.Filter(products, new SizeSpecification(Size.medium)))
            {
                Console.WriteLine($" -{p.Name} is Medium Size ");
            }

            //Combination Filters
            Console.WriteLine("\n\n Getting Red Coloured and Medium Product (New)");

            foreach (var p in bf.Filter(products,new AndSpecification<Product>(
                new SizeSpecification(Size.medium),new ColourSpecification(Colour.red))))
            {
                Console.WriteLine($" -{p.Name} is Medium Size and Red coloured ");

            }


        }
    }
}
