using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public static class ClassicSequences
    {
        private static Turn[] cornerSequence = new Turn[] { Turn.U, Turn.B, Turn.L, Turn.L, Turn.F, Turn.F, Turn.D, Turn.F, Turn.D_, Turn.F, Turn.L_, Turn.L_, Turn.B_ };

        private static Turn[] edgeSequence = new Turn[] { Turn.R, Turn.B, Turn.B, Turn.D, Turn.D, Turn.F, Turn.L, Turn.F_, Turn.D, Turn.D, Turn.B, Turn.R_, Turn.B };

        public static Turn[] CornerSequence
        {
            get
            {
                return cornerSequence;
            }
        }

        public static Turn[] EdgeSequence
        {
            get
            {
                return edgeSequence;
            }
        }

        public static Turn[] GetLeadIn(OSticker otype)
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

                case OSticker.UR:
                case OSticker.RU:
                    throw new InvalidOperationException("Cannot create lead in for buffer edge");

                case OSticker.UF: return new Turn[] { Turn.M, Turn.M, Turn.D, Turn.L, Turn.L };
                case OSticker.FU: return new Turn[] { Turn.M, Turn.D_, Turn.L, Turn.L };
                case OSticker.UL: return new Turn[] { };
                case OSticker.LU: return new Turn[] { Turn.L, Turn.E_, Turn.L };
                case OSticker.UB: return new Turn[] { Turn.M_, Turn.M_, Turn.D_, Turn.L, Turn.L };
                case OSticker.BU: return new Turn[] { Turn.M_, Turn.D, Turn.L, Turn.L };
                case OSticker.LF: return new Turn[] { Turn.E_, Turn.L };
                case OSticker.FL: return new Turn[] { Turn.L_ };
                case OSticker.LB: return new Turn[] { Turn.E, Turn.L_ };
                case OSticker.BL: return new Turn[] { Turn.L };
                case OSticker.RF: return new Turn[] { Turn.E_, Turn.L_ };
                case OSticker.FR: return new Turn[] { Turn.E_, Turn.E_, Turn.L };
                case OSticker.RB: return new Turn[] { Turn.E, Turn.L };
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
