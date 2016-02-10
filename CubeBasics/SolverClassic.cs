using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CubeBasics
{
    public class SolverClassic : BaseSolver
    {
        public SolverClassic(Cube cube) :
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

            Cycle3System.Fix(this.Cube, StickerExtensionMethods.AllEdgeStickers, Sticker.sUR, this.AddStickerSequence);

            this.HasParity = this.NumberOfEdgeSteps % 2 == 1;

            if (this.HasParity)
            {
                this.AddStickerSequence(OSticker.UL);
            }

            Cycle3System.Fix(this.Cube, StickerExtensionMethods.AllCornerStickers, Sticker.sURB, this.AddStickerSequence);

            if (this.HasParity)
            {
                this.AddStickerSequence(OSticker.UB);
                this.AddStickerSequence(OSticker.UL);
            }

            this.Cube.Apply(this);
        }

        public void SolveEdges()
        {
            Cycle3System.Fix(this.Cube, StickerExtensionMethods.AllEdgeStickers, Sticker.sUR, this.AddStickerSequence);

            this.Cube.Apply(this);
        }

        public void SolveCorners()
        {
            Cycle3System.Fix(this.Cube, StickerExtensionMethods.AllCornerStickers, Sticker.sURB, this.AddStickerSequence);

            this.Cube.Apply(this);
        }

        private void AddStickerSequence(OSticker osticker)
        {
            if (osticker.Sticker().IsEdge())
            {
                var leadIn = ClassicSequences.GetLeadIn(osticker);
                this.NumberOfEdgeSteps++;
                Add(leadIn);
                Add(ClassicSequences.EdgeSequence);
                Add(leadIn.Inverse());
            }
            else
            {
                var leadIn = ClassicSequences.GetLeadIn(osticker);
                this.NumberOfCornerSteps++;
                Add(leadIn);
                Add(ClassicSequences.CornerSequence);
                Add(leadIn.Inverse());
            }
        }
    }
}
