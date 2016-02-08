using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class SolverM2
    {
        public Cube Cube { get; set; }
        private ClassicPlan Plan { get; set; }

        public SolverM2(Cube cube)
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
                var startNewPathSticker = Sticker.cURB;
                var stickersLeft = new List<Sticker>(StickerExtensionMethods.AllCornerStickers);

                var current = OSticker.URB;
                OSticker next;
                bool firstCycle = true;
                bool restart = false;
                stickersLeft.Remove(current.Sticker());
                do
                {
                    //if (stickersLeft.Count() == 0) throw new InvalidOperationException("0 stickers left but not at end of cycle");


                    var nextCubie = this.Cube[current.Sticker()];
                    if (firstCycle == true && nextCubie.Type == startNewPathSticker)
                    {
                        firstCycle = false;
                        if (stickersLeft.Count() == 0)
                        {
                            break;
                        }

                        // Find new path start
                        startNewPathSticker = stickersLeft.First();
                        next = (OSticker)((int)startNewPathSticker * 3);
                        restart = true;
                    }
                    else
                    {
                        next = nextCubie.Oriented(current);
                    }

                    stickersLeft.Remove(next.Sticker()); // Done one time too much

                    var leadIn = GetLeadIn(next);
                    Add(leadIn);
                    Add(cornerSequence);
                    Add(leadIn.Reverse().Select(s => s.Inverse()).ToArray());
                    current = next;

                    if (restart)
                    {
                        restart = false;
                        continue;
                    }

                    if (firstCycle == false && current.Sticker() == startNewPathSticker)
                    {
                        if (stickersLeft.Count() == 0)
                        {
                            break;
                        }

                        // Find new path start
                        startNewPathSticker = stickersLeft.First();
                        next = (OSticker)((int)startNewPathSticker * 3);

                        stickersLeft.Remove(next.Sticker());

                        leadIn = GetLeadIn(next);
                        Add(leadIn);
                        Add(cornerSequence);
                        Add(leadIn.Reverse().Select(s => s.Inverse()).ToArray());
                        current = next;
                    }
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
                        throw new InvalidOperationException("Cannot rotate buffer corner");

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

                    default:
                        throw new NotImplementedException(nameof(GetLeadIn));
                }
            }
        }
    }
}
