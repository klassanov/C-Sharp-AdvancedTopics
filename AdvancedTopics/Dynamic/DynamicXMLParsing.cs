using System.Dynamic;
using System.Xml.Linq;

namespace AdvancedTopics
{
    internal class DynamicXMLParsing
    {
        public void Test()
        {
            var xmlString = @"
            <people>
                <person name = 'Alex'>
                    <cars>
                        <car brand = 'Opel' horsePower = '75'/>
                    </cars>
                </person>
            </people>
            ";


            //Standard way
            var node = XElement.Parse(xmlString);
            var name = node.Element("person")?.Attribute("name");
            Console.WriteLine(name?.Value);

            //Doing it with Dynamic object
            dynamic xml = new DynamicXMLNode(node);
            Console.WriteLine(xml.person.name);

            var car = xml.person.cars.car;
            Console.WriteLine($"Car: {car.brand}, HP: {car.horsePower}");
        }
    }

    class DynamicXMLNode: DynamicObject
    {
        private readonly XElement node;

        public DynamicXMLNode(XElement node)
        {
            this.node = node;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            var element = this.node.Element(binder.Name);
            if (element !=null)
            {
                result = new DynamicXMLNode(element);
                return true;
            }
            else
            {
                var attribute = this.node.Attribute(binder.Name);
                if(attribute != null)
                {
                    result = attribute.Value;
                    return true;
                }

                else
                {
                    result = null;
                    return false;
                }

            }
        }
    }
}
