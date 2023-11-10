using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.DuckTyping
{
    public class MixingsDemo
    {
        public static void Demo()
        {
            using (var mc = new MyClass()) { }

            var mc2 = new MyScalar();
            foreach (var item in mc2)
            {
                Console.WriteLine(item);
            }

        }

        public interface IMyDisposable<T>: IDisposable
        {
            //Default implementation
            void IDisposable.Dispose()
            {
                Console.WriteLine($"Disposing {typeof(T).Name}");
            }
        }

        public interface IScalar<T> : IEnumerable<T>
        {
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                yield return (T) this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        public class MyClass: IMyDisposable<MyClass>
        {
        }

        public class MyScalar: IScalar<MyScalar> 
        {
        }
    }
}
