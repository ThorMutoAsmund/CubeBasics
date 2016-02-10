using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CubeBasics
{
    public class SolverM2 : BaseSolver
    {
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

        public enum M2SolveMode
        {
            Corners, CornersParity, Edges, CornersParityEdges, Full
        }

        public override void Solve()
        {
            Solve(M2SolveMode.Full);
        }

        public void Solve(M2SolveMode mode)
        {
            this.Clear();

            if (mode != M2SolveMode.Edges)
            {
                Cycle3System.Fix(this.Cube, StickerExtensionMethods.AllCornerStickers, Sticker.sURB, this.AddStickerSequence);
            }

            this.HasParity = this.NumberOfCornerSteps % 2 == 1;

            if (mode != M2SolveMode.Edges && mode != M2SolveMode.Corners)
            {
                if (this.HasParity)
                {
                    Add(ClassicSequences.CornerSequence, "classic corner seq");
                }
            }

            if (mode != M2SolveMode.Corners && mode != M2SolveMode.CornersParity)
            {
                Cycle3System.Fix(this.Cube, StickerExtensionMethods.AllEdgeStickers, Sticker.sDF, this.AddStickerSequence);
            }

            if (mode == M2SolveMode.Full)
            {
                if (this.HasParity)
                {
                    Add(ClassicSequences.CornerSequence, "classic corner seq");
                    Add(M2Sequences.EdgeSequence, "M2 edge seq");
                    AddStickerSequence(OSticker.UR);
                    Add(M2Sequences.EdgeSequence, "M2 edge seq");
                }
            }

            this.Cube.Apply(this);
        }

        private void AddStickerSequence(OSticker osticker)
        {
            if (osticker.Sticker().IsEdge())
            {
                bool isComplete;
                var leadIn = M2Sequences.GetLeadIn(osticker, this.NumberOfEdgeSteps % 2 == 0, out isComplete);
                this.NumberOfEdgeSteps++;
                Add(leadIn, osticker.Translate());
                if (!isComplete)
                {
                    Add(M2Sequences.EdgeSequence);
                    Add(leadIn.Inverse());
                }
            }
            else
            {
                var leadIn = ClassicSequences.GetLeadIn(osticker);
                this.NumberOfCornerSteps++;
                Add(leadIn, osticker.Translate());
                Add(ClassicSequences.CornerSequence);
                Add(leadIn.Inverse());
            }
        }

    }
}
