using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.ContinuationPassingStyle
{
    enum WorkflowResult
    {
        Success,
        Failure
    }

    internal class ContinuationPassingDemo
    {
        public static void Demo()
        {
            var solver = new QuadraticEquationSolver();
            Tuple<Complex, Complex>? solution = null;
            var flag = solver.Start(1, 5, 1, out solution);
            if(flag==WorkflowResult.Success)
            {
                Console.WriteLine(solution);
            }
            else
            {
                Console.WriteLine(WorkflowResult.Failure);
            }
        }
    }


    class QuadraticEquationSolver
    {

        //Solve ax^2+bx+c == 0
        public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex>? solution)
        {
            var disc = b * b - 4 * a * c;
            if (disc < 0)
            {
                // return SolveComplex(a, b, c, disc); we suppose we don't know how to do it
                solution = null;
                return WorkflowResult.Failure;
            }
            else
            {
                return SolveSimple(a, b, c, disc, out solution);
            }
        }

        private WorkflowResult SolveSimple(double a, double b, double c, double disc, out Tuple<Complex, Complex>? solution)
        {
            var squareRootDisc = Math.Sqrt(disc);
            solution = Tuple.Create(
             new Complex(-b + squareRootDisc / (2 * a), 0),
             new Complex(-b - squareRootDisc / (2 * a), 0)
            );

            return WorkflowResult.Success;
        }

        private Tuple<Complex, Complex> SolveComplex(double a, double b, double c, double disc)
        {
            var squareRootDisc = Complex.Sqrt(new Complex(disc, 0));
            return Tuple.Create(
              -b + squareRootDisc / (2 * a),
              -b - squareRootDisc / (2 * a)
            );
        }
    }
}
