using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class SolverM2
    {
        public Cube Cube { get; set; }

        public SolverM2(Cube cube)
        {
            this.Cube = cube;
        }

        public void Solve()
        {
            var plan = CreatePlan();
            this.Cube.Apply(plan);
        }

        public Plan CreatePlan()
        {
            var plan = new Plan(this.Cube);
            plan.Create();

            return plan;
        }


        public class Plan : IContainsMoves
        {
            public Cube Cube { get; set; }

            public Plan(Cube cube)
            {
                this.Cube = cube;
            }

            public IEnumerable<Turn> GetMoves()
            {
                throw new NotImplementedException();
            }

            public void Create()
            {
                var startNewPathSticker = Sticker.URB;
                var okStickers = new List<Sticker>();

                var pos = Sticker.URB;
                var next = this.Cube[pos];
                if (next.Type == startNewPathSticker)
                {
                    // Find new path start
                    StickerExtensionMethods.AllCornerStickers.Whe
                }
                AddCornerMoves(next);
                okStickers.Add(next.Type);
                pos = next;


            }

            private void AddCornerMoves(Cubie cubie)
            {
            }
        }
    }
}
