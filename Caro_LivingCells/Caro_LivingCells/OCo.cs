using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL
{
    // Lớp OCo: đại diện cho một ô trên bàn cờ Caro
    class OCo
    {
        // Kích thước mặc định của 1 ô (pixel)
        public const int _ChieuRong = 25;
        public const int _ChieuCao = 24;
        // Dòng (hàng) của ô trên bàn cờ
        private int _Dong;
        public int Dong
        {
            set { _Dong = value; }
            get { return _Dong; }
        }
        // Cột của ô trên bàn cờ
        private int _Cot;
        public int Cot
        {
            set { _Cot = value; }
            get { return _Cot; }
        }
        // Vị trí (tọa độ) của ô trên giao diện (tọa độ góc trên trái)
        private Point _ViTri;
        public Point ViTri
        {
            set { _ViTri = value; }
            get { return _ViTri; }
        }
        // Trạng thái sở hữu ô:
        // 0 = ô trống, 1 = người chơi, 2 = máy (hoặc đối thủ)
        private int _SoHuu;
        public int SoHuu
        {
            set { _SoHuu = value; }
            get { return _SoHuu; }
        }
        public OCo(int dong,int cot,Point viTri,int soHuu)
        {
            _Dong = dong;
            _Cot = cot;
            _ViTri = viTri;
            _SoHuu = soHuu;
        }
        public OCo() { }
    }
}
