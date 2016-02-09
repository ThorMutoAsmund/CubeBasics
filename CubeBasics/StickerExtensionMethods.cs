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
        private static Sticker[] cornerStickers = new Sticker[] { cULF, cUFR, cUBL, cURB, cDFL, cDRF, cDLB, cDBR };
        private static Sticker[] edgeStickers = new Sticker[] { eUF, eUL, eUR, eUB, eLF, eLB, eRF, eRB, eDF, eDL, eDR, eDB };

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
            if (sticker < Sticker.eUF)
            {
                return (OSticker)((int)sticker * 3);
            }
            else
            {
                return (OSticker)(((int)sticker - (int)Sticker.eUF) * 2 + (int)OSticker.UF);
            }
        }
    }
}
