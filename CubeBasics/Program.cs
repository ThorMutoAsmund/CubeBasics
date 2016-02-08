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
            //var cube = new CubeOld(3);
            //var series = cube.Scramble(10);
            //Console.WriteLine(series);
            //cube.Show();

            var cube = new Cube();
            for (int i = 0; i < 1000; ++i)
            {
                var fixseed = 635904859886572548;// DateTime.Now.Ticks;
                var seed = DateTime.Now.Ticks;
                var scrambleSequence = cube.Scramble(25, (int)seed); // 160
                cube.Apply(scrambleSequence);
                var solver = new SolverM2(cube);
                solver.Solve();
                if (!cube.AreCornersSolved())
                {
                    Console.WriteLine("Corners not solved (seed={0}))!", seed);
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
            Console.WriteLine("Finished!");
            Console.ReadKey();
        }
    }
}
