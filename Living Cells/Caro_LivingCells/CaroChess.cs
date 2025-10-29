using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL
{
    // Enum xác định trạng thái kết thúc ván cờ
    public enum KetThuc
    {
        HoaCo, // hòa
        P1, // người chơi 1 thắng 
        P2, // người chơi 2 thắng 
        COM // Máy thắng 

    }
    // Lớp chính điều khiển logic của trò chơi Caro
    class CaroChess
    {
        private OCo[,] MangOco;
        private BanCo _BanCo;

        //public static Pen pen;
        public static Pen penRed;
        public static Pen penBlue;
        public static SolidBrush sbWhite;
        public static SolidBrush sbBlack;
        // Stack lưu lịch sử nước đi để lại
        public Stack<OCo> list_OcoCanNuocDaDi;
        public Stack<OCo> CannuocUndo;
        public static SolidBrush sbGreen;
        private int luotDi;
        private bool _SangSang;
        private KetThuc ketThuc;
        private int ChedoChoi;
        private int humanPlayer;
        private int computerPlayer;
        private bool isAlphaBetaMode;
        public bool SanSang
        {
            get { return _SangSang; }
        }
        public int CheDoChoi
        {
            get { return ChedoChoi; }
        }
        // Hàm khởi tạo
        public CaroChess()
        {
            penRed = new Pen(Color.Red, 5);
            penBlue = new Pen(Color.Blue, 5);
            sbWhite = new SolidBrush(Color.White);
            sbBlack = new SolidBrush(Color.Black);
            _BanCo = new BanCo(20, 20);
            MangOco = new OCo[_BanCo.SoDong, _BanCo.SoCot];
            list_OcoCanNuocDaDi = new Stack<OCo>();
            luotDi = 1;
            sbGreen=new SolidBrush(Color.Lime);
            CannuocUndo=new Stack<OCo>();
        }
        // ==================== CÁC HÀM VẼ & KHỞI TẠO ====================
        // Vẽ bàn cờ
        public void VeBanCo(Graphics g)
        {
            _BanCo.VeBanCo(g);
        }
        // Khởi tạo toàn bộ ma trận ô cờ (trống)
        public void KhoiTaoMangOCo()
        {
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    MangOco[i, j] = new OCo(i, j, new Point(j * OCo._ChieuRong, i * OCo._ChieuCao), 0);
                }
            }
        }
        // ==================== HÀM ĐÁNH CỜ ====================
        // Xử lý khi người chơi click chuột lên bàn cờ
        public bool DanhCo(int MouseX, int MouseY, Graphics g)
        {
            if (MouseX % OCo._ChieuRong == 0 && MouseY % OCo._ChieuCao == 0)
            {
                return false;
            }
            int Cot = MouseX / OCo._ChieuRong;
            int Dong = MouseY / OCo._ChieuCao;
            if (MangOco[Dong, Cot].SoHuu != 0)
            {
                return false;
            }

            if (luotDi == 1)
            {
                MangOco[Dong, Cot].SoHuu = 1;
                //_BanCo.VeQuanCo(g, MangOco[Dong, Cot].ViTri, sbBlack);
                _BanCo.VeQuanCoX(g, MangOco[Dong, Cot].ViTri, penBlue);
                luotDi = 2;
            }
            else
            {
                MangOco[Dong, Cot].SoHuu = 2;
                //_BanCo.VeQuanCo(g, MangOco[Dong, Cot].ViTri, sbWhite);
                _BanCo.VeQuanCoO(g, MangOco[Dong, Cot].ViTri, penRed);
                luotDi = 1;
            }
            // Lưu lại nước đi
            CannuocUndo = new Stack<OCo>();
            OCo oco=new OCo(MangOco[Dong,Cot].Dong, MangOco[Dong, Cot].Cot, MangOco[Dong, Cot].ViTri, MangOco[Dong, Cot].SoHuu);
            list_OcoCanNuocDaDi.Push(oco);
            return true;
        }
        // Vẽ lại toàn bộ quân cờ (dùng sau khi refresh form)
        public void VeLaiQuanCo(Graphics g)
        {

            foreach (var oco in list_OcoCanNuocDaDi)
            {
                if (oco.SoHuu == 1)
                {
                    //_BanCo.VeQuanCo(g, oco.ViTri, sbBlack);
                    _BanCo.VeQuanCoX(g, oco.ViTri, penBlue);
                }
                else if (oco.SoHuu == 2)
                {
                    //_BanCo.VeQuanCo(g, oco.ViTri, sbWhite);
                    _BanCo.VeQuanCoO(g, oco.ViTri, penRed);
                }
            }   

        }
        // ==================== CHẾ ĐỘ CHƠI ====================
        // Khởi tạo chơi người với người
        public void PvsPStart(Graphics g)
        {
            _SangSang=true;
            list_OcoCanNuocDaDi=new Stack<OCo> { };
            CannuocUndo = new Stack<OCo> { } ;
            luotDi = 1;
            ChedoChoi = 1;
            KhoiTaoMangOCo();

            VeBanCo(g);
        }
        // Chơi với máy (COMStart)
        public void COMStart(Graphics g)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn chơi trước không?", "Chọn lượt chơi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            InitializePlayers(result == DialogResult.Yes);
            StartComputerGame(g, false);
        }
        // Chơi với máy (Alpha-Beta)
        public void AlphaBetaStart(Graphics g)
        {

            DialogResult result = MessageBox.Show("Bạn có muốn chơi trước không?", "Chọn lượt chơi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            InitializePlayers(result == DialogResult.Yes);
            StartComputerGame(g, true);
        }
        // Khởi tạo người chơi và máy (xác định ai đi trước)
        private void InitializePlayers(bool isHumanFirst)
        {
            if (isHumanFirst)
            {
                humanPlayer = 1;
                computerPlayer = 2;
                luotDi = 1; // Người đi trước
            }
            else
            {
                humanPlayer = 2;
                computerPlayer = 1;
                luotDi = 2; // Máy đi trước
            }
        }
        // Bắt đầu chơi game với máy (có thể chọn chế độ COMStart hoặc AlphaBeta)
        public void StartComputerGame(Graphics g, bool alphaBetaMode)
        {
            _SangSang = true;
            list_OcoCanNuocDaDi = new Stack<OCo> { };
            CannuocUndo = new Stack<OCo> { };
            luotDi = 1;
            ChedoChoi = 2;
            isAlphaBetaMode = alphaBetaMode;
            KhoiTaoMangOCo();

            VeBanCo(g);
            // Computer makes first move if not human first
            if (humanPlayer == 2)
            {
                if (alphaBetaMode)
                    KhoiDongAlphaBeta(g);
                else
                    KhoiDongComputer(g);
            }
        }
        // ==================== UNDO / REDO ====================
        // Quay lại 1 nước
        public void undo(Graphics g)
        {
            if (list_OcoCanNuocDaDi.Count!=0)
            {
                OCo oco = list_OcoCanNuocDaDi.Pop();
                CannuocUndo.Push(new OCo(oco.Dong,oco.Cot,oco.ViTri,oco.SoHuu));
                MangOco[oco.Dong, oco.Cot].SoHuu = 0;
                _BanCo.XoaQuanCo(g, oco.ViTri, sbGreen);
                if (luotDi == 1)
                {
                    luotDi = 2;
                }
                else { luotDi = 1; }
            }
           
        }
        // Làm lại 1 nước (sau undo)
        public void redo(Graphics g)
        {
            if (CannuocUndo.Count != 0)
            {
                OCo oco = CannuocUndo.Pop();
                list_OcoCanNuocDaDi.Push(new OCo(oco.Dong, oco.Cot, oco.ViTri, oco.SoHuu));
                MangOco[oco.Dong, oco.Cot].SoHuu = oco.SoHuu;
                //_BanCo.VeQuanCo(g, oco.ViTri, oco.SoHuu==1?sbBlack:sbWhite);
                if (oco.SoHuu == 1)
                {
                    _BanCo.VeQuanCoX(g, oco.ViTri, penBlue);
                }
                else { _BanCo.VeQuanCoO(g, oco.ViTri, penRed);  }
                if (luotDi == 1)
                {
                    luotDi = 2;
                }
                else { luotDi = 1; }
            }


        }
        // ==================== KẾT THÚC & XẾP HẠNG ====================
        public string Player1Name { get; set; } = "Player1"; // người thật
        public int HumanPlayer { get; set; } = 1; // 1 hoặc 2, người thật là bên nào

        // Hiển thị kết quả sau khi kết thúc game
        public void KetThucTroChoi()
        {
            string message = "";
            switch (ketThuc)
            {
                case KetThuc.HoaCo:
                    message = "Hòa cờ";
                    break;
                case KetThuc.P1:
                    message = (ChedoChoi == 2) ? (humanPlayer == 1 ? "Chúc mừng! Bạn đã đánh bại được máy!" : "Người chơi 1 thắng") : "Người chơi 1 thắng";
                    break;
                case KetThuc.P2:
                    message = (ChedoChoi == 2) ? (humanPlayer == 2 ? "Chúc mừng! Bạn đã đánh bại được máy!" : "Người chơi 2 thắng") : "Người chơi 2 thắng";
                    break;
                case KetThuc.COM:
                    message = "Máy thắng";
                    break;
            }

            // ✅ CHÈN ĐOẠN NÀY Ở ĐÂY 👇
            try
            {
                if (ChedoChoi == 2) // chỉ tính khi chơi với máy
                {
                    XuLyRank xl = new XuLyRank();
                    string nguoiChoi = Player1Name;

                    if (ketThuc == KetThuc.HoaCo)
                        xl.CapNhatKetQua(nguoiChoi, "hoa");
                    else if ((ketThuc == KetThuc.P1 && HumanPlayer == 1) ||
                             (ketThuc == KetThuc.P2 && HumanPlayer == 2))
                        xl.CapNhatKetQua(nguoiChoi, "thang");
                    else
                        xl.CapNhatKetQua(nguoiChoi, "thua");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật rank: " + ex.Message);
            }

            // ✅ Giữ nguyên dòng dưới (thông báo kết thúc game)
            MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _SangSang = false;
        }
        // ==================== KIỂM TRA CHIẾN THẮNG ====================
        public bool KiemTraChienThang( )
        {
            
            if (list_OcoCanNuocDaDi.Count == _BanCo.SoCot * _BanCo.SoDong)
            {
                ketThuc = KetThuc.HoaCo;
                return true;
            }
            foreach (var oco in list_OcoCanNuocDaDi)
            {
                bool win = DuyetDoc(oco.Dong, oco.Cot, oco.SoHuu) ||
                          DuyetNgang(oco.Dong, oco.Cot, oco.SoHuu) ||
                          DuyetCheoXuoi(oco.Dong, oco.Cot, oco.SoHuu) ||
                          DuyetCheoNguoc(oco.Dong, oco.Cot, oco.SoHuu);

                if (win)
                {
                    // Nếu đang chơi với máy thì xác định người thắng là ai
                    if (ChedoChoi == 2) 
                    {
                        ketThuc = (oco.SoHuu == computerPlayer) ? KetThuc.COM :
                                 (oco.SoHuu == humanPlayer) ? KetThuc.P1 : KetThuc.P2;
                    }
                    else
                    {
                        ketThuc = oco.SoHuu == 1 ? KetThuc.P1 : KetThuc.P2;
                    }
                    return true;
                }
            }
            return false;
        }
        // ==============================================================
        // Các hàm duyệt thắng - chặn 5 quân liên tiếp
        // ==============================================================
        private bool DuyetDoc(int curDong,int curCot,int curSohuu)
        {
            if (curDong>_BanCo.SoDong-5)
            {
                return false;
            }
            int dem;
            for (dem = 1; dem < 5; dem++)
            {
                if (MangOco[curDong+dem,curCot].SoHuu!=curSohuu)
                {
                    return false;
                }
            }
            if (curDong==0||curDong+dem==_BanCo.SoDong)
            {
                return true;
            }
            if (MangOco[curDong - 1, curCot].SoHuu == 0 || MangOco[curDong+dem,curCot].SoHuu==0)
                { return true; }
            
            return false ;
        }
        private bool DuyetNgang(int curDong, int curCot, int curSohuu)
        {
            if (curCot > _BanCo.SoCot - 5)
            {
                return false;
            }
            int dem;
            for (dem = 1; dem < 5; dem++)
            {
                if (MangOco[curDong, curCot+dem].SoHuu != curSohuu)
                {
                    return false;
                }
            }
            if (curCot == 0 || curCot + dem == _BanCo.SoCot)
            {
                return true;
            }
            if (MangOco[curDong , curCot-1].SoHuu == 0 || MangOco[curDong, curCot+dem].SoHuu == 0)
            { return true; }

            return false;
        }
        private bool DuyetCheoXuoi(int curDong, int curCot, int curSohuu)
        {
            if (curDong>_BanCo.SoDong-5 || curCot > _BanCo.SoCot - 5)
            {
                return false;
            }
            int dem;
            for (dem = 1; dem < 5; dem++)
            {
                if (MangOco[curDong+dem, curCot + dem].SoHuu != curSohuu)
                {
                    return false;
                }
            }
            if (curDong==0 ||curDong+dem==_BanCo.SoDong|| curCot == 0 || curCot + dem == _BanCo.SoCot)
            {
                return true;
            }
            if (MangOco[curDong-1, curCot - 1].SoHuu == 0 || MangOco[curDong+dem, curCot + dem].SoHuu == 0)
            { return true; }

            return false;
        }
        private bool DuyetCheoNguoc(int curDong, int curCot, int curSohuu)
        {
            if (curDong<4 || curCot > _BanCo.SoCot - 5)
            {
                return false;
            }
            int dem;
            for (dem = 1; dem < 5; dem++)
            {
                if (MangOco[curDong-dem, curCot + dem].SoHuu != curSohuu)
                {
                    return false;
                }
            }
            if (curDong == 4 || curDong == _BanCo.SoDong-1||curCot==0||curCot+dem==_BanCo.SoCot)
            {
                return true;
            }
            if (MangOco[curDong+1, curCot - 1].SoHuu == 0 || MangOco[curDong-dem, curCot + dem].SoHuu == 0)
            { return true; }

            return false;
        }
        private long[] MangDiemTanCong = { 0, 3, 24, 192, 1536, 12288, 98304 };
        private long[] MangDiemPhongThu = { 0, 5, 32, 256, 4096, 65536, 524288 };

        public void KhoiDongComputer(Graphics g)
        {
            if (list_OcoCanNuocDaDi.Count==0)
            {
                DanhCo(_BanCo.SoCot / 2 * OCo._ChieuRong + 1, _BanCo.SoDong / 2 * OCo._ChieuCao + 1, g);

            }
            else
            {
                OCo oco = TimKiemNuocDi();
                DanhCo(oco.ViTri.X+1,oco.ViTri.Y+1,g);
            }
        }
        public void KhoiDongAlphaBeta(Graphics g)
        {
            if (list_OcoCanNuocDaDi.Count == 0)
            {
                DanhCo(_BanCo.SoCot / 2 * OCo._ChieuRong + 1, _BanCo.SoDong / 2 * OCo._ChieuCao + 1, g);

            }
            else
            {
                OCo oco = TimKiemNuocDiAlphaBeta();
                DanhCo(oco.ViTri.X + 1, oco.ViTri.Y + 1, g);
            }
        }
        // ==============================================================
        // Các hàm tính điểm AI tấn công - phòng thủ
        // ==============================================================
        private OCo TimKiemNuocDi() 
        {
            OCo oCoresult = new OCo();
            long DiemMax = 0;
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    if (MangOco[i, j].SoHuu == 0)
                    {
                        long DiemTanCong = DiemTanCongDuyetDoc(i, j) + DiemTanCongDuyetNgang(i, j) + DiemTanCongDuyetCheoXuoi(i, j) + DiemTanCongDuyetCheoNguoc(i, j);
                        long DiemPhongThu = DiemPhongThuDuyetDoc(i, j) + DiemPhongThuDuyetNgang(i, j) + DiemPhongThuDuyetCheoXuoi(i, j) + DiemPhongThuDuyetCheoNguoc(i, j);
                        long Diemtam = DiemTanCong > DiemPhongThu ? DiemTanCong : DiemPhongThu;
                        if (DiemMax < Diemtam)
                        {
                            DiemMax = Diemtam;
                            oCoresult = new OCo(MangOco[i, j].Dong, MangOco[i, j].Cot, MangOco[i, j].ViTri, MangOco[i, j].SoHuu);
                        }
                    }
                }
            }
            return oCoresult;


        }
        private OCo TimKiemNuocDiAlphaBeta()
        {
            OCo oCoresult = new OCo();
            int depth = 3; // Độ sâu tìm kiếm, có thể điều chỉnh
            long alpha = long.MinValue;
            long beta = long.MaxValue;
            long bestScore = long.MinValue;

            
            List<OCo> candidates = GenerateCandidateMoves();
            candidates = SortCandidates(candidates);

            foreach (OCo candidate in candidates)
            {
                int i = candidate.Dong;
                int j = candidate.Cot;

                MangOco[i, j].SoHuu = computerPlayer; 
                long score = AlphaBeta(depth - 1, alpha, beta, false, i, j);
                MangOco[i, j].SoHuu = 0; 

                if (score > bestScore)
                {
                    bestScore = score;
                    oCoresult = new OCo(i, j, MangOco[i, j].ViTri, 1);
                    alpha = Math.Max(alpha, bestScore);
                }

                if (beta <= alpha)
                    break; 
            }

            return oCoresult;
        }
        private List<OCo> GenerateCandidateMoves()
        {
            List<OCo> candidates = new List<OCo>();

            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    if (MangOco[i, j].SoHuu != 0)
                        continue;

                    bool isCandidate = false;

                    
                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            if (di == 0 && dj == 0)
                                continue;

                            int ni = i + di;
                            int nj = j + dj;

                            if (ni >= 0 && ni < _BanCo.SoDong && nj >= 0 && nj < _BanCo.SoCot)
                            {
                                if (MangOco[ni, nj].SoHuu != 0)
                                {
                                    isCandidate = true;
                                    break;
                                }
                            }
                        }
                        if (isCandidate)
                            break;
                    }

                    if (isCandidate)
                        candidates.Add(MangOco[i, j]);
                }
            }

            
            if (candidates.Count == 0)
            {
                int centerDong = _BanCo.SoDong / 2;
                int centerCot = _BanCo.SoCot / 2;
                candidates.Add(MangOco[centerDong, centerCot]);
            }

            return candidates;
        }

        private List<OCo> SortCandidates(List<OCo> candidates)
        {
            
            var scored = candidates.Select(c =>
            {
                int score = 0;
                for (int di = -1; di <= 1; di++)
                {
                    for (int dj = -1; dj <= 1; dj++)
                    {
                        if (di == 0 && dj == 0)
                            continue;

                        int ni = c.Dong + di;
                        int nj = c.Cot + dj;

                        if (ni >= 0 && ni < _BanCo.SoDong && nj >= 0 && nj < _BanCo.SoCot)
                        {
                            if (MangOco[ni, nj].SoHuu != 0)
                                score++;
                        }
                    }
                }
                return new { Candidate = c, Score = score };
            }).OrderByDescending(x => x.Score).Select(x => x.Candidate).ToList();

            return scored;
        }
        // Hàm Alpha-Beta Pruning
        private long AlphaBeta(int depth, long alpha, long beta, bool maximizingPlayer, int lastRow, int lastCol)
        {
            if (depth == 0 || KiemTraChienThangCOM(lastRow, lastCol) != 0)
            {
                return EvaluateBoard(); // Đánh giá bàn cờ tại độ sâu hiện tại
            }

            List<OCo> candidates = GenerateCandidateMoves();
            candidates = SortCandidates(candidates);

            if (maximizingPlayer)
            {
                long maxEval = long.MinValue;
                foreach (OCo candidate in candidates)
                {
                    int i = candidate.Dong;
                    int j = candidate.Cot;

                    MangOco[i, j].SoHuu = 1; // Máy đánh 
                    long eval = AlphaBeta(depth - 1, alpha, beta, false, i, j);
                    MangOco[i, j].SoHuu = 0; // Undo

                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
                return maxEval;
            }
            else
            {
                long minEval = long.MaxValue;
                foreach (OCo candidate in candidates)
                {
                    int i = candidate.Dong;
                    int j = candidate.Cot;

                    MangOco[i, j].SoHuu = 2; // Đối thủ đánh 
                    long eval = AlphaBeta(depth - 1, alpha, beta, true, i, j);
                    MangOco[i, j].SoHuu = 0; // Undo

                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }
        // Hàm đánh giá bàn cờ 
        private long EvaluateBoard()
        {
            long score = 0;
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    if (MangOco[i, j].SoHuu != 0)
                    {
                        score += DiemTanCongDuyetDoc(i, j) - DiemPhongThuDuyetDoc(i, j);
                        score += DiemTanCongDuyetNgang(i, j) - DiemPhongThuDuyetNgang(i, j);
                        score += DiemTanCongDuyetCheoXuoi(i, j) - DiemPhongThuDuyetCheoXuoi(i, j);
                        score += DiemTanCongDuyetCheoNguoc(i, j) - DiemPhongThuDuyetCheoNguoc(i, j);
                    }
                }
            }
            return score;
        }
        private int KiemTraChienThangCOM(int row, int col)
        {
            if (DuyetDoc(row, col, 1) || DuyetNgang(row, col, 1) || DuyetCheoXuoi(row, col, 1) || DuyetCheoNguoc(row, col, 1))
            {
                return 1;
            }
            if (DuyetDoc(row, col, 2) || DuyetNgang(row, col, 2) || DuyetCheoXuoi(row, col, 2) || DuyetCheoNguoc(row, col, 2))
            {
                return -1;
            }
            return 0;
        }
        private long DiemTanCongDuyetDoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Đếm từ vị trí hiện tại xuống (dưới)
            for (int i = 1; i <= 5 && currDong + i < _BanCo.SoDong; i++)
            {
                if (MangOco[currDong + i, currCot].SoHuu == computerPlayer) //if (MangOco[currDong + i, currCot].SoHuu == 1)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong + i, currCot].SoHuu == humanPlayer) //if (MangOco[currDong + i, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Đếm từ vị trí hiện tại lên (trên)
            for (int i = 1; i <= 5 && currDong - i >= 0; i++)
            {
                if (MangOco[currDong - i, currCot].SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong - i, currCot].SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Nếu bị chặn cả 2 đầu thì không có giá trị tấn công
            if (SoQuanDich >= 2)
            {
                return 0;
            }

            // Tăng điểm theo số quân ta và giảm theo quân địch (phòng thủ)
            DiemTong += MangDiemTanCong[SoQuanTa];

            // Nếu bị chặn 1 đầu thì trừ nhẹ điểm (ít cơ hội hơn)
            //if (SoQuanDich == 1)
            //{
            //    DiemTong /= 2;
            //}

            return DiemTong;
        }
        // Các hàm tính điểm theo 4 hướng (doc/ngang/chéo)
        private long DiemTanCongDuyetNgang(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Duyệt sang phải
            for (int i = 1; i <= 5 && currCot + i < _BanCo.SoCot; i++)
            {
                if (MangOco[currDong, currCot + i].SoHuu == computerPlayer) // if (MangOco[currDong + i, currCot].SoHuu == 1)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong, currCot + i].SoHuu == humanPlayer) //if (MangOco[currDong + i, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Duyệt sang trái
            for (int i = 1; i <= 5 && currCot - i >= 0; i++)
            {
                if (MangOco[currDong, currCot - i].SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong, currCot - i].SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Nếu bị chặn 2 đầu thì không có giá trị tấn công
            if (SoQuanDich >= 2)
            {
                return 0;
            }

            // Cộng điểm theo số quân ta, xử lý nếu bị chặn 1 đầu
            DiemTong += MangDiemTanCong[SoQuanTa];
            //if (SoQuanDich == 1)
            //{
            //    DiemTong /= 2; // Chia điểm nếu bị chặn một đầu
            //}
            return DiemTong;
        }

        private long DiemTanCongDuyetCheoXuoi(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Duyệt chéo xuống phải (↘)
            for (int i = 1; i <= 5 && currCot + i < _BanCo.SoCot && currDong + i < _BanCo.SoDong; i++)
            {
                if (MangOco[currDong + i, currCot + i].SoHuu == computerPlayer) //if (MangOco[currDong + i, currCot].SoHuu == 1)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong + i, currCot + i].SoHuu == humanPlayer) //if (MangOco[currDong + i, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Duyệt chéo lên trái (↖)
            for (int i = 1; i <= 5 && currCot - i >= 0 && currDong - i >= 0; i++)
            {
                if (MangOco[currDong - i, currCot - i].SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong - i, currCot - i].SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Nếu bị chặn 2 đầu thì bỏ qua
            if (SoQuanDich >= 2)
            {
                return 0;
            }

            // Cộng điểm theo số quân ta, giảm nếu bị chặn 1 đầu
            DiemTong += MangDiemTanCong[SoQuanTa];
            //if (SoQuanDich == 1)
            //{
            //    DiemTong /= 2;
            //}

            return DiemTong;
        }

        private long DiemTanCongDuyetCheoNguoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Duyệt lên phải ↗
            for (int i = 1; i <= 5 && currCot + i < _BanCo.SoCot && currDong - i >= 0; i++)
            {
                if (MangOco[currDong - i, currCot + i].SoHuu == computerPlayer) //if (MangOco[currDong + i, currCot].SoHuu == 1)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong - i, currCot + i].SoHuu == humanPlayer) //if (MangOco[currDong + i, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Duyệt xuống trái ↙
            for (int i = 1; i <= 5 && currCot - i >= 0 && currDong + i < _BanCo.SoDong; i++)
            {
                if (MangOco[currDong + i, currCot - i].SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                }
                else if (MangOco[currDong + i, currCot - i].SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                    break;
                }
                else break;
            }

            // Nếu bị chặn 2 đầu thì không có cơ hội tấn công
            if (SoQuanDich >= 2)
            {
                return 0;
            }

            // Tính điểm tấn công dựa vào số quân ta và số quân địch cản
            DiemTong += MangDiemTanCong[SoQuanTa];
            //if (SoQuanDich == 1)
            //{
            //    DiemTong /= 2; // Nếu bị chặn một đầu thì giảm điểm
            //}

            return DiemTong;
        }

        private long DiemPhongThuDuyetDoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Duyệt xuống
            for (int i = 1; i <= 5 && currDong + i < _BanCo.SoDong; i++)
            {
                int SoHuu = MangOco[currDong + i, currCot].SoHuu;
                if (SoHuu == humanPlayer) //if (SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer) //if (SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Duyệt lên
            for (int i = 1; i <= 5 && currDong - i >= 0; i++)
            {
                int SoHuu = MangOco[currDong - i, currCot].SoHuu;
                if (SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Nếu đã có 2 quân ta chặn 2 đầu thì không cần phòng thủ nữa
            if (SoQuanTa >= 2)
            {
                return 0;
            }

            // Tính điểm phòng thủ dựa vào số quân địch
            DiemTong += MangDiemPhongThu[SoQuanDich];

            // Nếu chỉ bị chặn 1 đầu thì tăng điểm lên một chút
            if (SoQuanTa == 1)
            {
                DiemTong /= 2;
            }

            return DiemTong;
        }

        private long DiemPhongThuDuyetNgang(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Duyệt sang phải
            for (int i = 1; i <= 5 && currCot + i < _BanCo.SoCot; i++)
            {
                int SoHuu = MangOco[currDong, currCot + i].SoHuu;
                if (SoHuu == humanPlayer) //if (SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer) //if (SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Duyệt sang trái
            for (int i = 1; i <= 5 && currCot - i >= 0; i++)
            {
                int SoHuu = MangOco[currDong, currCot - i].SoHuu;
                if (SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Nếu có 2 quân ta chặn thì không cần phòng thủ
            if (SoQuanTa >= 2)
            {
                return 0;
            }

            // Tính điểm phòng thủ
            DiemTong += MangDiemPhongThu[SoQuanDich];

            // Nếu bị chặn một đầu thì giảm điểm
            if (SoQuanTa == 1)
            {
                DiemTong /= 2;
            }

            return DiemTong;
        }

        private long DiemPhongThuDuyetCheoXuoi(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Duyệt chéo xuống phải
            for (int i = 1; i <= 5 && currDong + i < _BanCo.SoDong && currCot + i < _BanCo.SoCot; i++)
            {
                int SoHuu = MangOco[currDong + i, currCot + i].SoHuu;
                if (SoHuu == humanPlayer) //if (SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer) //if (SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Duyệt chéo lên trái
            for (int i = 1; i <= 5 && currDong - i >= 0 && currCot - i >= 0; i++)
            {
                int SoHuu = MangOco[currDong - i, currCot - i].SoHuu;
                if (SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Nếu bị chặn cả hai đầu bởi quân ta thì không cần phòng thủ
            if (SoQuanTa >= 2)
            {
                return 0;
            }

            // Tính điểm phòng thủ
            DiemTong += MangDiemPhongThu[SoQuanDich];

            // Nếu bị chặn một đầu thì giảm điểm phòng thủ
            if (SoQuanTa == 1)
            {
                DiemTong /= 2;
            }

            return DiemTong;
        }

        private long DiemPhongThuDuyetCheoNguoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;

            // Duyệt chéo lên phải
            for (int i = 1; i <= 5 && currCot + i < _BanCo.SoCot && currDong - i >= 0; i++)
            {
                int SoHuu = MangOco[currDong - i, currCot + i].SoHuu;
                if (SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Duyệt chéo xuống trái
            for (int i = 1; i <= 5 && currCot - i >= 0 && currDong + i < _BanCo.SoDong; i++)
            {
                int SoHuu = MangOco[currDong + i, currCot - i].SoHuu;
                if (SoHuu == humanPlayer)
                {
                    SoQuanDich++;
                }
                else if (SoHuu == computerPlayer)
                {
                    SoQuanTa++;
                    break;
                }
                else break;
            }

            // Nếu cả 2 đầu bị chặn bởi quân ta thì bỏ qua
            if (SoQuanTa >= 2)
            {
                return 0;
            }

            // Tính điểm phòng thủ theo số lượng quân địch
            DiemTong += MangDiemPhongThu[SoQuanDich];

            // Nếu bị chặn 1 đầu bởi quân ta thì giảm nhẹ điểm
            if (SoQuanTa == 1)
            {
                DiemTong /= 2;
            }
            return DiemTong;
        }
    }
}