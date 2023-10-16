using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics
{
    internal class DynamicsDemo
    {
        public void Demo()
        {
            //Purpose of dynamic: LATE BINDING (at runtime)
            //Original Reason: interoperability with other weak-typed languages such as Python
            //The type of the object is decided on the basis of the data it holds on the right-hand side during run-time

            dynamic d = "Hello";
            Console.WriteLine(d.GetType());
            Console.WriteLine(d.Length);

            d += " world";
            Console.WriteLine(d);

            try
            {
                Console.WriteLine(d.Area);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
            }

            //Assign it to a different type
            d = 123;
            Console.WriteLine(d);
            Console.WriteLine(d.GetType());
        }


        //Obtain a dynamic custom type
        public void Demo2()
        {
            //Way 1
            //dynamic w = new Widget();
            //Way 2
            
            var wid = new Widget() as dynamic;
            Console.WriteLine(wid.Hello);
            Console.WriteLine(wid[3]);

            wid.WhatIsThis();
        }
    }


    

    //Have dynamic behavior
    internal class Widget: DynamicObject
    {
        //Trick to have it as a dynamic object inside itself
        dynamic This => this;

        public void WhatIsThis()
        {
            //Console.WriteLine(this.Hello); //Error
            Console.WriteLine(This.World); //Will work
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            result = binder.Name;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
        {
            result =  "*";
            return true;
        }
    }
}
