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
            TestSolve(c => { return new SolverM2(c); });
            //M2Stats();

            Console.WriteLine("Finished!");
            Console.ReadKey();
        }

        const int StateBufferOK = 0;
        const int StateBufferRotated = 1;
        const int StateBufferWrong = 2;

        const int StateNotSolved = 0;
        const int StateSolved = 1;
        const int StateHasParity = 2;
        const int StateUFBDSwapped = 4;

        static void M2Stats()
        {
            var totalSolves = 1000;
            var cube = new Cube();
            var solver = new SolverM2(cube);
            var states = new Dictionary<int, int>();
            for (int i = 0; i < totalSolves; ++i)
            {
                cube.Reset();

                var seed = DateTime.Now.Ticks;
                var scrambleSequence = cube.Scramble(20+(int)(seed % 20), (int)seed);
                cube.Apply(scrambleSequence);
                solver.Solve(SolverM2.M2SolveMode.CornersParityEdges);
                
                var state = GetState(solver, cube);
                if (states.ContainsKey(state))
                {
                    states[state]++;
                }
                else
                {
                    states[state] = 1;
                }

                //if (state != 0 && state != 1 && state != 11 && state != 19)
                //{
                //    Console.WriteLine("FAIL");
                //    Console.WriteLine("Seed: {0}", seed);
                //    Console.Write("Seq: ");
                //    foreach (var turn in scrambleSequence)
                //    {
                //        Console.Write(turn); Console.Write(" ");
                //    }
                //    Console.WriteLine();
                //    Console.WriteLine("Parity: {0}", solver.HasParity ? "yes" : "no");
                //    solver.Describe();

                //    break;
                //}
            }

            Console.WriteLine();
            Console.WriteLine("STATS");
            foreach (int key in states.Keys)
            {
                Console.WriteLine("Key {0} = {1} times", key, states[key]);
            }
        }

        /// <summary>
        /// This test shows that either the cube is solved and had no parity OR the UF BD cubies are swapped and it had parity
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        static int GetState(SolverM2 solver, Cube cube)
        {
            var state = cube.IsSolved() ? StateSolved : StateNotSolved;
            state += (solver.HasParity ? StateHasParity : 0);

            var cubie = cube[Sticker.sUF];
            if (cubie.Type != Sticker.sUF)
            {
                state += StateUFBDSwapped;
            }

            return state;
        }

        /// <summary>
        /// This test shows that the buffer always contains the right cubie rotated right!
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        static int GetStateBufferTest(SolverM2 solver, Cube cube)
        {
            var cubie = cube[Sticker.sDF];
            if (cubie.Type == Sticker.sDF && !cubie.IsCorrect)
            {
                return StateBufferRotated;
            }
            else if (cubie.Type != Sticker.sDF)
            {
                return (int)cubie.Type;
            }
            else
                return StateBufferOK;
        }

        /// <summary>
        /// This test shows that when the DB cubie is not correct (that is contains the UF cubie) this cubie is always oriented 98 (rotated twice around x-axis)
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        static int GetStateDBOrientationTest(SolverM2 solver, Cube cube)
        {
            var cubie = cube[Sticker.sDB];
            if (cubie.Type == Sticker.sDB && !cubie.IsCorrect)
            {
                return StateBufferRotated;
            }
            else if (cubie.Type != Sticker.sDB)
            {
                return (int)cubie.Orientation;
            }
            else
                return StateBufferOK;
        }

        /// <summary>
        /// This test shows that when the UF cubie is not correct (that is contains the DB cubie) this cubie is always oriented 98 (rotated twice around x-axis)
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        static int GetStateUFOrientationTest(SolverM2 solver, Cube cube)
        {
            var cubie = cube[Sticker.sUF];
            if (cubie.Type == Sticker.sUF && !cubie.IsCorrect)
            {
                return StateBufferRotated;
            }
            else if (cubie.Type != Sticker.sUF)
            {
                return (int)cubie.Orientation;
            }
            else
                return StateBufferOK;
        }

        /// <summary>
        /// This test shows that the DB cubie is either correct or contains the UF cubie. either is 50-50
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        static int GetStateDBTest(SolverM2 solver, Cube cube)
        {
            var cubie = cube[Sticker.sDB];
            if (cubie.Type == Sticker.sDB && !cubie.IsCorrect)
            {
                return StateBufferRotated;
            }
            else if (cubie.Type != Sticker.sDB)
            {
                return (int)cubie.Type;
            }
            else
                return StateBufferOK;
        }

        /// <summary>
        /// This test shows that the UF cubie is either correct or contains the DB cubie. either is 50-50
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        static int GetStateUFTest(SolverM2 solver, Cube cube)
        {
            var cubie = cube[Sticker.sUF];
            if (cubie.Type == Sticker.sUF && !cubie.IsCorrect)
            {
                return StateBufferRotated;
            }
            else if (cubie.Type != Sticker.sUF)
            {
                return (int)cubie.Type;
            }
            else
                return StateBufferOK;
        }

        /// <summary>
        /// This test shows that the cubie opposite the buffer (UB) always contains the right cubie rotated right!
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        static int GetStateOppositeBufferTest(SolverM2 solver, Cube cube)
        {
            var cubie = cube[Sticker.sUB];
            if (cubie.Type == Sticker.sUB && !cubie.IsCorrect)
            {
                return StateBufferRotated;
            }
            else if (cubie.Type != Sticker.sUB)
            {
                return StateBufferWrong;
            }
            else
                return StateBufferOK;
        }

        static void TestSolve(Func<Cube, ISolver> solverCreator)
        {
            var totalSolves = 100;
            var cube = new Cube();
            var solver = solverCreator(cube);

            Console.WriteLine("Performing {0} solves using {1}", totalSolves, solver);
            for (int i = 0; i < totalSolves; ++i)
            {
                cube.Reset();

                var seed = DateTime.Now.Ticks;
                var scrambleSequence = cube.Scramble(4+(int)(seed % 4), (int)seed);
                cube.Apply(scrambleSequence);
                solver.Solve();

                if (!cube.IsSolved())
                {
                    Console.WriteLine("Not solved (seed={0})!", seed);
                    Console.Write("Seq: ");
                    foreach (var turn in scrambleSequence)
                    {
                        Console.Write(turn); Console.Write(" ");
                    }
                    Console.WriteLine();
                    solver.Describe();
                    break;
                }
                if (i % 100 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }

        static void TestMinMaxSteps()
        {
            var totalSolves = 3000;
            var cube = new Cube();
            var solver = new SolverClassic(cube);

            var maxSteps = 0;
            var minSteps = 100000;
            var stepAverage = 0;

            Console.WriteLine("Performing {0} solves using {1}", totalSolves, solver);
            for (int i = 0; i < totalSolves; ++i)
            {
                cube.Reset();

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
