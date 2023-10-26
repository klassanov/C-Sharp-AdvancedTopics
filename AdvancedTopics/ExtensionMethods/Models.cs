using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.ExtensionMethods
{
    public class Foo
    {
        public virtual string Name => "Foo";
    }

    public class FooDerived: Foo
    {
        public override string Name => "FooDerived";
    }

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }



}
