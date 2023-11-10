using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.DuckTyping
{
    public class DuckTypingDemo
    {
        ref struct Foo //: IDisposable -> not legal: ref structs cannot implement interfaces, but we can implement Dispose() method
                       //  and then use Foo inside a using statement
        {
            public Foo()
            {
                Console.WriteLine("Creating a new Foo ref struct");
            }

            public void Dispose()
            {
                Console.WriteLine("Disposing Foo");
            }
        }

        public static void Demo()
        {
            //DuckTyping example: 
            using (Foo foo = new Foo())
            {
                Console.WriteLine("Hello from the using block");
            }
        }
    }
}
