using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.Dynamic.DynamicVisitor
{
    internal class DynamicRecursiveVisitorDemo
    {
        public static void Demo()
        {
            // 1+2+3
            var expr = new Addition(
                new Addition(
                    new Literal(1),
                    new Literal(2)),
                new Literal(3));

            //Classical way
            //var sb = new StringBuilder();
            //var visitor = new ExpressionPrinter();
            //expr.Accept(visitor, sb);
            //Console.WriteLine(sb.ToString());

            //Dynamic way
            var sb = new StringBuilder();
            ExpressionPrinter.Print((dynamic)expr, sb);
            Console.WriteLine(sb.ToString());
        }
    }

    public abstract class Expression
    {
        //Classical way
        //public abstract void Accept(ExpressionPrinter visitor, StringBuilder sb);
    }

    public class Literal : Expression
    {
        public int Value { get; private set; }

        public Literal(int value)
        {
            Value = value;
        }

        //Classical way
        //public override void Accept(ExpressionPrinter visitor, StringBuilder sb)
        //{
        //    ExpressionPrinter.Print(this, sb);
        //}
    }

    public class Addition : Expression
    {
        public Expression Left { get; private set; }

        public Expression Right { get; private set; }

        public Addition(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        //Classical way
        //public override void Accept(ExpressionPrinter visitor, StringBuilder sb)
        //{
        //    sb.Append("(");
        //    Left.Accept(visitor, sb);
        //    sb.Append("+");
        //    Right.Accept(visitor, sb);
        //    sb.Append(")");
        //}
    }


    public class ExpressionPrinter
    {
        public static void Print(Literal literal, StringBuilder sb)
        {
            sb.Append(literal.Value);
        }

        //Dynamic way
        public static void Print(Addition addition, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)addition.Left, sb);
            sb.Append("+");
            Print((dynamic)addition.Right, sb);
            sb.Append(")");
        }
    }
}
