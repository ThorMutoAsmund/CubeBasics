using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CubeBasics
{
    public class SolverClassic
    {
        public Cube Cube { get; set; }
        private ClassicPlan Plan { get; set; }

        public SolverClassic(Cube cube)
        {
            this.Cube = cube;
            this.Plan = new ClassicPlan(this.Cube);
        }

        public void Solve()
        {
            this.Plan.FixCube();
            this.Cube.Apply(this.Plan);
        }

        public void SolveCorners()
        {
            this.Plan.FixCorners();
            this.Cube.Apply(this.Plan);
        }

        public void SolveEdges()
        {
            this.Plan.FixEdges();
            this.Cube.Apply(this.Plan);
        }

        public void DescribePlan()
        {
            this.Plan.Describe();
        }


        public class ClassicPlan : BasePlan
        {
            private readonly Turn[] cornerSequence = new Turn[] { Turn.U, Turn.B, Turn.L, Turn.L, Turn.F, Turn.F, Turn.D, Turn.F, Turn.D_, Turn.F, Turn.L_, Turn.L_, Turn.B_ };

            private readonly Turn[] edgeSequence = new Turn[] { Turn.R, Turn.B, Turn.B, Turn.D, Turn.D, Turn.F, Turn.L, Turn.F_, Turn.D, Turn.D, Turn.B, Turn.R_, Turn.B };

            public int NumberOfEdgeSteps { get; private set; }

            public int NumberOfCornerSteps { get; private set; }

            public bool HasParity { get; private set; }

            public ClassicPlan(Cube cube) :
                base(cube)
            {
                this.NumberOfEdgeSteps = 0;
                this.NumberOfCornerSteps = 0;
            }

            public void FixCube()
            {
                this.FixCorners();
                this.HasParity = this.NumberOfCornerSteps % 2 == 1;
                if (this.HasParity)
                {
                    this.ApplyEdgeSequence(OSticker.UB);
                    this.NumberOfEdgeSteps--;
                }
                this.FixEdges();
                if (this.HasParity)
                {
//                    this.ApplyCornerSequence(OSticker.FRU);
                }
            }


            public void FixEdges()
            {
                Sticker? cycleHead = null;
                var stickersLeft = new List<Sticker>(StickerExtensionMethods.AllEdgeStickers);

                OSticker? next = null;
                bool atCycleHead = true;

                do
                {
                    if (next.HasValue)
                    {
                        if (cycleHead.HasValue || next.Value.Sticker() != Sticker.eUR)
                        {
                            ApplyEdgeSequence(next.Value);
                        }

                        if (
                            (!cycleHead.HasValue && next.Value.Sticker() == Sticker.eUR) ||
                            (cycleHead.HasValue && !atCycleHead && next.Value.Sticker() == cycleHead.Value))
                        {
                            if (stickersLeft.Count() == 0)
                            {
                                break;
                            }

                            // Find new path start
                            cycleHead = stickersLeft.First();
                            atCycleHead = true;
                            next = (OSticker)(((int)cycleHead.Value - (int)Sticker.eUF) * 2 + (int)OSticker.UF);
                            continue;
                        }
                    }   
                    else
                    {
                        next = OSticker.UR;
                    }

                    atCycleHead = false;

                    stickersLeft.Remove(next.Value.Sticker());

                    var nextCubie = this.Cube[next.Value.Sticker()];
                    next = nextCubie.Oriented(next.Value);
                }
                while (true);
            }

            public void FixCorners()
            {
                Sticker? cycleHead = null;
                var stickersLeft = new List<Sticker>(StickerExtensionMethods.AllCornerStickers);

                OSticker? next = null;
                bool atCycleHead = true;

                do
                {
                    if (next.HasValue)
                    {
                        if (cycleHead.HasValue || next.Value.Sticker() != Sticker.cURB)
                        {
                            ApplyCornerSequence(next.Value);
                        }

                        if (
                            (!cycleHead.HasValue && next.Value.Sticker() == Sticker.cURB) ||
                            (cycleHead.HasValue && !atCycleHead && next.Value.Sticker() == cycleHead.Value))
                        {
                            if (stickersLeft.Count() == 0)
                            {
                                break;
                            }

                            // Find new path start
                            cycleHead = stickersLeft.First();
                            atCycleHead = true;
                            next = (OSticker)((int)cycleHead.Value * 3);
                            continue;
                        }
                    }
                    else
                    {
                        next = OSticker.URB;
                    }

                    atCycleHead = false;

                    stickersLeft.Remove(next.Value.Sticker());

                    var nextCubie = this.Cube[next.Value.Sticker()];
                    next = nextCubie.Oriented(next.Value);
                }
                while (true);
            }

            public void ApplyEdgeSequence(OSticker osticker)
            {
                var leadIn = GetLeadIn(osticker);
                this.NumberOfEdgeSteps++;
                Add(leadIn);
                Add(edgeSequence);
                Add(leadIn.Reverse().Select(s => s.Inverse()).ToArray());
            }

            public void ApplyCornerSequence(OSticker osticker)
            {
                var leadIn = GetLeadIn(osticker);
                this.NumberOfCornerSteps++;
                Add(leadIn);
                Add(cornerSequence);
                Add(leadIn.Reverse().Select(s => s.Inverse()).ToArray());
            }

            public Turn[] GetLeadIn(OSticker otype)
            {
                switch (otype)
                {
                    case OSticker.URB: 
                    case OSticker.RBU:
                    case OSticker.BUR:
                        throw new InvalidOperationException("Cannot create lead in for buffer corner");

                    case OSticker.FUL: return new Turn[] { };
                    case OSticker.ULF: return new Turn[] { Turn.L, Turn.F };
                    case OSticker.LFU: return new Turn[] { Turn.F_, Turn.L_ };

                    case OSticker.FRU: return new Turn[] { Turn.F_ };
                    case OSticker.RUF: return new Turn[] { Turn.F_, Turn.L, Turn.F };
                    case OSticker.UFR: return new Turn[] { Turn.F_, Turn.F_, Turn.L_ };

                    case OSticker.UBL: return new Turn[] { Turn.L };
                    case OSticker.BLU: return new Turn[] { Turn.L, Turn.L, Turn.F };
                    case OSticker.LUB: return new Turn[] { Turn.L, Turn.F_, Turn.L_ };

                    case OSticker.DFL: return new Turn[] { Turn.L_ };
                    case OSticker.FLD: return new Turn[] { Turn.F };
                    case OSticker.LDF: return new Turn[] { Turn.L_, Turn.F_, Turn.L_ };

                    case OSticker.FDR: return new Turn[] { Turn.F, Turn.F };
                    case OSticker.DRF: return new Turn[] { Turn.F, Turn.F, Turn.L, Turn.F };
                    case OSticker.RFD: return new Turn[] { Turn.F, Turn.L_ };

                    case OSticker.BDL: return new Turn[] { Turn.L, Turn.L };
                    case OSticker.DLB: return new Turn[] { Turn.L_, Turn.F };
                    case OSticker.LBD: return new Turn[] { Turn.L, Turn.L, Turn.F_, Turn.L_ };

                    case OSticker.DBR: return new Turn[] { Turn.D, Turn.D, Turn.L_ };
                    case OSticker.BRD: return new Turn[] { Turn.D, Turn.D, Turn.F };
                    case OSticker.RDB: return new Turn[] { Turn.D, Turn.D, Turn.L_, Turn.F_, Turn.L_ };

                    case OSticker.UR: return new Turn[] { };
                    case OSticker.RU: return new Turn[] { };
                        throw new InvalidOperationException("Cannot create lead in for buffer edge");

                    case OSticker.UF: return new Turn[] { Turn.M, Turn.M, Turn.D, Turn.L, Turn.L };
                    case OSticker.FU: return new Turn[] { Turn.M ,Turn.D_, Turn.L, Turn.L };
                    case OSticker.UL: return new Turn[] { };
                    case OSticker.LU: return new Turn[] { Turn.L, Turn.E_, Turn.L };
                    case OSticker.UB: return new Turn[] { Turn.M_, Turn.M_, Turn.D_, Turn.L, Turn.L };
                    case OSticker.BU: return new Turn[] { Turn.M_, Turn.D, Turn.L, Turn.L };
                    case OSticker.LF: return new Turn[] { Turn.E_, Turn.L  };
                    case OSticker.FL: return new Turn[] { Turn.L_ };
                    case OSticker.LB: return new Turn[] { Turn.E, Turn.L_ };
                    case OSticker.BL: return new Turn[] { Turn.L };
                    case OSticker.RF: return new Turn[] { Turn.E_, Turn. L_ };
                    case OSticker.FR: return new Turn[] { Turn.E_, Turn.E_, Turn.L };
                    case OSticker.RB: return new Turn[] { Turn.E, Turn. L };
                    case OSticker.BR: return new Turn[] { Turn.E, Turn.E, Turn.L_ };
                    case OSticker.DF: return new Turn[] { Turn.D_, Turn.L, Turn.L };
                    case OSticker.FD: return new Turn[] { Turn.M, Turn.D, Turn.L, Turn.L };
                    case OSticker.DL: return new Turn[] { Turn.L, Turn.L };
                    case OSticker.LD: return new Turn[] { Turn.L_, Turn.E_, Turn.L };
                    case OSticker.DR: return new Turn[] { Turn.D, Turn.D, Turn.L, Turn.L };
                    case OSticker.RD: return new Turn[] { Turn.D, Turn.D, Turn.L_, Turn.E_, Turn.L };
                    case OSticker.DB: return new Turn[] { Turn.D, Turn.L, Turn.L };
                    case OSticker.BD: return new Turn[] { Turn.M_, Turn.D_, Turn.L, Turn.L };

                    default:
                        throw new NotImplementedException(nameof(GetLeadIn));
                }
            }
        }
    }
}
