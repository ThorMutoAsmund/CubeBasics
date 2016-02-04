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
            cube.Scramble(40);
            var solver = new SolverM2(cube);
            solver.Solve();
            Console.ReadKey();
        }
    }
}
