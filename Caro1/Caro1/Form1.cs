using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;


namespace BTL
{
    public partial class Form1 : Form
    {
        private CaroChess caroChess;
        public Graphics grs;
        private int thoiGianConLai = 30;
        WindowsMediaPlayer player = new WindowsMediaPlayer();


        public Form1()
        {
            InitializeComponent();
            caroChess = new CaroChess();
            caroChess.KhoiTaoMangOCo();
            grs=pnlBanco.CreateGraphics();
            caroChess.Player1Name = Program.TenDangNhap; // lấy tên người đăng nhập
            caroChess.HumanPlayer = 1;                    // người thật là Player1

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;      // Ẩn nút phóng to
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Không cho thay đổi kích thước

            player.URL = "Resources\\caro_music.mp3"; // đường dẫn file
            player.settings.setMode("loop", true);     // lặp lại liên tục
            player.controls.play();

        }
        private void DoiMauCheDo(Button nutDangChon)
        {
            // Reset tất cả nút về mặc định
            btnCom.BackColor = Color.White;
            btnCom.ForeColor = Color.Black;

            btnAlphaBeta.BackColor = Color.White;
            btnAlphaBeta.ForeColor = Color.Black;

            btnPvP.BackColor = Color.White;
            btnPvP.ForeColor = Color.Black;

            // Nút đang chọn
            nutDangChon.BackColor = Color.FromArgb(30, 144, 255); // xanh dương
            nutDangChon.ForeColor = Color.White;
        }


        private void pnlBanco_Paint(object sender, PaintEventArgs e)
        {
            caroChess.VeBanCo(grs);
            caroChess.VeLaiQuanCo(grs);
        }

        private void pnlBanco_MouseClick(object sender, MouseEventArgs e)
        {
            if (!caroChess.SanSang)
            {
                return;
            }
            if(caroChess.DanhCo(e.X, e.Y, grs))
            {
                timerTurn.Stop();
                BatDauDemThoiGian();
                if (caroChess.KiemTraChienThang())
                {
                    timerTurn.Stop();
                    lblTime.Text = "00";
                    caroChess.KetThucTroChoi();
                }
                else
                {
                    if (caroChess.CheDoChoi == 2)
                    {
                        caroChess.KhoiDongComputer(grs);
                        if (caroChess.KiemTraChienThang())
                        {
                            timerTurn.Stop();
                            lblTime.Text = "00";
                            caroChess.KetThucTroChoi();
                        }
                    }
                }
            }
            
        }

        private void btnPvP_Click(object sender, EventArgs e)
        {
            grs.Clear(pnlBanco.BackColor);
            caroChess.PvsPStart(grs);
            DoiMauCheDo(btnPvP); // tô màu nút P vs P
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            
            caroChess.undo(grs);
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            caroChess.redo(grs);
        }

        private void btnCom_Click(object sender, EventArgs e)
        {
            grs.Clear(pnlBanco.BackColor);
            caroChess.COMStart(grs);
            DoiMauCheDo(btnCom); // tô màu nút Lv1 dễ
        }

        private void btnAlphaBeta_Click(object sender, EventArgs e)
        {
            grs.Clear(pnlBanco.BackColor);
            caroChess.AlphaBetaStart(grs);
            DoiMauCheDo(btnAlphaBeta); // tô màu nút Lv2 khó
        }

        private void BatDauDemThoiGian()
        {
            timerTurn.Stop(); // dừng nếu đang chạy
            thoiGianConLai = 30;
            lblTime.Text = "30";
            timerTurn.Start();
        }

        private void timerTurn_Tick(object sender, EventArgs e)
        {
            thoiGianConLai--;
            lblTime.Text = thoiGianConLai.ToString();

            if (thoiGianConLai <= 0)
            {
                timerTurn.Stop();
                MessageBox.Show("Hết thời gian! Bạn đã thua lượt này.", "Hết giờ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                caroChess.KetThucTroChoi(); // hoặc mất lượt tùy cách bạn xử lý
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            timerTurn.Stop();
            thoiGianConLai = 30;
            lblTime.Text = "30";
            grs.Clear(pnlBanco.BackColor);

            // Reset theo chế độ chơi hiện tại
            switch (caroChess.CheDoChoi)
            {
                case 1: // PvP
                    caroChess.PvsPStart(grs);
                    break;
                case 2: // Computer
                    caroChess.COMStart(grs);
                    break;
                case 3: // AlphaBeta
                    caroChess.AlphaBetaStart(grs);
                    break;
                default:
                    caroChess.PvsPStart(grs); // Mặc định PvP nếu chưa chọn
                    break;
            }

        }

        private void toggleMusic_CheckedChanged_1(object sender, EventArgs e)
        {
            if (toggleMusic.Checked)
            {
                player.controls.play();
            }
            else
            {
                player.controls.pause();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FormBangXepHang f = new FormBangXepHang();
            f.ShowDialog();
        }
    }
}
