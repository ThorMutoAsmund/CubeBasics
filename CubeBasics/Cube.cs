using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static CubeBasics.Sticker;

namespace CubeBasics
{ 
    public class Cube
    {
        public Dictionary<Sticker, Cubie> Cubies { get; private set; }

        public Cube()
        {
            this.Cubies = new Dictionary<Sticker, Cubie>();
            Reset();
        }

        public Cubie this[Sticker index]
        {
            get
            {
                return this.Cubies[index];
            }
            set
            {
                this.Cubies[index] = value;
            }
        }

        public void Reset()
        {
            foreach (Sticker key in Enum.GetValues(typeof(Sticker)))
            {
                this.Cubies[key] = new Cubie(key, 0);
            }
        }

        public Cube Clone()
        {
            return new Cube()
            {
                Cubies = this.Cubies.ToDictionary(pair => pair.Key, pair => pair.Value.Clone())
            };
        }

        public void Scramble(int turns)
        {
            var rand = new Random();
            var possibleTurns = Enum.GetValues(typeof(Turn));
            while (turns-- > 0)
            {
                this.ApplyTurn((Turn)possibleTurns.GetValue(rand.Next(possibleTurns.Length)));
            }
        }

        public bool IsSolved()
        {
            return this.Cubies.Values.All(cubie => cubie.Orientation == 0);
        }

        public bool AreCornersSolved()
        {
            return this.Cubies.Values.Where(cubie => cubie.IsCorner).All(cubie => cubie.Orientation == 0);
        }

        public bool AreEdgesSolved()
        {
            return this.Cubies.Values.Where(cubie => cubie.IsEdge).All(cubie => cubie.Orientation == 0);
        }

        public void Apply(IContainsMoves plan)
        {
            foreach (var move in plan.GetMoves())
            {
                ApplyTurn(move);
            }
        }

        public void ApplyTurn(Turn move)
        {
            switch (move)
            {
                case Turn.U: U(); break;
                case Turn.U_: U_(); break;
                case Turn.F: F(); break;
                case Turn.F_: F_(); break;
                case Turn.L: L(); break;
                case Turn.L_: L_(); break;
                case Turn.R: R(); break;
                case Turn.R_: R_(); break;
                case Turn.B: B(); break;
                case Turn.B_: B_(); break;
                case Turn.D: D(); break;
                case Turn.D_: D_(); break;
            }
        }

        private void Do(Axis axis, bool inverse, Sticker[][] allMoves)
        {
            var clone = Clone();
            foreach (var moves in allMoves)
            {
                if (!inverse)
                {
                    for (int i = 0; i < moves.Length; ++i)
                    {
                        this[moves[(i + 1) % moves.Length]] = clone[moves[i]].Rot(axis, inverse);
                    }
                }
                else
                {
                    for (int i = moves.Length-1; i >= 0; --i)
                    {
                        this[moves[(i + 1) % moves.Length]] = clone[moves[i]].Rot(axis, inverse);
                    }
                }
            }
        }

        public Cube L(bool inverse = false)
        {
            Do(Axis.X, !inverse,
                new Sticker[2][] {
                    new Sticker[] { UBL, ULF, DFL, DLB },
                    new Sticker[] { UL, LF, DL, LB }
                });
            return this;
        }
        public Cube L_() { return L(true); }

        public Cube R(bool inverse = false)
        {
            Do(Axis.X, inverse,
                new Sticker[2][] {
                    new Sticker[] { UFR, URB, DBR, DRF},
                    new Sticker[] { UR, RB, DR, RF }
                });
            return this;
        }
        public Cube R_() { return R(true); }

        public Cube U(bool inverse = false)
        {
            Do(Axis.Y, inverse,
                new Sticker[2][] {
                    new Sticker[] { ULF, UBL, URB, UFR },
                    new Sticker[] { UL, UB, UR, UF }
                });
            return this;
        }
        public Cube U_() { return U(true); }

        public Cube D(bool inverse = false)
        {
            Do(Axis.Y, !inverse,
                new Sticker[2][] {
                    new Sticker[] { DFL, DRF, DBR, DLB },
                    new Sticker[] { DL, DF, DR, DB }
                });
            return this;
        }
        public Cube D_() { return D(true); }

        public Cube B(bool inverse = false)
        {
            Do(Axis.Z, inverse,
                new Sticker[2][] {
                    new Sticker[] { UBL, DLB, DBR, URB },
                    new Sticker[] { LB, DB, RB, UB }
                });
            return this;
        }
        public Cube B_() { return B(true); }

        public Cube F(bool inverse = false)
        {
            Do(Axis.Z, !inverse,
                new Sticker[2][] {
                    new Sticker[] { ULF, UFR, DRF, DFL },
                    new Sticker[] { UF, RF, DF, LF }
                });
            return this;
        }
        public Cube F_() { return F(true); }
    }
}
