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
            private List<Turn> Turns { get; set; }
            
            public Plan(Cube cube)
            {
                this.Cube = cube;
                this.Turns = new List<Turn>();
            }

            public IEnumerable<Turn> GetTurns()
            {
                return this.Turns;
            }

            public void Describe()
            {
                Console.WriteLine("PLAN with {0} turns{1}", this.Turns.Count(), this.Turns.Count() % 2 == 1 ? " (parity)" : String.Empty);
            }

            private Plan Add(Turn turn)
            {
                this.Turns.Add(turn);
                return this;
            }

            private Plan L() { return this.Add(Turn.L); }
            private Plan L_() { return this.Add(Turn.L_); }
            private Plan R() { return this.Add(Turn.R); }
            private Plan R_() { return this.Add(Turn.R_); }
            private Plan U() { return this.Add(Turn.U); }
            private Plan U_() { return this.Add(Turn.U_); }
            private Plan D() { return this.Add(Turn.D); }
            private Plan D_() { return this.Add(Turn.D_); }
            private Plan F() { return this.Add(Turn.F); }
            private Plan F_() { return this.Add(Turn.F_); }
            private Plan B() { return this.Add(Turn.B); }
            private Plan B_() { return this.Add(Turn.B_); }

            public void Create()
            {
                var startNewPathSticker = Sticker.cURB;
                var okStickers = new List<Sticker>();

                var location = Sticker.cURB;
                var olocation = OSticker.URB;
                do
                {
                    var next = this.Cube[location];
                    if (next.Type == startNewPathSticker)
                    {
                        // Find new path start
                        // StickerExtensionMethods.AllCornerStickers.Whe
                    }
                    olocation = next.Oriented(olocation);
                    AddCornerMoves(next, olocation);
                    okStickers.Add(next.Type);
                    location = next.Type;
                }
                while (true);
            }

            private Plan AddCornerMoves(Cubie cubie, OSticker otype)
            {
                switch (otype)
                {
                    case OSticker.URB: 
                    case OSticker.RBU:
                    case OSticker.BUR:
                        throw new InvalidOperationException("Cannot rotate buffer corner");

                    case OSticker.FUL: return this;
                    case OSticker.ULF: return this.L().F();
                    case OSticker.LFU: return this.F_().L_();

                    case OSticker.FRU: return this.F_();
                    case OSticker.RUF: return this.F_().L().F();
                    case OSticker.UFR: return this.F_().F_().L_();

                    case OSticker.UBL: return this.L();
                    case OSticker.BLU: return this.L().L().F();
                    case OSticker.LUB: return this.L().F_().L_();

                    case OSticker.DFL: return this.L_();
                    case OSticker.FLD: return this.F();
                    case OSticker.LDF: return this.L_().F_().L_();

                    case OSticker.FDR: return this.F().F();
                    case OSticker.DRF: return this.F().F().L().F();
                    case OSticker.RFD: return this.F().L_();

                    case OSticker.BDL: return this.L().L();
                    case OSticker.DLB: return this.L_().F();
                    case OSticker.LBD: return this.L().L().F_().L_();

                    case OSticker.DBR: return this.D().D().L_();
                    case OSticker.BRD: return this.D().D().F();
                    case OSticker.RDB: return this.D().D().L().F_().L_();

                    default:
                        throw new NotImplementedException(nameof(AddCornerMoves));
                }
            }
        }
    }
}
