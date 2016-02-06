using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CubeBasics.Sticker;

namespace CubeBasics
{
    public static class OStickerExtensionMethods
    {
        public static Sticker Sticker(this OSticker osticker)
        {
            return (Sticker)(((int)osticker) / 3);
        }
    }
}
