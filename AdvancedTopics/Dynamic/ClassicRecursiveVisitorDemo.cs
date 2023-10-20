using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.Dynamic.ClassicVisitor
{
    public class ClassicRecursiveVisitorDemo
    {
        public static void Demo()
        {
            // (5 + ((6+7) + 8 ))
            var addition = new Addition(
                new Literal(5),
                new Addition(
                    new Addition(new Literal(6), new Literal(7)),
                    new Literal(8)));


            var visitor = new Visitor();
            Console.WriteLine(addition.Accept(visitor));
        }
    }

    public interface IExpression
    {
        string Accept(Visitor visitor);
    }

    public class Literal : IExpression
    {
        public int Value { get; }

        public Literal(int value)
        {
            Value = value;
        }

        public string Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class Addition : IExpression
    {
        public IExpression Left { get; }
        public IExpression Right { get; }

        public Addition(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public string Accept(Visitor visitor)
        {
            return $"({this.Left.Accept(visitor)} + {this.Right.Accept(visitor)})";
        }
    }

    public class Visitor
    {
        public string Visit(Literal literal)
        {
            return literal.Value.ToString();
        }
    }
}
