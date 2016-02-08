using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CubeBasics.Sticker;

namespace CubeBasics
{
    public static class TurnExtensionMethods
    {
        public static Turn Inverse(this Turn turn)
        {
            return (Turn)(((int)turn/2)*2 + 1 -((int)turn % 2));
        }
    }
}
