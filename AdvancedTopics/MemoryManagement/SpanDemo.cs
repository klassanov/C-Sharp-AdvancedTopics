using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.MemoryManagement
{
    public class SpanDemo
    {
        public static void Demo()
        {
            unsafe
            {
                //1.Pointing to managed memory on the Stack -> automatically deallocated when the method exits
                byte* ptr = stackalloc byte[100];
                Span<byte> memory = new Span<byte>(ptr, 100);// reference to all the 100 byte cells
                memory[0] = 123;

                Console.WriteLine(((int)ptr).ToString("X")); //Memory address of the first byte

                //The pointer points to the first array cell in the memory, so the following 2 should be the same
                Console.WriteLine(*ptr); //Value contained in the address
                Console.WriteLine(memory[0]); //Value contained in the address  


                //2.Pointing to unmanaged memory -> we should remember to deallocate it
                IntPtr unmanagedPtr = Marshal.AllocHGlobal(100);
                Span<byte> unmanagedMemory = new Span<byte>(unmanagedPtr.ToPointer(), 100);
                Marshal.FreeHGlobal(unmanagedPtr);
            }

            //3.Pointing to managed heap memory pointing
            //span of chars
            char[] letters = "hello".ToCharArray();
            Span<char> lettersMemory = letters;
            Console.WriteLine(letters);
            lettersMemory.Fill('X'); //Overwrites all the elements of the array
            Console.WriteLine(letters);
            lettersMemory.Clear();
            Console.WriteLine(letters); //Blank line


            //span of a string: beacause of string immutability, it should be a read-only span
            ReadOnlySpan<char> stringSpan = "hi there!".AsSpan();
            Console.WriteLine($"Our span has {stringSpan.Length} elements");
        }
    }
}
