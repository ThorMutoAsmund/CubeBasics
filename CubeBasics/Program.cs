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
            Test1();

            Console.WriteLine("Finished!");
            Console.ReadKey();
        }

        static void Test1()
        {
            var cube = new Cube();
            cube.M().M();
            Console.WriteLine(cube[Sticker.eUB].Oriented(OSticker.UB));
        }

        static void Test2()
        { 
            var cube = new Cube();
            for (int i = 0; i < 1000; ++i)
            {
                var fixseed = 160;// 635904859886572548;// DateTime.Now.Ticks;
                var seed = DateTime.Now.Ticks;
                var scrambleSequence = cube.Scramble(25, (int)seed); // 160
                cube.Apply(scrambleSequence);
                var solver = new SolverClassic(cube);
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
        }
    }
}
