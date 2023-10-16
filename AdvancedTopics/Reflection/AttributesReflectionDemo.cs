using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics
{
    internal class AttributesReflectionDemo
    {
        [Repeat(3)]
        public void SomeMethod()
        {
        }
    }

    public class RepeatAttribute: Attribute
    {
        public int Times { get; }

        public RepeatAttribute(int times)
        {
            Times = times;
        }
    }
    
}
