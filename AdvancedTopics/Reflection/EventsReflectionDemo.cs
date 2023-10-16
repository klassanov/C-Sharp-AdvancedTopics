using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics
{
    internal class EventsReflectionDemo
    {
        public event EventHandler<int>? MyEvent;

        public void Handler(object sender, int arg)
        {
            Console.WriteLine($"I've just got {arg}");
        }

        public void RaiseEvent(int arg)
        {
            this.MyEvent?.Invoke(this, arg);
        }
    }
}
