using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSolve(c => { return new SolverClassic(c); });

            Console.WriteLine("Finished!");
            Console.ReadKey();
        }
        
        static void TestSolve(Func<Cube, ISolver> solverCreator)
        {
            var totalSolves = 10;
            var cube = new Cube();
            var solver = solverCreator(cube);

            var maxSteps = 0;
            var minSteps = 100000;
            var stepAverage = 0;

            Console.WriteLine("Performing {0} solves using {1}", totalSolves, solver);
            for (int i = 0; i < totalSolves; ++i)
            {
                var fixseed = 635906536730923589;// DateTime.Now.Ticks;
                var seed = DateTime.Now.Ticks;
                var scrambleSequence = cube.Scramble(25, (int)seed);
                cube.Apply(scrambleSequence);
                solver.Solve();

                var steps = solver.TotalStepsWithoutParity;
                if (steps < minSteps)
                {
                    minSteps = steps;
                }
                if (steps > maxSteps)
                {
                    maxSteps = steps;
                }
                stepAverage += steps;

                if (!cube.IsSolved())
                {
                    Console.WriteLine("Not solved (seed={0}))!", seed);
                    Console.Write("Seq: ");
                    foreach (var turn in scrambleSequence)
                    {
                        Console.Write(turn); Console.Write(" ");
                    }
                    Console.WriteLine();
                    solver.DescribePlan();
                    break;
                }
                if (i % 100 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("Minimum number of steps in a solve: {0}", minSteps);
            Console.WriteLine("Maximum number of steps in a solve: {0}", maxSteps);
            Console.WriteLine("Average number of steps in a solve: {0:N2}", stepAverage / totalSolves);
        }
    }
}
