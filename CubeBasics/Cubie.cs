using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class Cubie
    {
        private static int _UF = 0;
        private static int _UL = 1;
        private static int _UB = 2;
        private static int _UR = 3;

        private static int _FU = 4;
        private static int _FL = 5;
        private static int _FR = 6;
        private static int _FD = 7;

        private static int _LO = 8;
        private static int _LF = 9;
        private static int _LB = 10;
        private static int _LD = 11;

        private static int _RU = 12;
        private static int _RF = 13;
        private static int _RB = 14;
        private static int _RD = 15;

        private static int _BU = 16;
        private static int _BL = 17;
        private static int _BR = 18;
        private static int _BD = 19;

        private static int _DF = 20;
        private static int _DL = 21;
        private static int _DR = 22;
        private static int _DB = 23;

        private static int[][] XGroup = new int[6][]
        {
            new int[4] { _LF, _FR, _RB, _BL },
            new int[4] { _LD, _DR, _RU, _UL },
            new int[4] { _UB, _BD, _DF, _FU },
            new int[4] { _FD, _DB, _BU, _UF },
            new int[4] { _UR, _RD, _DL, _LO },
            new int[4] { _LB, _BR, _RF, _FL },
        };

        private static int[][] YGroup = new int[6][]
        {
            new int[4] { _UF, _UR, _UB, _UL },
            new int[4] { _FU, _FL, _FD, _FR },
            new int[4] { _LO, _LB, _LD, _LF },
            new int[4] { _RU, _RF, _RD, _RB },
            new int[4] { _BU, _BR, _BD, _BL },
            new int[4] { _DF, _DL, _DB, _DR },
        };

        private static int[][] ZGroup = new int[6][]
        {
            new int[4] { _RD, _BD, _LD, _FD },
            new int[4] { _DB, _RB, _UB, _LB },
            new int[4] { _UR, _BR, _DR, _FR },
            new int[4] { _DL, _BL, _UL, _FL },
            new int[4] { _UF, _RF, _DF, _LF },
            new int[4] { _RU, _FU, _LO, _BU },
        };

        public Sticker type;
        public Sticker Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
                this.isCorner = this.type.IsCorner();
                this.isEdge = this.type.IsEdge();
            }
        }

        public int Orientation{ get; set; }

        public bool isCorner;
        public bool IsCorner
        {
            get
            {
                return this.isCorner;
            }
        }

        public bool isEdge;
        public bool IsEdge
        {
            get
            {
                return this.isEdge;
            }
        }

        public bool IsCorrect
        {
            get { return this.Orientation == 0; }
        }

        public Cubie(Sticker type, int orientation = 0)
        {
            this.Type = type;
            this.Orientation = orientation;
        }

        public Cubie Clone(int? orientation = null)
        {
            return new Cubie(this.Type, orientation.HasValue ? orientation.Value : this.Orientation);
        }

        private Cubie Rotate(int[][] group, bool inverse)
        {
            int[] row = group.First(r => r.Contains(this.Orientation));
            int index = Array.IndexOf(row, this.Orientation);
            int orientation = row[(index + (inverse ? 3 : 5)) % 4];
            return Clone(orientation);
        }

        public Cubie Rot(Axis axis, bool inverse = false)
        {
            switch (axis)
            {
                case Axis.X:
                    return Rotate(XGroup, inverse);
                case Axis.Y:
                    return Rotate(YGroup, inverse);
                default: //case Axis.Z:
                    return Rotate(ZGroup, inverse);
            }
        }

        public Cubie RotX(bool inverse = false)
        {
            return Rotate(XGroup, inverse);
        }
        public Cubie RotX_(bool inverse = false)
        {
            return RotX(true);
        }
        public Cubie RotY(bool inverse = false)
        {
            return Rotate(YGroup, inverse);
        }
        public Cubie RotY_(bool inverse = false)
        {
            return RotY(true);
        }
        public Cubie RotZ(bool inverse = false)
        {
            return Rotate(ZGroup, inverse);
        }
        public Cubie RotZ_(bool inverse = false)
        {
            return RotZ(true);
        }

        public OSticker Oriented(Sticker location)
        {
            var o = this.Orientation;
            switch (this.Type)
            {
                case Sticker.ULF:
                    switch (location)
                    {
                        case Sticker.ULF:
                            switch (this.Orientation)
                            {
                                case 0: return OSticker.ULF;
                                case LF: return OSticker.LFU;
                                case 0: return OSticker.FUL;
                            }
                            break;
                    }
                    break;
                default:
                    throw new InvalidOperationException("Oriented sticker can not be calculated");
            }
        }
    }
}
