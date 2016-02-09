using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CubeBasics.Turn;

namespace CubeBasics
{
    public static class TurnExtensionMethods
    {
        public static Turn[] basicTurns = new Turn[] { U, U_, F, F_, L, L_, R, R_, B, B_, D, D_ };

        public static Turn Inverse(this Turn turn)
        {
            return (Turn)(((int)turn/2)*2 + 1 -((int)turn % 2));
        }

        public static Turn[] Inverse(this Turn[] turns)
        {
            return turns.Reverse().Select(s => s.Inverse()).ToArray();
        }

        public static Turn[] BasicTurns
        {
            get
            {
                return basicTurns;
            }
        }
    }
}
