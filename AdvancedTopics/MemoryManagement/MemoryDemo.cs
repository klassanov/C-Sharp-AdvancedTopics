using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.MemoryManagement
{
    internal class MemoryDemo
    {
        //Structs are passed by value by default (a copy of the original value)
        //U would be passing 4 doubles...=> it would be better to pass it by reference
        public double MeasureDistance(Point p1, Point p2)
        {
            var dx = (p1.X - p2.X);
            var dy = (p1.Y - p2.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        //in keyword ->indicates that the struct will be passed by reference ->memory saving
        //in makes the structure read-only, immutable in the scope of the method
        public double MeasureDistanceBetweenPoints(in Point p1, in Point p2)
        {
            //Not allowed, it is readonly
            //p1 = new Point();
            //p1.X = 5;

            //Compiles, but has no effect since it is modifying the struct, which is something that cannot be done
            //We do the operation on the copy?
            p1.Reset();

            var dx = (p1.X - p2.X);
            var dy = (p1.Y - p2.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void Demo()
        {
            var p1 = new Point(1, 2);
            var p2 = new Point(2, 3);
            var distsance =  MeasureDistanceBetweenPoints(p1, p2);
            Console.WriteLine($"The distance between {p1} and {p2} is {distsance}");


            //Use reference
            var distanceFromOrigin = MeasureDistanceBetweenPoints(p1, Point.originRef);

            //Use value-copy
            Point copyOfOrigin = Point.originRef; //by-value copy, because there is no ref keyword here


            //valid (we do not need it)
            ref readonly var origin = ref Point.originRef;
        }

        
    }


    //When passing a struct around, U pass a full copy of the entire memory of the struct, so
    //in this case it would be 8+8 bytes = 16 bytes
    struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public void Reset()
        {
            X = 0;
            Y = 0;
        }

        //Make a reference to an origin point and pass it around
        private static Point origin = new Point(0, 0);

        public static ref readonly Point originRef => ref origin;
    }
}
