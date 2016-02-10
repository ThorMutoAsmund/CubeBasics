using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public enum Sticker {
        sULF, sUFR, sUBL, sURB, sDFL, sDRF, sDLB, sDBR,
        sUF, sUL, sUR, sUB, sLF, sLB, sRF, sRB, sDF, sDL, sDR, sDB,
        sU, sF, sL, sR, sB, sD
    }
    public enum OSticker {
        ULF, LFU, FUL, UFR, FRU, RUF, UBL, BLU, LUB, URB, RBU, BUR, DFL, FLD, LDF, DRF, RFD, FDR, DLB, LBD, BDL, DBR, BRD, RDB,
        UF, FU, UL, LU, UR, RU, UB, BU, LF, FL, LB, BL, RF, FR, RB, BR, DF, FD, DL, LD, DR, RD, DB, BD
    }

    public enum Turn
    {
        U, U_, F, F_, L, L_, R, R_, B, B_, D, D_,
        M, M_, E, E_, S, S_,
    }

    public enum Axis
    {
        X,Y,Z
    }

}
