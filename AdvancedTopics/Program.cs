namespace AdvancedTopics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IntegralTypesOverflow();
        }

        static void IntegralTypesOverflow()
        {
            int a = int.MaxValue;
            int b = 1;
            //Overflow not handled: U get a garbage value
            int c = a + b;
            Console.WriteLine(c);
        }
    }
}