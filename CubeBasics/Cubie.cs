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


        private static int[][] XGroup = new int[6][]
        {
            new int[4] { _LF, _FR, _RB, _BL },
            new int[4] { _LD, _DR, _RU, _UL },
            new int[4] { _UB, _BD, _DF, _FU },
            new int[4] { _FD, _DB, _BU, _UF },
            new int[4] { _UR, _RD, _DL, _LU },
            new int[4] { _LB, _BR, _RF, _FL },
        };

        private static int[][] YGroup = new int[6][]
        {
            new int[4] { _UF, _UR, _UB, _UL },
            new int[4] { _FU, _FL, _FD, _FR },
            new int[4] { _LU, _LB, _LD, _LF },
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
            new int[4] { _RU, _FU, _LU, _BU },
        };

        //private Dictionary<Sticker, int[]> Mappings = new Dictionary<Sticker, int[]>();

        /*
        private static Dictionary<Sticker, Dictionary<Sticker, int[]>> Mappings = new Dictionary<Sticker, Dictionary<Sticker, int[]>>();
        {
            { Sticker.cULF, new  Dictionary<Sticker, int[]> { { Sticker.cULF, new int[] { _UF, _LU, _FL } } } },
            { Sticker.cUFR, new  Dictionary<Sticker, int[]> { { Sticker.cUFR, new int[] { _UF, _FR, _RU } } } },
            { Sticker.cUBL, new  Dictionary<Sticker, int[]> { { Sticker.cUBL, new int[] { _UF, _BR, _LD } } } },
            { Sticker.cURB, new  Dictionary<Sticker, int[]> { { Sticker.cURB, new int[] { _UF, _RD, _BL } } } },
            { Sticker.cDFL, new  Dictionary<Sticker, int[]> { { Sticker.cULF, new int[] { _UF, _BL, _RD } } } },
            { Sticker.cDRF, new  Dictionary<Sticker, int[]> { { Sticker.cULF, new int[] { _UF, _LD, _BR } } } },
            { Sticker.cDLB, new  Dictionary<Sticker, int[]> { { Sticker.cULF, new int[] { _UF, _RU, _FR } } } },
            { Sticker.cDBR, new  Dictionary<Sticker, int[]> { { Sticker.cULF, new int[] { _UF, _FL, _LU } } } },
            { Sticker.eDB, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eDF, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eDL, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eDR, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eLB, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eLF, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eRB, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eRF, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eUB, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eUF, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eUL, new  Dictionary<Sticker, int[]> {  } },
            { Sticker.eUR, new  Dictionary<Sticker, int[]> {  } },
        };*/

        /// <summary>
        /// Indexers med 1 = egen type, 2 = positionen. Man finder positionen af this.Orientation i det man får ud og det er oStickeren
        /// </summary>

        //private Dictionary<Sticker, int[]> mapping;
        //public Dictionary<Sticker, int[]> Mapping
        //{
        //    get
        //    {
        //        return this.mapping;
        //    }
        //}

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
                SetOStickerFromColor(value);
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

        private OSticker[] OStickerFromColor { get; set; }

        public Cubie(Sticker type, int orientation = _UF)
        {
            this.Type = type;
            this.Orientation = orientation;
        }

        private void SetOStickerFromColor(Sticker type)
        {
            this.OStickerFromColor = new OSticker[6];
            switch (type)
            {
                case Sticker.cULF:
                    this.OStickerFromColor[_U] = OSticker.ULF;
                    this.OStickerFromColor[_L] = OSticker.LFU;
                    this.OStickerFromColor[_F] = OSticker.FUL;
                    break;
                case Sticker.cUFR:
                    this.OStickerFromColor[_U] = OSticker.UFR;
                    this.OStickerFromColor[_F] = OSticker.FRU;
                    this.OStickerFromColor[_R] = OSticker.RUF;
                    break;
                case Sticker.cUBL:
                    this.OStickerFromColor[_U] = OSticker.UBL;
                    this.OStickerFromColor[_B] = OSticker.BLU;
                    this.OStickerFromColor[_L] = OSticker.LUB;
                    break;
                case Sticker.cURB:
                    this.OStickerFromColor[_U] = OSticker.URB;
                    this.OStickerFromColor[_R] = OSticker.RBU;
                    this.OStickerFromColor[_B] = OSticker.BUR;
                    break;
                case Sticker.cDFL:
                    this.OStickerFromColor[_D] = OSticker.DFL;
                    this.OStickerFromColor[_F] = OSticker.FLD;
                    this.OStickerFromColor[_L] = OSticker.LDF;
                    break;
                case Sticker.cDRF:
                    this.OStickerFromColor[_D] = OSticker.DRF;
                    this.OStickerFromColor[_R] = OSticker.RFD;
                    this.OStickerFromColor[_F] = OSticker.FDR;
                    break;
                case Sticker.cDLB:
                    this.OStickerFromColor[_D] = OSticker.DLB;
                    this.OStickerFromColor[_L] = OSticker.LBD;
                    this.OStickerFromColor[_B] = OSticker.BDL;
                    break;
                case Sticker.cDBR:
                    this.OStickerFromColor[_D] = OSticker.DBR;
                    this.OStickerFromColor[_B] = OSticker.BRD;
                    this.OStickerFromColor[_R] = OSticker.RDB;
                    break;
            }
        }

        /*
        private Dictionary<Sticker, int[]> CreateMapping(Sticker type)
        {
            var result = new Dictionary<Sticker, int[]>();

            foreach (var loc in StickerExtensionMethods.AllCornerStickers)
            {
                switch (loc)
                {
                    case Sticker.cULF:

                        break;
                    case Sticker.cUFR:
                        break;
                    case Sticker.cUBL:
                        break;
                    case Sticker.cURB:
                        break;
                    case Sticker.cDFL:
                        break;
                    case Sticker.cDRF:
                        break;
                    case Sticker.cDLB:
                        break;
                    case Sticker.cDBR:
                        break;
                }
            }

            return result;
        }*/

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
            var a = (3*((int)result / 3)) + (((int)result % 3) + ((int)location % 3)) % 3;
            return (OSticker)a;
        }

        private OSticker Oriented(Sticker location)
        {
            var u = this.Orientation / 64;
            var d = (u / 2) + (1 - (u % 2));
            //var f = (this.Orientation - u*64) / 8;
            //var l = this.Orientation % 8;
            //var r = (l / 2) + (1 - (l % 2));
            //var b = (f / 2) + (1 - (f % 2));

            OSticker osticker;
            switch (location)
            {
                case Sticker.cULF:
                case Sticker.cUFR:
                case Sticker.cUBL:
                case Sticker.cURB:
                    osticker = this.OStickerFromColor[u];
                    break;
                //case Sticker.cDFL:
                //case Sticker.cDRF:
                //case Sticker.cDLB:
                //case Sticker.cDBR:
                default:
                    osticker = this.OStickerFromColor[d];
                    break;
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
