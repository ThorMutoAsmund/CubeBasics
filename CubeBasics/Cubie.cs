using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class Cubie
    {
        private static int YO = 0;
        private static int YG = 1;
        private static int YR = 2;
        private static int YB = 3;

        private static int OY = 4;
        private static int OG = 5;
        private static int OB = 6;
        private static int OW = 7;

        private static int GY = 8;
        private static int GO = 9;
        private static int GR = 10;
        private static int GW = 11;

        private static int BY = 12;
        private static int BO = 13;
        private static int BR = 14;
        private static int BW = 15;

        private static int RY = 16;
        private static int RG = 17;
        private static int RB = 18;
        private static int RW = 19;

        private static int WO = 20;
        private static int WG = 21;
        private static int WB = 22;
        private static int WR = 23;

        private static int[][] XGroup = new int[6][]
        {
            new int[4] { GO, OB, BR, RG },
            new int[4] { GW, WB, BY, YG },
            new int[4] { YR, RW, WO, OY },
            new int[4] { OW, WR, RY, YO },
            new int[4] { YB, BW, WG, GY },
            new int[4] { GR, RB, BO, OG },
        };

        private static int[][] YGroup = new int[6][]
        {
            new int[4] { YO, YB, YR, YG },
            new int[4] { OY, OG, OW, OB },
            new int[4] { GY, GR, GW, GO },
            new int[4] { BY, BO, BW, BR },
            new int[4] { RY, RB, RW, RG },
            new int[4] { WO, WG, WR, WB },
        };

        private static int[][] ZGroup = new int[6][]
        {
            new int[4] { BW, RW, GW, OW },
            new int[4] { WR, BR, YR, GR },
            new int[4] { YB, RB, WB, OB },
            new int[4] { WG, RG, YG, OG },
            new int[4] { YO, BO, WO, GO },
            new int[4] { BY, OY, GY, RY },
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
    }
}
