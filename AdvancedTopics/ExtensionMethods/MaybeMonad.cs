using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.ExtensionMethods
{
    public static class MaybeExtensions
    {
        public static TResult? With<TInput, TResult>(this TInput? o, Func<TInput, TResult> evaluator)
            where TInput : class
            where TResult : class
        {
            if (o == null) return null;
            return evaluator(o);
        }

        public static TResult? WithValue<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
             where TInput : struct            
        {
            return evaluator(o);
        }

        public static TInput? If<TInput>(this TInput? o, Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }

        public static TInput? Do<TInput>(this TInput? o, Action<TInput> action) where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        public static TResult? Return<TInput, TResult>(this TInput? o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if (o == null) return failureValue;
            return evaluator(o);
        }
    }


    internal class MaybeMonad
    {
        public static void Demo()
        {
            //Old style and New style, the results should be the same
            var p = new DemoPerson();
            var r1 = MyOldStyleMethod(p);
            var r2 = MyExtMethodStyleMethod(p);

            //New style with extension methods that allows fluent interface
            var postcode = p.With(p => p.Address)
                            .With(x => x.PostCode);


            var number = p.With(x => x.Address).Number.WithValue(x => x == 5);
        }

        public static string? MyExtMethodStyleMethod(DemoPerson p)
        {
            //Nice and concise evaluation chain
            var result = p.If(HasMedicalRecord)
                            .With(x => x.Address)
                            .Do(CheckAddress)
                            .Return(x => x.PostCode, "UNKNOWN");


            return result;
        }

        public static string MyOldStyleMethod(DemoPerson p)
        {
            //Old style
            string postcode = "UNKNOWN";
            if (p != null)
            {
                if (HasMedicalRecord(p) && p.Address != null)
                {
                    CheckAddress(p.Address);
                    if (p.Address.PostCode != null)
                    {
                        postcode = p.Address.PostCode;
                    }
                }
            }

            return postcode;
        }

        public static bool HasMedicalRecord(DemoPerson p)
        {
            return true;
        }

        public static void CheckAddress(Address a)
        {
            Console.WriteLine("test");
        }
    }

    class DemoPerson
    {
        public Address Address { get; set; }

    }

    class Address
    {
        public string PostCode { get; set; }
        public int Number { get; set; }

    }
}
