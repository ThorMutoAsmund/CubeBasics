using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CubeBasics.OSticker;

namespace CubeBasics
{
    public static class OStickerExtensionMethods
    {
        public static Sticker Sticker(this OSticker osticker)
        {
            return osticker < UF ? 
                (Sticker)(((int)osticker) / 3) :
                (Sticker)((((int)osticker) - (int)UF) / 2 + (int)CubeBasics.Sticker.sUF);
        }

        public static string Translate(this OSticker osticker)
        {
            switch (osticker)
            {
                case ULF: return "E";
                case LFU: return "To";
                case FUL: return "Se";
                case UFR: return "Ko";
                case FRU: return "Su";
                case RUF: return "Ne";
                case UBL: return "Ka";
                case BLU: return "A";
                case LUB: return "Te";
                case URB: return "Ku";
                case RBU: return "Na";
                case BUR: return "He";
                case DFL: return "Mo";
                case FLD: return "O";
                case LDF: return "Ti";
                case DRF: return "Mu";
                case RFD: return "No";
                case FDR: return "Si";
                case DLB: return "I";
                case LBD: return "Ta";
                case BDL: return "Hi";
                case DBR: return "Ma";
                case BRD: return "Hu";
                case RDB: return "Ni";
                case UF: return "ko";
                case FU: return "se";
                case UL: return "e";
                case LU: return "te";
                case UR: return "ku";
                case RU: return "ne";
                case UB: return "ka";
                case BU: return "he";
                case LF: return "to";
                case FL: return "o";
                case LB: return "ta";
                case BL: return "a";
                case RF: return "no";
                case FR: return "su";
                case RB: return "na";
                case BR: return "hu";
                case DF: return "mo";
                case FD: return "si";
                case DL: return "i";
                case LD: return "ti";
                case DR: return "mu";
                case RD: return "ni";
                case DB: return "ma";
                case BD: return "hi";
            }

            return osticker.ToString();
        }
    }
}
