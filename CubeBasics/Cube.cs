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

        public OSticker this[OSticker index]
        {
            get
            {
                return this[index.Sticker()].Oriented(index);
            }
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
                this.Cubies[key] = new Cubie(key);
            }
        }

        public Cube Clone()
        {
            return new Cube()
            {
                Cubies = this.Cubies.ToDictionary(pair => pair.Key, pair => pair.Value.Clone())
            };
        }

        public Turn[] Scramble(int turns, int? seed = null)
        {
            var turnList = new List<Turn>();
            Reset();
            if (!seed.HasValue)
            {
                seed = DateTime.Now.Millisecond;
            }
            var rand = new Random(seed.Value);
            var possibleTurns = Enum.GetValues(typeof(Turn));
            while (turns-- > 0)
            {
                turnList.Add((Turn)possibleTurns.GetValue(rand.Next(possibleTurns.Length)));
            }

            return turnList.ToArray();
        }

        public bool IsSolved()
        {
            return this.Cubies.Values.All(cubie => cubie.IsCorrect);
        }

        public bool AreCornersSolved()
        {
            return this.Cubies.Values.Where(cubie => cubie.IsCorner).All(cubie => cubie.IsCorrect);
        }

        public bool AreEdgesSolved()
        {
            return this.Cubies.Values.Where(cubie => cubie.IsEdge).All(cubie => cubie.IsCorrect);
        }

        public void Apply(IContainsMoves plan)
        {
            Apply(plan.GetTurns());
        }

        public void Apply(IEnumerable<Turn> turns)
        {
            foreach (var turn in turns)
            {
                ApplyTurn(turn);
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
                        this[moves[i]] = clone[moves[(i + 1) % moves.Length]].Rot(axis, inverse);
                    }
                }
            }
        }

        public Cube L(bool inverse = false)
        {
            Do(Axis.X, !inverse,
                new Sticker[2][] {
                    new Sticker[] { cDLB, cDFL, cULF, cUBL },
                    new Sticker[] { eLB, eDL, eLF, eUL }
                });
            return this;
        }
        public Cube L_() { return L(true); }

        public Cube R(bool inverse = false)
        {
            Do(Axis.X, inverse,
                new Sticker[2][] {
                    new Sticker[] { cUFR, cURB, cDBR, cDRF},
                    new Sticker[] { eUR, eRB, eDR, eRF }
                });
            return this;
        }
        public Cube R_() { return R(true); }

        public Cube U(bool inverse = false)
        {
            Do(Axis.Y, inverse,
                new Sticker[2][] {
                    new Sticker[] { cULF, cUBL, cURB, cUFR },
                    new Sticker[] { eUL, eUB, eUR, eUF }
                });
            return this;
        }
        public Cube U_() { return U(true); }

        public Cube D(bool inverse = false)
        {
            Do(Axis.Y, !inverse,
                new Sticker[2][] {
                    new Sticker[] { cDFL, cDLB, cDBR, cDRF },
                    new Sticker[] { eDL, eDB, eDR, eDF }
                });
            return this;
        }
        public Cube D_() { return D(true); }

        public Cube B(bool inverse = false)
        {
            Do(Axis.Z, inverse,
                new Sticker[2][] {
                    new Sticker[] { cUBL, cDLB, cDBR, cURB },
                    new Sticker[] { eLB, eDB, eRB, eUB }
                });
            return this;
        }
        public Cube B_() { return B(true); }

        public Cube F(bool inverse = false)
        {
            Do(Axis.Z, !inverse,
                new Sticker[2][] {
                    new Sticker[] { cULF, cDFL, cDRF, cUFR },
                    new Sticker[] { eUF, eLF, eDF, eRF }
                });
            return this;
        }
        public Cube F_() { return F(true); }
    }
}
