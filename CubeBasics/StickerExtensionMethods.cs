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
        private static Sticker[] cornerStickers = new Sticker[] { sULF, sUFR, sUBL, sURB, sDFL, sDRF, sDLB, sDBR };
        private static Sticker[] edgeStickers = new Sticker[] { sUF, sUL, sUR, sUB, sLF, sLB, sRF, sRB, sDF, sDL, sDR, sDB };

        public static Sticker[] AllCornerStickers
        {
            get
            {
                return cornerStickers;
            }
        }

        public static Sticker[] AllEdgeStickers
        {
            get
            {
                return edgeStickers;
            }
        }

        public static bool IsCorner(this Sticker sticker)
        {
            return cornerStickers.Contains(sticker);
        }

        public static bool IsEdge(this Sticker sticker)
        {
            return edgeStickers.Contains(sticker);
        }

        public static OSticker PrimarySticker(this Sticker sticker)
        {
            if (sticker < Sticker.sUF)
            {
                return (OSticker)((int)sticker * 3);
            }
            else
            {
                return (OSticker)(((int)sticker - (int)Sticker.sUF) * 2 + (int)OSticker.UF);
            }
        }
    }
}
