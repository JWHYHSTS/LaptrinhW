using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BTL
{
    public partial class FormBangXepHang : Form
    {
        public FormBangXepHang()
        {
            InitializeComponent();
        }

        private void FormBangXepHang_Load(object sender, EventArgs e)
        {
            XuLyRank xl = new XuLyRank();
            var ds = xl.DocDanhSach();

            // --- Thêm cột Tổng điểm tính theo công thức ---
            var dsWithPoints = ds.Select(x => new
            {
                x.Ten,
                x.Thang,
                x.Thua,
                x.Hoa,
                TongDiem = x.Thang * 20 + x.Hoa * 10 + x.Thua * 0
            })
            .OrderByDescending(x => x.TongDiem) // Sắp xếp theo tổng điểm
            .ThenByDescending(x => x.Thang)
            .ThenBy(x => x.Thua)
            .ToList();

            dataGridView1.DataSource = dsWithPoints;

            // --- Thêm cột STT ---
            DataGridViewTextBoxColumn sttCol = new DataGridViewTextBoxColumn();
            sttCol.HeaderText = "STT";
            sttCol.Name = "STT";
            sttCol.Width = 50;
            dataGridView1.Columns.Insert(0, sttCol);

            // --- Gán STT cho từng dòng ---
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["STT"].Value = i + 1;
            }

            // --- Tô màu top 3 ---
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i == 0) // 🥇 Hạng 1
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gold;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (i == 1) // 🥈 Hạng 2
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (i == 2) // 🥉 Hạng 3
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.SandyBrown;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else // Các hạng khác
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
            }

            // --- Làm đẹp bảng ---
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(135, 206, 250);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // --- Auto fit ---
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
