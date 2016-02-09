using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class BasePlan : IContainsMoves
    {
        protected Cube Cube { get; set; }
        protected List<Turn> Turns { get; set; }

        public BasePlan(Cube cube)
        {
            this.Cube = cube;
            this.Turns = new List<Turn>();
        }

        public virtual void Describe()
        {
            Console.WriteLine("PLAN with {0} turns: ", this.Turns.Count());
        }

        public IEnumerable<Turn> GetTurns()
        {
            return this.Turns;
        }

        protected BasePlan Add(Turn[] turns)
        {
            this.Turns.AddRange(turns);
            return this;
        }

        protected BasePlan Add(Turn turn)
        {
            this.Turns.Add(turn);
            return this;
        }

        private BasePlan L() { return this.Add(Turn.L); }
        private BasePlan L_() { return this.Add(Turn.L_); }
        private BasePlan R() { return this.Add(Turn.R); }
        private BasePlan R_() { return this.Add(Turn.R_); }
        private BasePlan U() { return this.Add(Turn.U); }
        private BasePlan U_() { return this.Add(Turn.U_); }
        private BasePlan D() { return this.Add(Turn.D); }
        private BasePlan D_() { return this.Add(Turn.D_); }
        private BasePlan F() { return this.Add(Turn.F); }
        private BasePlan F_() { return this.Add(Turn.F_); }
        private BasePlan B() { return this.Add(Turn.B); }
        private BasePlan B_() { return this.Add(Turn.B_); }

    }
}
