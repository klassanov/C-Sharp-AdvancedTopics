using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.ExtensionMethods
{
    public static class Extensions
    {
        /*
        * U can do extension methods on:
        * - reference types
        * - value types 
        * - interfaces
        * - tuples, value tuples
        * - generics
        * - delegates
        * - any type: object or making the method generic
        * 
        * Be careful with the inheritance, polymorphysm is not going to work, it looks at the actual type
        * Cannot override object methods, such as to string
        */


        //reference type
        public static int Measure(this Foo foo)
        {
            return foo.Name.Length;
        }

        //reference type
        public static int Measure(this FooDerived fooDerived)
        {
            return 1;
        }

        //value type
        public static string ToBinary(this int n)
        {
            return Convert.ToString(n, 2);
        }

        //interface
        public static void Save(this ISerializable serializable)
        {
        }

        //object
        //even if U have overriden it it will still call the object ToString method
        public static string ToString(this Foo foo)
        {
            return foo.Name;
        }


        //tuple
        public static Person ToPerson(this (string name, int age) data)
        {
            return new Person() { Name = data.name, Age = data.age };
        }

        //generics
        public static int Measure<T, U>(this Tuple<T, U> t)
        {
            return t.Item2.ToString().Length;
        }

        //delegate, we want to measure how much time a given func call takes
        public static long Measure(this Func<int> func)
        {
            var st = new Stopwatch();
            st.Start();
            func();
            st.Stop();
            return st.ElapsedMilliseconds;
        }

        //any type: object
        public static void Example1 (this object obj)
        {

        }

        //any type: generic
        public static void Example2<T>(this T obj)
        {

        }
    }
}
