using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.MemoryManagement
{
    internal class MemoryManagersDemo
    {
        public static void Demo()
        {
            var memoryManager = new ManagedMemoryManager();
            var memoryBeforeAllocationBytes = GC.GetTotalMemory(false);
            memoryManager.AllocateMemory();
            var memoryAfterAllocationBytes = GC.GetTotalMemory(false);
            Console.WriteLine($"Memory allocated to the list: {memoryAfterAllocationBytes - memoryBeforeAllocationBytes} bytes");

            var nativeLibrariesManager = new NativeLibrariesManager();
            nativeLibrariesManager.DisplayMessage();

        }
    }

    public class ManagedMemoryManager
    {
        private readonly List<int> _numbers;

        public ManagedMemoryManager()
        {
            _numbers = new List<int>();
        }

        public void AllocateMemory()
        {
            for (var i = 0; i < 1000000; i++)
            {
                _numbers.Add(i);
            }
        }
    }

    public class NativeLibrariesManager
    {
        //Interoperability with unmanaged code libraries

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, int options);

        public void DisplayMessage()
        {
            MessageBox(IntPtr.Zero, "Hello world", "Caption", 2);
        }
    }


}
