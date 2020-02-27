using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.AbstractFactory
{

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Tea is Awesome");
        }
    }
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Coffee is Exaggerated");
        }
    }
    internal class MilkShake : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("MilkShake is Good");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int noOfCups);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int noOfCups)
        {
            Console.WriteLine($"Prepare {noOfCups} cups of Tea");
            return new Tea();
        }
    }
    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int noOfCups)
        {
            Console.WriteLine($"Prepare {noOfCups} mugss of Coffee");
            return new Coffee();
        }
    }
    internal class MilkShakeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int noOfCups)
        {
            Console.WriteLine($"Prepare {noOfCups} glasses of Milkshake");
            return new MilkShake();
        }
    }

    public class HotDrinkMachine
    {
        //This Enum Breaks The OCP as any addition of drink requires modification of this class
        //public enum AvailableDrink
        //{
        //    Tea, Coffee
        //}
        //private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();
        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("Factory.AbstractFactory." + Enum.GetName(typeof(AvailableDrink),drink) + "Factory"));
        //        factories.Add(drink, factory);
        //    }
        //}
        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return factories[drink].Prepare(amount);
        //}

        private List<Tuple<string, IHotDrinkFactory>> namedFactories =
            new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    namedFactories.Add(Tuple.Create(
                      t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t)));
                }
            }
        }
        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks");
            for (var index = 0; index < namedFactories.Count; index++)
            {
                var tuple = namedFactories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i) // c# 7
                    && i >= 0
                    && i < namedFactories.Count)
                {
                    Console.Write("Specify amount: ");
                    s = Console.ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return namedFactories[i].Item2.Prepare(amount);
                    }
                }
                Console.WriteLine("Incorrect input, try again.");
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();

            //Calls for commented code
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 2);
            //drink.Consume();
            //var secondDrink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 5);
            //secondDrink.Consume();

            IHotDrink drink = machine.MakeDrink();
            drink.Consume();


        }
    }
}
