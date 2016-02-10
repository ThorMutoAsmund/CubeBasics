using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public static class Cycle3System
    {
        public static void Fix(Cube cube, Sticker[] allStickers, Sticker bufferPosition, Action<OSticker> addStickerSequence)
        {
            Sticker? cycleHead = null;
            var stickersLeft = new List<Sticker>(allStickers);

            OSticker? next = null;
            bool atCycleHead = true;

            do
            {
                if (next.HasValue)
                {
                    if (cycleHead.HasValue || next.Value.Sticker() != bufferPosition)
                    {
                        addStickerSequence(next.Value);
                    }

                    if (
                        (!cycleHead.HasValue && next.Value.Sticker() == bufferPosition) ||
                        (cycleHead.HasValue && !atCycleHead && next.Value.Sticker() == cycleHead.Value))
                    {
                        if (stickersLeft.Count() == 0)
                        {
                            break;
                        }

                        // Find new path start
                        cycleHead = null;
                        do
                        {
                            if (cycleHead.HasValue)
                            {
                                stickersLeft.Remove(next.Value.Sticker());
                                if (stickersLeft.Count() == 0)
                                {
                                    break;
                                }
                            }
                            cycleHead = stickersLeft.First();
                            atCycleHead = true;
                            next = cycleHead.Value.PrimarySticker();
                        }
                        while (cube[next.Value.Sticker()].Type == cycleHead && cube[next.Value.Sticker()].IsCorrect);

                        if (stickersLeft.Count() == 0)
                        {
                            break;
                        }

                        continue;
                    }
                }
                else
                {
                    next = bufferPosition.PrimarySticker();
                }

                atCycleHead = false;

                stickersLeft.Remove(next.Value.Sticker());

                next = cube[next.Value];
                //var nextCubie = cube[next.Value.Sticker()];
                //next = nextCubie.Oriented(next.Value);
            }
            while (true);
        }
    }
}
