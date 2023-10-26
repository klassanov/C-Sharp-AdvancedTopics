using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.ExtensionMethods
{
    internal class ExtensionMethodsDemo
    {
        public static void Demo()
        {
            int number = 255;
            Console.WriteLine($"The binary representation of {number} is {number.ToBinary()}");


            //Polymorphysm demo
            var fooDerived = new FooDerived(); //FooDerived type
            Foo foo = fooDerived; //Foo type

            Console.WriteLine($"fooDerived name: {fooDerived.Name}"); //FooDerived
            Console.WriteLine($"foo name: {foo.Name}"); //FooDerived
            Console.WriteLine();
            Console.WriteLine($"fooDerived measure: {fooDerived.Measure()}"); //1
            Console.WriteLine($"foo measure: {foo.Measure()}"); //10

            Console.WriteLine();

            //It does not call the extension method
            Console.WriteLine(foo.ToString());
            //U can make it work as below
            Console.WriteLine(Extensions.ToString(foo));

            //Tuple demo
            var me = ("Alex", 38);
            var person = me.ToPerson();
            Console.WriteLine(person);


            //Generic tuple demo
            Console.WriteLine(Tuple.Create("Alex", 38).Measure());

            
            //Delegates
            Func<int> Calculate = delegate
            {
                Thread.Sleep(2000);
                return 100;
            };
            var st = Calculate;
            Console.WriteLine(Calculate.Measure());

            //Extend Any type by extending object
            "hello".Example1();

            //Extend Any type by extending generic type
            "hello".Example2();


        }
    }
}
