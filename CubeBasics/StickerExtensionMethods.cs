using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CubeBasics.Sticker;

namespace CubeBasics
{
    public static class StickerExtensionMethods
    {
        public static Sticker[] CornerStickers = new Sticker[] { ULF, UFR, UBL, URB, DFL, DRF, DLB, DBR };
        public static Sticker[] EdgeStickers = new Sticker[] { UF, UL, UR, UB, LF, LB, RF, RB, DF, DL, DR, DB };

        public static Sticker[] AllCornerStickers
        {
            get
            {
                return CornerStickers;
            }
        }

        public static Sticker[] AllEdgeStickers
        {
            get
            {
                return EdgeStickers;
            }
        }

        public Sticker[] AllCornerStickers()
        {
            return CornerStickers;
        }

        public static bool IsCorner(this Sticker sticker)
        {
            return CornerStickers.Contains(sticker);
        }

        public static bool IsEdge(this Sticker sticker)
        {
            return EdgeStickers.Contains(sticker);
        }
    }
}
