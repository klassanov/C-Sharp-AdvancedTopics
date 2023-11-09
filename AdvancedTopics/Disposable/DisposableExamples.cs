using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.Disposable
{
    public class DisposableDemo
    {
        public static void Demo()
        {
            //Constructor and dispose called
            using (var mySecondClass = new MySecondClass())
            {
            }

            using(new SimpleTimer())
            {
                Thread.Sleep(1000);
            }

            var st = Stopwatch.StartNew();
            using(GeneralPurposeDisposable.Create(
                        start: ()=> st.Start(),
                        end: ()=> st.Stop()))
            {
                Console.WriteLine("something here");
                Thread.Sleep(1000);
            }

            Console.WriteLine($" {st.ElapsedMilliseconds} ms elapsed");
        }
    }

    //Implemented by VS intellisense
    public class MyClass : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MyClass()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class MySecondClass : IDisposable
    {
        public MySecondClass()
        {
            Console.WriteLine("Hello");
        }

        public void Dispose()
        {
            Console.WriteLine("Goodbye");
        }
    }

    public class SimpleTimer : IDisposable
    {
        private Stopwatch st;

        public SimpleTimer()
        {
            st = Stopwatch.StartNew();

        }

        public void Dispose()
        {
            st.Stop();
            Console.WriteLine($"{st.ElapsedMilliseconds} ms elapsed.");
        }
    }

    public class GeneralPurposeDisposable: IDisposable
    {
        private readonly Action end;

        private GeneralPurposeDisposable(Action start, Action end)
        {
            this.end = end;
            start();
        }

        public static GeneralPurposeDisposable Create(Action start, Action end)
        {
            return new GeneralPurposeDisposable(start, end);
        }

        public void Dispose()
        {
            end();
        }
    }
}
