using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasicsOld
{
    public static class CornerExtensions
    {
        private const int Y = 0;
        private const int O = 1;
        private const int G = 2;
        private const int B = 3;
        private const int R = 4;
        private const int W = 5;

        public static int[] Indices(this Corner corner)
        {
            int[] result;
            switch (corner)
            {
                case Corner.UL:
                case Corner.LF:
                case Corner.FU:
                    result = new int[] { Y, 6, G, 2, O, 0 }; break;
                case Corner.UF:
                case Corner.FR:
                case Corner.RU:
                    result = new int[] { Y, 8, O, 2, B, 0 }; break;
                case Corner.UB:
                case Corner.BL:
                case Corner.LU:
                    result = new int[] { 0, 0, 4, 6, 2, 0 }; break;
                case Corner.UR:
                case Corner.RB:
                case Corner.BU:
                    result = new int[] { 0, 3, 3, 3, 4, 8 }; break;
                case Corner.FL:
                case Corner.LD:
                case Corner.DF:
                    result = new int[] { 1, 6, 2, 8, 5, 0 }; break;
                case Corner.FD:
                case Corner.DR:
                case Corner.RF:
                    result = new int[] { 1, 8, 5, 3, 3, 6 }; break;
                case Corner.BR:
                case Corner.RD:
                case Corner.DB:
                    result = new int[] { 4, 2, 3, 8, 5, 8 }; break;
                case Corner.BD:
                case Corner.DL:
                case Corner.LB:
                default:
                    result = new int[] { 4, 0, 5, 6, 2, 6 }; break;
            }

            var v = (int)corner;
            return new int[] { result[(v * 2) % 6], result[(v * 2 + 1) % 6], result[(v * 2 + 2) % 6], result[(v * 2 + 3) % 6] };
        }
    }

    public enum Edge
    {
        UF, FU, UL, LU, UR, RU, UB, BU, FL, LF, FR, RF, FD, DF, LB, BL, LD, DL, RB, BR, RD, DR, BD, DB
    }

    public enum Corner
    {
        UL = 0, LF = 1, FU = 2,
        UF = 3, FR = 4, RU = 5,
        UB = 6, BL = 7, LU = 8,
        UR = 9, RB = 10, BU = 11,
        FL = 12, LD = 13, DF = 14,
        FD = 15, DR = 16, RF = 17,
        BR = 18, RD = 19, DB = 20,
        BD = 21, DL = 22, LB = 23
    }

    public enum Move
    {
        U, U_, F, F_, L, L_, R, R_, B, B_, D, D_
    }

    public interface IPlan
    {
        IEnumerable<Move> GetMoves();
    }


    public class Cube
    {
        public const int Y = 0;
        public const int O = 1;
        public const int G = 2;
        public const int B = 3;
        public const int R = 4;
        public const int W = 5;

        public int Dimension { get; private set; }
        private readonly int[] AllColors = new int[] { Y, O, G, B, R, W };

        public Face[] Faces { get; private set; }

        public Cube(int dimension)
        {
            this.Dimension = dimension;
            Reset();
        }

        public void Reset()
        {
            this.Faces = new Face[6];
            for (int i = 0; i < this.AllColors.Length; ++i)
            {
                this.Faces[i] = new Face(this.Dimension, this.AllColors[i]);
            }
        }

        public bool IsSolved()
        {
            return this.Faces.All(f => f.AreSameColor());
        }

        private void Transform(int[] code, bool inverse)
        {
            var clone = this.Clone();
            for (int i = 0; i < code.Length; i += 4)
            {
                if (!inverse)
                {
                    this.Faces[code[i + 2]].Stickers[code[i + 3]] = clone.Faces[code[i]].Stickers[code[i + 1]];
                }
                else
                {
                    this.Faces[code[i]].Stickers[code[i + 1]] = clone.Faces[code[i + 2]].Stickers[code[i + 3]];
                }
            }
        }

        public Cube Clone()
        {
            var result = new Cube(this.Dimension);
            for (int i = 0; i < 6; ++i)
            {
                result.Faces[i].CopyFrom(this.Faces[i]);
            }
            return result;
        }

        public void Apply(IPlan plan)
        {
            foreach (var move in plan.GetMoves())
            {
                ApplyMove(move);
            }
        }

        public void ApplyMove(Move move)
        {
            switch (move)
            {
                case Move.U: Up(); break;
                case Move.U_: Up_(); break;
                case Move.F: Front(); break;
                case Move.F_: Front_(); break;
                case Move.L: Left(); break;
                case Move.L_: Left_(); break;
                case Move.R: Right(); break;
                case Move.R_: Right_(); break;
                case Move.B: Back(); break;
                case Move.B_: Back_(); break;
                case Move.D: Down(); break;
                case Move.D_: Down_(); break;
            }
        }

        public string Scramble(int turns)
        {
            var result = String.Empty;
            var rand = new Random();
            while (turns > 0)
            {
                switch (rand.Next(12))
                {
                    case 0: result += "U"; this.Up(); break;
                    case 1: result += "U'"; this.Up_(); break;
                    case 2: result += "F"; this.Front(); break;
                    case 3: result += "F'"; this.Front_(); break;
                    case 4: result += "L"; this.Left(); break;
                    case 5: result += "L'"; this.Left_(); break;
                    case 6: result += "R"; this.Right(); break;
                    case 7: result += "R'"; this.Right_(); break;
                    case 8: result += "B"; this.Back(); break;
                    case 9: result += "B'"; this.Back_(); break;
                    case 10: result += "D"; this.Down(); break;
                    case 11: result += "D'"; this.Down_(); break;
                }
                --turns;
            }

            return result;
        }

        public void Left(bool inverse = false)
        {
            Transform(new int[] { Y,0,O,0, Y,3,O,3, Y,6,O,6,    O,0,W,0, O,3,W,3, O,6,W,6,   W,0,R,0, W,3,R,3, W,6,R,6,   R,0,Y,0, R,3,Y,3, R,6,Y,6,
                                 G,0,G,2, G,1,G,5, G,2,G,8, G,3,G,1, G,5,G,7, G,6,G,0, G,7,G,3, G,8,G,6}, inverse);
        }
        public void Left_()
        {
            Left(true);
        }
        public void Right_(bool inverse = false)
        {
            Transform(new int[] { Y,2,O,2, Y,5,O,5, Y,8,O,8,    O,2,W,2, O,5,W,5, O,8,W,8,   W,2,R,2, W,5,R,5, W,8,R,8,   R,2,Y,2, R,5,Y,5, R,8,Y,8,
                                 B,0,B,6, B,1,B,3, B,2,B,0, B,3,B,7, B,5,B,1, B,6,B,8, B,7,B,5, B,8,B,2}, inverse);
        }
        public void Right()
        {
            Right_(true);
        }
        public void Up(bool inverse = false)
        {
            Transform(new int[] { R,6,B,2, R,7,B,1, R,8,B,0,    B,2,O,2, B,1,O,1, B,0,O,0,   O,0,G,0, O,1,G,1, O,2,G,2,   G,0,R,8, G,1,R,7, G,2,R,6,
                                 Y,0,Y,2, Y,1,Y,5, Y,2,Y,8, Y,3,Y,1, Y,5,Y,7, Y,6,Y,0, Y,7,Y,3, Y,8,Y,6}, inverse);
        }
        public void Up_()
        {
            Up(true);
        }
        public void Front(bool inverse = false)
        {
            Transform(new int[] { Y,6,B,0, Y,7,B,3, Y,8,B,6,    B,0,W,2, B,3,W,1, B,6,W,0,   W,2,G,8, W,1,G,5, W,0,G,2,   G,8,Y,6, G,5,Y,7, G,2,Y,8,
                                 O,0,O,2, O,1,O,5, O,2,O,8, O,3,O,1, O,5,O,7, O,6,O,0, O,7,O,3, O,8,O,6}, inverse);
        }
        public void Front_()
        {
            Front(true);
        }
        public void Back(bool inverse = false)
        {
            Transform(new int[] { W,6,B,8, W,7,B,5, W,8,B,2,    B,8,Y,2, B,5,Y,1, B,2,Y,0,   Y,2,G,0, Y,1,G,3, Y,0,G,6,   G,0,W,6, G,3,W,7, G,6,W,8,
                                 R,0,R,2, R,1,R,5, R,2,R,8, R,3,R,1, R,5,R,7, R,6,R,0, R,7,R,3, R,8,R,6}, inverse);
        }
        public void Back_()
        {
            Back(true);
        }
        public void Down(bool inverse = false)
        {
            Transform(new int[] { O,6,B,6, O,7,B,7, O,8,B,8,    B,6,R,2, B,7,R,1, B,8,R,0,   R,2,G,6, R,1,G,7, R,0,G,8,   G,6,O,6, G,7,O,7, G,8,O,8,
                                 W,0,W,2, W,1,W,5, W,2,W,8, W,3,W,1, W,5,W,7, W,6,W,0, W,7,W,3, W,8,W,6}, inverse);
        }
        public void Down_()
        {
            Down(true);
        }

        public void Show()
        {
            Console.WriteLine("___________");
            Console.WriteLine("    {0}", RenderStickers(this.Faces[W], new int[] { 0, 1, 2 }));
            Console.WriteLine("    {0}", RenderStickers(this.Faces[W], new int[] { 3, 4, 5 }));
            Console.WriteLine("    {0}", RenderStickers(this.Faces[W], new int[] { 6, 7, 8 }));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("    {0}", RenderStickers(this.Faces[R], new int[] { 0, 1, 2 }));
            Console.WriteLine("    {0}", RenderStickers(this.Faces[R], new int[] { 3, 4, 5 }));
            Console.WriteLine("    {0}", RenderStickers(this.Faces[R], new int[] { 6, 7, 8 }));
            Console.WriteLine();
            Console.WriteLine("{0} {1} {2}", RenderStickers(this.Faces[G], new int[] { 6, 3, 0 }), RenderStickers(this.Faces[Y], new int[] { 0, 1, 2 }), RenderStickers(this.Faces[B], new int[] { 2, 5, 8 }));
            Console.WriteLine("{0} {1} {2}", RenderStickers(this.Faces[G], new int[] { 7, 4, 1 }), RenderStickers(this.Faces[Y], new int[] { 3, 4, 5 }), RenderStickers(this.Faces[B], new int[] { 1, 4, 7 }));
            Console.WriteLine("{0} {1} {2}", RenderStickers(this.Faces[G], new int[] { 8, 5, 2 }), RenderStickers(this.Faces[Y], new int[] { 6, 7, 8 }), RenderStickers(this.Faces[B], new int[] { 0, 3, 6 }));
            Console.WriteLine();
            Console.WriteLine("    {0}", RenderStickers(this.Faces[O], new int[] { 0, 1, 2 }));
            Console.WriteLine("    {0}", RenderStickers(this.Faces[O], new int[] { 3, 4, 5 }));
            Console.WriteLine("    {0}", RenderStickers(this.Faces[O], new int[] { 6, 7, 8 }));
        }

        private string RenderStickers(Face face, int[] stickers)
        {
            var result = String.Empty;
            foreach (int sticker in stickers)
            {
                switch (face.Stickers[sticker])
                {
                    case Y: result += "Y"; break;
                    case O: result += "O"; break;
                    case G: result += "G"; break;
                    case B: result += "B"; break;
                    case R: result += "R"; break;
                    case W: result += "W"; break;
                }
            }
            return result;
        }
    }

    public class Face
    {
        public int Dimension { get; private set; }
        public int[] Stickers { get; private set; }

        public int NumberOfStickers
        {
            get
            {
                return this.Dimension * this.Dimension;
            }
        }

        public Face(int dimension, int color)
        {
            this.Dimension = dimension;
            this.Stickers = new int[this.NumberOfStickers];
            SetAllStickers(color);
        }

        public void SetAllStickers(int color)
        {
            for (int i = 0; i < this.NumberOfStickers; ++i)
            {
                this.Stickers[i] = color;
            }
        }

        public void CopyFrom(Face other)
        {
            for (int i = 0; i < this.NumberOfStickers; ++i)
            {
                this.Stickers[i] = other.Stickers[i];
            }
        }

        public bool AreAll(int color)
        {
            return this.Stickers.All(s => s == color);
        }

        public bool AreSameColor()
        {
            return this.Stickers.All(s => s == this.Stickers[0]);
        }
    }

}
