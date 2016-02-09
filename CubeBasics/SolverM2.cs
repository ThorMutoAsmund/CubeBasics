using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CubeBasics
{
    public class SolverM2 : BaseSolver
    {
        private readonly Turn[] cornerSequence = new Turn[] { Turn.U, Turn.B, Turn.L, Turn.L, Turn.F, Turn.F, Turn.D, Turn.F, Turn.D_, Turn.F, Turn.L_, Turn.L_, Turn.B_ };

        private readonly Turn[] edgeSequence = new Turn[] { Turn.R, Turn.B, Turn.B, Turn.D, Turn.D, Turn.F, Turn.L, Turn.F_, Turn.D, Turn.D, Turn.B, Turn.R_, Turn.B };

        public SolverM2(Cube cube) :
                            base(cube)
        {
        }

        public bool HasParity { get; private set; }

        public override int TotalStepsWithoutParity
        {
            get
            {
                return this.NumberOfEdgeSteps + this.NumberOfCornerSteps -
                    (this.HasParity ? 3 : 0);
            }
        }

        public override void Solve()
        {
            this.Clear();

            Fix(StickerExtensionMethods.AllEdgeStickers, Sticker.eUR);

            this.HasParity = this.NumberOfEdgeSteps % 2 == 1;

            if (this.HasParity)
            {
                this.ApplySequence(OSticker.UL);
            }

            Fix(StickerExtensionMethods.AllCornerStickers, Sticker.cURB);

            if (this.HasParity)
            {
                this.ApplySequence(OSticker.UB);
                this.ApplySequence(OSticker.UL);
            }

            this.Cube.Apply(this);
        }

        public void SolveCorners()
        {
            Fix(StickerExtensionMethods.AllCornerStickers, Sticker.cURB);

            this.Cube.Apply(this);
        }

        public void SolveEdges()
        {
            Fix(StickerExtensionMethods.AllEdgeStickers, Sticker.eUR);

            this.Cube.Apply(this);
        }

        private void ApplySequence(OSticker osticker)
        {
            if (osticker.Sticker().IsEdge())
            {
                var leadIn = ClassicLeadIns.GetLeadIn(osticker);
                this.NumberOfEdgeSteps++;
                Add(leadIn);
                Add(edgeSequence);
                Add(leadIn.Reverse().Select(s => s.Inverse()).ToArray());
            }
            else
            {
                var leadIn = ClassicLeadIns.GetLeadIn(osticker);
                this.NumberOfCornerSteps++;
                Add(leadIn);
                Add(cornerSequence);
                Add(leadIn.Reverse().Select(s => s.Inverse()).ToArray());
            }
        }
        private void Fix(Sticker[] allStickers, Sticker bufferPosition)
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
                        ApplySequence(next.Value);
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
                        while (this.Cube[next.Value.Sticker()].Type == cycleHead);

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

                var nextCubie = this.Cube[next.Value.Sticker()];
                next = nextCubie.Oriented(next.Value);
            }
            while (true);
        }
    }
}
