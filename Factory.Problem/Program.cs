using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Problem
{
    using System;

    namespace DotNetDesignPatternDemos.Creational.Factories
    {
        //The point classes requires lots of constructors and two contructors with same number of arguements cannot exists
        public class Point
        {

            private double x, y;
            public enum CoordinateSystem
            {
                Cartesian,
                Polar
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

            public override string ToString()
            {
                return $"x:{x} , y: {y}";
            }

           
        }
        class Demo
        {
            static void Main(string[] args)
            {
                var p1 = new Point(2, 3, Point.CoordinateSystem.Cartesian);
                Console.WriteLine(p1);
                
            }
        }
    }

}
