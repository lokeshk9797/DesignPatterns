using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.LSP
{
    public class Rectangle
    {
        /*
        Liskov substitution principle states
        that if S is a subtype of T, then objects of type T may be replaced(or substituted) with objects of type S.
        */

        //public int Width { get; set; }
        //public int Height { get; set; }

        // to implement Liscov Substitution Priniple
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }

        public Rectangle()
        {

        }
        public Rectangle(int height, int width)
        {
            Height = height;
            Width = width;
        }
        public override string ToString()
        {
            return $"{nameof(Width)}:{Width} ,{nameof(Height)}:{Height} ";

        }
    }

    public class Square : Rectangle
    {
        //public new int Width
        //{
        //  set { base.Width = base.Height = value; }
        //}

        //public new int Height
        //{ 
        //  set { base.Width = base.Height = value; }
        //}
        
        //instead of creating new properties we override them

        public override int Width // nasty side effects will learn later
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    class Program
    {
        static public int Area(Rectangle r) => r.Width * r.Height;

        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(10, 15);
            Console.WriteLine($"Rectangle {rc} has area {Area(rc)}");

            // should be able to substitute a base type for a subtype
            /*Square*/
            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}
