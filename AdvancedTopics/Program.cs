namespace AdvancedTopics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //IntegralTypesOverflow();
            //IntegralTypeUnchecked();
            //IntegralTypeChecked();
            //FloatingPointDivisionByZero();
            //FloatingPointNaN();
            //FloatingPointIssues();

            //Events
            EventsReflectionDemo();

            Console.ReadKey();
        }

        static void IntegralTypesOverflow()
        {
            int a = int.MaxValue;
            int b = 1;
            //Overflow not handled: U get a garbage value
            int c = a + b;
            Console.WriteLine(c);
        }

        static void IntegralTypeUnchecked()
        {
            unchecked
            {
                int a = int.MaxValue + 1; //btw = Int.MinValue, превъртел го е
                Console.WriteLine(a);
            }
        }

        static void IntegralTypeChecked()
        {

            //checked by default
            try
            {
                int a = int.MaxValue;
                int b = 1;
                int c = a + b;
                Console.WriteLine(c);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Overflow {ex.Message}");
            }
        }

        static void FloatingPointDivisionByZero()
        {
            double a = 5.0;
            double b = a / 0.0; //Positive infinity, no exception
            double c = b*(-1) / 0.0; //Negarive infinity, no exception
            Console.WriteLine($"{b} {c}");
            //Available as constants
            //double.PositiveInfinity;
            //float.NegativeInfinity;
        }

        static void FloatingPointNaN()
        {
            var positiveInfinity1 = 1.0f / 0.0;
            var positiveInfinity2 = 1.0f / 0.0;
            var nan = positiveInfinity1/positiveInfinity2; //forma indeterminata
            Console.WriteLine(nan);
            var nan2 = 0.0 / 0.0; //forma indeterminata
            var x = double.PositiveInfinity / double.NegativeInfinity; //forma indeterminata

            //Available as constant
            //double.NaN;
            //float.NaN;

            //Check
            Console.WriteLine(double.IsNaN(x));
        }

        static void FloatingPointIssues()
        {
            //Every integer (within some range) can be represented as a sum of powers of 2
            //Not every RATIONAL number can be represented this way, for ex 0.1 = 1/10 results in an infinite binary sequence
            //This is the problem of floating point numbers => some numbers are not exactly what U intend them to be

            //We can obtain surprising results

            double d = 0.1 + 0.2; //Not exactly 0.3
            bool areEqual = d == 0.3;
            Console.WriteLine(areEqual);

            //Comparisons need to be made by using a tolerance value
            if (Math.Abs(d - 0.3) < 1e-8)
            {
                Console.WriteLine("Are equal");
            }
        }

        static void EventsReflectionDemo()
        {
            //Subscribe to the event by using reflection
            var demo = new EventsReflectionDemo();
            var eventInfo = typeof(EventsReflectionDemo).GetEvent("MyEvent");

            //2 possible approaches
            //var handler = typeof(EventsReflectionDemo).GetMethod("Handler");
            var handlerMethodInfo = demo.GetType().GetMethod("Handler");
            var handler = Delegate.CreateDelegate(
                eventInfo!.EventHandlerType!,
                null,
                handlerMethodInfo!);

            eventInfo.AddEventHandler(demo, handler);
            demo.RaiseEvent(123);
        }
    }
}