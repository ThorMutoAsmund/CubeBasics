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
            Test2();

            Console.WriteLine("Finished!");
            Console.ReadKey();
        }

        static void Test1()
        {
            var cube = new Cube();
            var scrambleSequence = cube.Scramble(25, 160255);
            cube.Apply(scrambleSequence);
            var solver = new SolverClassic(cube);
            solver.SolveEdges();
            if (!cube.AreEdgesSolved())
            {
                Console.WriteLine("Edges not solved!");
                Console.Write("Seq: ");
                foreach (var turn in scrambleSequence)
                {
                    Console.Write(turn); Console.Write(" ");
                }
                Console.WriteLine();
                solver.DescribePlan();
            }
        }

        static void Test2()
        { 
            var cube = new Cube();
            for (int i = 0; i < 1000; ++i)
            {
                var fixseed = 160;// 635904859886572548;// DateTime.Now.Ticks;
                var seed = DateTime.Now.Ticks;
                var scrambleSequence = cube.Scramble(25, (int)160); // 160
                cube.Apply(scrambleSequence);
                var solver = new SolverClassic(cube);
                solver.Solve();
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
        }
    }
}
