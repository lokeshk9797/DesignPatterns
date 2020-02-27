using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Solution
{
    //public class Point
    //{
    //    private double x, y;
    //    private Point(double x,double y)
    //    {
    //        this.x = x;
    //        this.y = y;
    //    }

    //    public static Point NewCartesianPoint(double x, double y)
    //    {
    //        return new Point(x, y);
    //    }
    //    public static Point NewPolarPoint(double rho, double theta)
    //    {
    //        return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
    //    }

    //    public override string ToString()
    //    {
    //        return $"x:{x}, y:{y}";
    //    }
    //}
    //class Program
    //{

    //    static void Main(string[] args)
    //    {
    //        var point = Point.NewPolarPoint(2*Math.Sqrt(2),(3*Math.PI)/4);
    //        Console.WriteLine(point);
    //    }
    //}

    public class Point
    {
        private double x, y;

        protected Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(double a,
          double b, // names do not communicate intent
          CoordinateSystem cs = CoordinateSystem.Cartesian)
        {
            switch (cs)
            {
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    x = a;
                    y = b;
                    break;
            }

        }

        // factory property
        //Returns a new Point object everytime
        public static Point Origin => new Point(0, 0);

        // singleton field
        //returns the Value of the property , Better Way
        public static Point Origin2 = new Point(0, 0);

        // factory method
       

        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        //Inner Factory
        // make it lazy
        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }
            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

   
    class Demo
    {
        static void Main(string[] args)
        {
            var p1 = new Point(2, 3, Point.CoordinateSystem.Cartesian);
            var origin = Point.Origin;

            //Inner Factory Call
            var p2 = Point.Factory.NewCartesianPoint(1, 2);
        }
    }
}
