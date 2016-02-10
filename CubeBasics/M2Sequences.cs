using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public static class M2Sequences
    {
        private static Turn[] edgeSequence = new Turn[] { Turn.M, Turn.M };

        public static Turn[] EdgeSequence
        {
            get
            {
                return edgeSequence;
            }
        }

        public static Turn[] GetLeadIn(OSticker otype, bool even, out bool isComplete)
        {
            isComplete = false;
            switch (otype)
            {
                case OSticker.DF: return new Turn[] { Turn.D_, Turn.L, Turn.L };
                case OSticker.FD: return new Turn[] { Turn.M, Turn.D, Turn.L, Turn.L };
                    throw new InvalidOperationException("Cannot create lead in for buffer edge");

                case OSticker.UL: return new Turn[] { Turn.L, Turn.U_, Turn.L_, Turn.U };
                case OSticker.LU: return new Turn[] { Turn.B, Turn.L_, Turn.B_ };
                case OSticker.FL: return new Turn[] { Turn.U_, Turn.L_, Turn.U };
                case OSticker.LF: return new Turn[] { Turn.B, Turn.L, Turn.L, Turn.B_ };
                case OSticker.DL: return new Turn[] { Turn.U_, Turn.L, Turn.L, Turn.U };
                case OSticker.LD: return new Turn[] { Turn.B, Turn.L, Turn.B_ };
                case OSticker.BL: return new Turn[] { Turn.U_, Turn.L, Turn.U };
                case OSticker.LB: return new Turn[] { Turn.L_, Turn.B, Turn.L, Turn.B_ };

                case OSticker.UR: return new Turn[] { Turn.R_, Turn.U, Turn.R, Turn.U_ };
                case OSticker.RU: return new Turn[] { Turn.B_, Turn.R, Turn.B };
                case OSticker.FR: return new Turn[] { Turn.U, Turn.R, Turn.U_ };
                case OSticker.RF: return new Turn[] { Turn.B_, Turn.R, Turn.R, Turn.B };
                case OSticker.DR: return new Turn[] { Turn.U, Turn.R, Turn.R, Turn.U_ };
                case OSticker.RD: return new Turn[] { Turn.B_, Turn.R_, Turn.B };
                case OSticker.BR: return new Turn[] { Turn.U, Turn.R_, Turn.U_ };
                case OSticker.RB: return new Turn[] { Turn.R, Turn.B_, Turn.R_, Turn.B };

                case OSticker.UB: return new Turn[] { };
                case OSticker.BU:
                    return new Turn[] { Turn.F_, Turn.D, Turn.R_, Turn.F, Turn.D_  };
                case OSticker.UF:
                case OSticker.DB: 
                    isComplete = true;
                    return (otype == OSticker.UF ? even : !even) ? 
                        new Turn[] { Turn.U, Turn.U, Turn.M_, Turn.U, Turn.U, Turn.M_ } :
                        new Turn[] { Turn.M, Turn.U, Turn.U, Turn.M, Turn.U, Turn.U };
                case OSticker.FU:
                case OSticker.BD:
                    isComplete = true;
                    return (otype == OSticker.FU ? even : !even) ?
                        new Turn[] { Turn.F, Turn.E, Turn.R, Turn.U, Turn.R_, Turn.E_, Turn.R, Turn.U_, Turn.R_, Turn.F_, Turn.M, Turn.M } :
                        new Turn[] { Turn.D, Turn.D, Turn.F, Turn.F, Turn.U, Turn.M_, Turn.U_, Turn.F, Turn.F, Turn.D, Turn.D, Turn.U_, Turn.M_, Turn.U };

                default:
                    throw new NotImplementedException(nameof(GetLeadIn));
            }
        }
    }
}
