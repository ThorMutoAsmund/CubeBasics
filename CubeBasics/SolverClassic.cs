using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

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
            this.Plan.Create();
            this.Cube.Apply(this.Plan);
        }

        public void DescribePlan()
        {
            this.Plan.Describe();
        }


        public class ClassicPlan : BasePlan
        {
            private readonly Turn[] cornerSequence = new Turn[] { Turn.U, Turn.B, Turn.L, Turn.L, Turn.F, Turn.F, Turn.D, Turn.F, Turn.D_, Turn.F, Turn.L_, Turn.L_, Turn.B_ };

            public ClassicPlan(Cube cube) :
                base(cube)
            {
            }

            public void TestLeadIn()
            {
                Add(GetLeadIn(OSticker.BLU));
                Add(cornerSequence);
                Add(GetLeadIn(OSticker.BLU).Reverse().Select(s => s.Inverse()).ToArray());
            }

            public void Create()
            {
                FixCorners();
                FixEdges();
            }

            public void FixEdges()
            {
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
                            var leadIn = GetLeadIn(next.Value);
                            Add(leadIn);
                            Add(cornerSequence);
                            Add(leadIn.Reverse().Select(s => s.Inverse()).ToArray());
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

                    case OSticker.UF: return new Turn[] { };
                    case OSticker.FU: return new Turn[] { };
                    case OSticker.UL: return new Turn[] { };
                    case OSticker.LU: return new Turn[] { };
                    case OSticker.UB: return new Turn[] { };
                    case OSticker.BU: return new Turn[] { };
                    case OSticker.LF: return new Turn[] { };
                    case OSticker.FL: return new Turn[] { };
                    case OSticker.LB: return new Turn[] { };
                    case OSticker.BL: return new Turn[] { };
                    case OSticker.RF: return new Turn[] { };
                    case OSticker.FR: return new Turn[] { };
                    case OSticker.RB: return new Turn[] { };
                    case OSticker.BR: return new Turn[] { };
                    case OSticker.DF: return new Turn[] { };
                    case OSticker.FD: return new Turn[] { };
                    case OSticker.DL: return new Turn[] { };
                    case OSticker.LD: return new Turn[] { };
                    case OSticker.DR: return new Turn[] { };
                    case OSticker.RD: return new Turn[] { };
                    case OSticker.DB: return new Turn[] { };
                    case OSticker.BD: return new Turn[] { };


                    default:
                        throw new NotImplementedException(nameof(GetLeadIn));
                }
            }
        }
    }
}
