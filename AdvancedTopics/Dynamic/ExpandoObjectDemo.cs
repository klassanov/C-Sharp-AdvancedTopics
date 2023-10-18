using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdvancedTopics.Dynamic
{
    public class ExpandoObjectDemo
    {
        public void Test()
        {
            //U can use Developer Command Prompt > csi.exe
            //Alternatively C# Interactive window where U have Intellisense


            dynamic person = new ExpandoObject();

            //Properties
            person.Name = "John";
            person.Age = 38;

            Console.WriteLine($"{person.Name}, {person.Age}");
            Console.WriteLine($"{person.Name.GetType()}, {person.Age.GetType()}");

            //Nested properties
            person.Address = new ExpandoObject();
            person.Address.Street = "Street 1";
            person.Address.ZipCode = "1000";

            Console.WriteLine($"{person.Name} is {person.Age} years old and lives in {person.Address.Street}, {person.Address.ZipCode}");


            //Methods
            person.SayHello = new Action(() =>
            {
                Console.WriteLine("Hello");
            });
            person.SayHello();


            person.Multiply = new Func<int, int, int>((x, y) =>
            {
                return x * y;
            });
            Console.WriteLine(person.Multiply(5, 6));



            //Events
            person.FallsIll = null;
            person.FallsIll += new EventHandler<dynamic>((sender, args) =>
            {
                Console.WriteLine($"We need a doctor for {args}");
            });

            person.FallsIll?.Invoke(person, person.Name);

            //Internally expando object implements IDictionary interface (as well as  INotifyPropertyChanged interface)
            //=> explicit conversion to the type
            var dict = (IDictionary<string, object>)person;
            foreach (var key in dict.Keys)
            {
                Console.WriteLine($"{key}:{dict[key]}");
            }

            //Check for property exsistence
            bool hasNameProperty = dict.ContainsKey("Name");

            //Add a key to the dictionary <=> Add a property to the expando object
            dict["LastName"] = "Petrov";
            Console.WriteLine(person.LastName);

            //Remove a key from the dictionary <=> Remove a property from the expando object
            dict.Remove("Name");
            //Console.WriteLine(person.Name); //Error



            //Receiving Notifications of Property Changes => explicit cast to the interface type and subscribe
            ((INotifyPropertyChanged)person).PropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"Property {e.PropertyName} changed");
            };

            person.Age += 30;






        }
    }
}
