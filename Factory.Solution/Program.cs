using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Solution
{
    public class Point
    {
        private double x, y;
        private Point(double x,double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
        }

        public override string ToString()
        {
            return $"x:{x}, y:{y}";
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var point = Point.NewPolarPoint(2*Math.Sqrt(2),(3*Math.PI)/4);
            Console.WriteLine(point);
        }
    }
}
