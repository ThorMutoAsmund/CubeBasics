using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class SolverM2
    {
        public Cube Cube { get; set; }

        public SolverM2(Cube cube)
        {
            this.Cube = cube;
        }

        public void Solve()
        {
            var plan = CreatePlan();
            this.Cube.Apply(plan);
        }

        public Plan CreatePlan()
        {
            var plan = new Plan(this.Cube);
            plan.Create();

            return plan;
        }


        public class Plan : IContainsMoves
        {
            public Cube Cube { get; set; }

            public Plan(Cube cube)
            {
                this.Cube = cube;
            }

            public IEnumerable<Turn> GetMoves()
            {
                throw new NotImplementedException();
            }

            public void Create()
            {

            }
        }
    }
}
