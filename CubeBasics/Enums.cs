using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public enum Sticker {
        ULF, UFR, UBL, URB, DFL, DRF, DLB, DBR,
        UF, UL, UR, UB, LF, LB, RF, RB, DF, DL, DR, DB
    }
    public enum OSticker {
        ULF, LFU, FUL, UFR, FRU, RUF, UBL, BLU, LUB, URB, RBU, BUR, DFL, FLD, LDF, DRF, RFD, FDR, DLB, LBD, BDL, DBR, BRD, RDB,
        UF, FU, UL, LU, UR, RU, UB, BU, LF, FL, LB, BL, RF, FR, RB, BR, DF, FD, DL, LD, DR, RD, DB, BD
    }

    public enum Turn
    {
        U, U_, F, F_, L, L_, R, R_, B, B_, D, D_
    }

    public enum Axis
    {
        X,Y,Z
    }

}
