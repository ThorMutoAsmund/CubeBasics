using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeBasics
{
    public class Cubie
    {
        private const int _U = 0;
        private const int _D = 1;
        private const int _L = 2;
        private const int _R = 3;
        private const int _B = 4;
        private const int _F = 5;

        private const int _UF = _U *64 + _F * 8 + _L;
        private const int _UL = _U *64 + _L * 8 + _B;
        private const int _UB = _U *64 + _B * 8 + _R;
        private const int _UR = _U *64 + _R * 8 + _F;

        private const int _FU = _F *64 + _U * 8 + _R;
        private const int _FL = _F *64 + _L * 8 + _U;
        private const int _FR = _F *64 + _R * 8 + _D;
        private const int _FD = _F *64 + _D * 8 + _L;

        private const int _LU = _L *64 + _U * 8 + _F;
        private const int _LF = _L *64 + _F * 8 + _D;
        private const int _LB = _L *64 + _B * 8 + _U;
        private const int _LD = _L *64 + _D * 8 + _B;

        private const int _RU = _R *64 + _U * 8 + _B;
        private const int _RF = _R *64 + _F * 8 + _U;
        private const int _RB = _R *64 + _B * 8 + _D;
        private const int _RD = _R *64 + _D * 8 + _F;

        private const int _BU = _B *64 + _U * 8 + _L;
        private const int _BL = _B *64 + _L * 8 + _D;
        private const int _BR = _B *64 + _R * 8 + _U;
        private const int _BD = _B *64 + _D * 8 + _R;

        private const int _DF = _D *64 + _F * 8 + _R;
        private const int _DL = _D *64 + _L * 8 + _F;
        private const int _DR = _D *64 + _R * 8 + _B;
        private const int _DB = _D *64 + _B * 8 + _L;

        // Left hand rule
        private static int[][] XGroup = new int[6][]
        {
            new int[4] { _LF, _FR, _RB, _BL },
            new int[4] { _LD, _DR, _RU, _UL },
            new int[4] { _UB, _BD, _DF, _FU },
            new int[4] { _FD, _DB, _BU, _UF },
            new int[4] { _UR, _RD, _DL, _LU },
            new int[4] { _LB, _BR, _RF, _FL },
        };

        // Left hand rule
        private static int[][] YGroup = new int[6][]
        {
            new int[4] { _UF, _UR, _UB, _UL },
            new int[4] { _FU, _FL, _FD, _FR },
            new int[4] { _LU, _LB, _LD, _LF },
            new int[4] { _RU, _RF, _RD, _RB },
            new int[4] { _BU, _BR, _BD, _BL },
            new int[4] { _DF, _DL, _DB, _DR },
        };

        // Left hand rule
        private static int[][] ZGroup = new int[6][]
        {
            new int[4] { _RD, _BD, _LD, _FD },
            new int[4] { _DB, _RB, _UB, _LB },
            new int[4] { _UR, _BR, _DR, _FR },
            new int[4] { _DL, _BL, _UL, _FL },
            new int[4] { _UF, _RF, _DF, _LF },
            new int[4] { _RU, _FU, _LU, _BU },
        };

        private Sticker type;
        public Sticker Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
                this.isCorner = value.IsCorner();
                this.isEdge = value.IsEdge();
                SetColorMapping(value);
            }
        }

        public int Orientation{ get; set; }

        private bool isCorner;
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
            get { return this.Orientation == _UF; }
        }

        private OSticker[] ColorMapping { get; set; }

        public Cubie(Sticker type, int orientation = _UF)
        {
            this.Type = type;
            this.Orientation = orientation;
        }

        private void SetColorMapping(Sticker type)
        {
            this.ColorMapping = new OSticker[6];
            switch (type)
            {
                case Sticker.cULF:
                    this.ColorMapping[_U] = OSticker.ULF;
                    this.ColorMapping[_L] = OSticker.LFU;
                    this.ColorMapping[_F] = OSticker.FUL;
                    break;
                case Sticker.cUFR:
                    this.ColorMapping[_U] = OSticker.UFR;
                    this.ColorMapping[_F] = OSticker.FRU;
                    this.ColorMapping[_R] = OSticker.RUF;
                    break;
                case Sticker.cUBL:
                    this.ColorMapping[_U] = OSticker.UBL;
                    this.ColorMapping[_B] = OSticker.BLU;
                    this.ColorMapping[_L] = OSticker.LUB;
                    break;
                case Sticker.cURB:
                    this.ColorMapping[_U] = OSticker.URB;
                    this.ColorMapping[_R] = OSticker.RBU;
                    this.ColorMapping[_B] = OSticker.BUR;
                    break;
                case Sticker.cDFL:
                    this.ColorMapping[_D] = OSticker.DFL;
                    this.ColorMapping[_F] = OSticker.FLD;
                    this.ColorMapping[_L] = OSticker.LDF;
                    break;
                case Sticker.cDRF:
                    this.ColorMapping[_D] = OSticker.DRF;
                    this.ColorMapping[_R] = OSticker.RFD;
                    this.ColorMapping[_F] = OSticker.FDR;
                    break;
                case Sticker.cDLB:
                    this.ColorMapping[_D] = OSticker.DLB;
                    this.ColorMapping[_L] = OSticker.LBD;
                    this.ColorMapping[_B] = OSticker.BDL;
                    break;
                case Sticker.cDBR:
                    this.ColorMapping[_D] = OSticker.DBR;
                    this.ColorMapping[_B] = OSticker.BRD;
                    this.ColorMapping[_R] = OSticker.RDB;
                    break;
                case Sticker.eUF:
                    this.ColorMapping[_U] = OSticker.UF;
                    this.ColorMapping[_F] = OSticker.FU;
                    break;
                case Sticker.eUL:
                    this.ColorMapping[_U] = OSticker.UL;
                    this.ColorMapping[_L] = OSticker.LU;
                    break;
                case Sticker.eUR:
                    this.ColorMapping[_U] = OSticker.UR;
                    this.ColorMapping[_R] = OSticker.RU;
                    break;
                case Sticker.eUB:
                    this.ColorMapping[_U] = OSticker.UB;
                    this.ColorMapping[_B] = OSticker.BU;
                    break;
                case Sticker.eLF:
                    this.ColorMapping[_L] = OSticker.LF;
                    this.ColorMapping[_F] = OSticker.FL;
                    break;
                case Sticker.eLB:
                    this.ColorMapping[_L] = OSticker.LB;
                    this.ColorMapping[_B] = OSticker.BL;
                    break;
                case Sticker.eRF:
                    this.ColorMapping[_R] = OSticker.RF;
                    this.ColorMapping[_F] = OSticker.FR;
                    break;
                case Sticker.eRB:
                    this.ColorMapping[_R] = OSticker.RB;
                    this.ColorMapping[_B] = OSticker.BR;
                    break;
                case Sticker.eDF:
                    this.ColorMapping[_D] = OSticker.DF;
                    this.ColorMapping[_F] = OSticker.FD;
                    break;
                case Sticker.eDL:
                    this.ColorMapping[_D] = OSticker.DF;
                    this.ColorMapping[_L] = OSticker.LD;
                    break;
                case Sticker.eDR:
                    this.ColorMapping[_D] = OSticker.DR;
                    this.ColorMapping[_R] = OSticker.RD;
                    break;
                case Sticker.eDB:
                    this.ColorMapping[_D] = OSticker.DB;
                    this.ColorMapping[_B] = OSticker.BD;
                    break;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", this.Type, this.Orientation);
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

        public OSticker Oriented(OSticker location)
        {
            var result = this.Oriented(location.Sticker());
            var a = this.IsCorner ? 3 : 2;

            return (OSticker)((a * ((int)result / a)) + (((int)result % a) + ((int)location % a)) % a);
        }

        private OSticker Oriented(Sticker location)
        {
            //var f = (this.Orientation - u*64) / 8;
            //var b = (f / 2)*2 + (1 - (f % 2));

            OSticker osticker;
            switch (location)
            {
                case Sticker.cULF:
                case Sticker.cUFR:
                case Sticker.cUBL:
                case Sticker.cURB:
                case Sticker.eUB:
                case Sticker.eUF:
                case Sticker.eUL:
                case Sticker.eUR:
                    {
                        var u = this.Orientation / 64;
                        osticker = this.ColorMapping[u];
                        break;
                    }
                case Sticker.cDFL:
                case Sticker.cDRF:
                case Sticker.cDLB:
                case Sticker.cDBR:
                case Sticker.eDB:
                case Sticker.eDF:
                case Sticker.eDL:
                case Sticker.eDR:
                    {
                        var u = this.Orientation / 64;
                        var d = (u / 2) * 2 + (1 - (u % 2));
                        osticker = this.ColorMapping[d];
                        break;
                    }
                case Sticker.eLB:
                case Sticker.eLF:
                    {
                        var l = this.Orientation % 8;
                        osticker = this.ColorMapping[l];
                        break;
                    }
                case Sticker.eRB:
                case Sticker.eRF:
                    {
                        var l = this.Orientation % 8;
                        var r = (l / 2)*2 + (1 - (l % 2));
                        osticker = this.ColorMapping[r];
                        break;
                    }
                default:
                    throw new InvalidOperationException(String.Format("Color mapping not defined for location '{0}'", location));
            }

            return osticker;

            /*
            var m = this.Mapping[location];

            if (this.Orientation == m[0])
                return (OSticker)((int)this.Type * 3);
            else if (this.Orientation == m[1])
                return (OSticker)((int)this.Type * 3 + 1);
            else if (this.Orientation == m[2])
                return (OSticker)((int)this.Type * 3 + 2);
            else
                throw new InvalidOperationException("Mapping does not contain orientation");
                */
        }
    }
}
