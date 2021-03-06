﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class BaseSolver : ISolver, IContainsTurns
    {
        public BaseSolver(Cube cube)
        {
            this.Cube = cube;
            this.Turns = new List<Turn>();
            this.Description = new List<string>();
            Clear();
        }

        public List<string> Description { get; private set; }

        public int NumberOfCornerSteps { get; protected set; }

        public int NumberOfEdgeSteps { get; protected set; }

        public virtual int TotalStepsWithoutParity
        {
            get
            {
                throw new NotImplementedException(nameof(TotalStepsWithoutParity));
            }
        }

        protected Cube Cube { get; set; }

        protected List<Turn> Turns { get; set; }

        public void Clear()
        {
            this.Turns.Clear();
            this.Description.Clear();
            this.NumberOfCornerSteps = 0;
            this.NumberOfEdgeSteps = 0;
        }

        public virtual void Describe()
        {
            Console.Write("Desc: ");
            foreach (var s in this.Description)
            {
                Console.Write(s + " - ");
            }
            Console.WriteLine();
        }

        public IEnumerable<Turn> GetTurns()
        {
            return this.Turns;
        }

        public virtual void Solve()
        {
            throw new NotImplementedException(nameof(Solve));
        }
        protected BaseSolver Add(Turn[] turns, string description = null)
        {
            if (description != null)
            {
                this.Description.Add(description);
            }
            this.Turns.AddRange(turns);
            return this;
        }

        protected BaseSolver Add(Turn turn, string description = null)
        {
            if (description != null)
            {
                this.Description.Add(description);
            }
            this.Turns.Add(turn);
            return this;
        }

        private BaseSolver B() { return this.Add(Turn.B); }

        private BaseSolver B_() { return this.Add(Turn.B_); }

        private BaseSolver D() { return this.Add(Turn.D); }

        private BaseSolver D_() { return this.Add(Turn.D_); }

        private BaseSolver F() { return this.Add(Turn.F); }

        private BaseSolver F_() { return this.Add(Turn.F_); }

        private BaseSolver L() { return this.Add(Turn.L); }
        private BaseSolver L_() { return this.Add(Turn.L_); }
        private BaseSolver R() { return this.Add(Turn.R); }
        private BaseSolver R_() { return this.Add(Turn.R_); }
        private BaseSolver U() { return this.Add(Turn.U); }
        private BaseSolver U_() { return this.Add(Turn.U_); }
    }
}
