namespace AdvancedTopics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //IntegralTypesOverflow();

            IntegralTypeUnchecked();
            IntegralTypeChecked();

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
    }
}