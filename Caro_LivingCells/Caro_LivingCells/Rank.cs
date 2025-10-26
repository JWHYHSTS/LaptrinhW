using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BTL
{
    // Lớp Rank: lưu thông tin xếp hạng của người chơi
    public class Rank
    {
        public string Ten { get; set; }
        public int Thang { get; set; }
        public int Thua { get; set; }
        public int Hoa { get; set; }

        public Rank(string ten, int thang, int thua, int hoa)
        {
            Ten = ten;
            Thang = thang;
            Thua = thua;
            Hoa = hoa;
        }

        public override string ToString()
        {
            return $"{Ten}|{Thang}|{Thua}|{Hoa}";
        }
    }
    // Lớp XuLyRank: xử lý việc đọc/ghi và cập nhật bảng xếp hạng
    public class XuLyRank
    {
        private string _filePath;

        public XuLyRank()
        {
            string projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            _filePath = Path.Combine(projectDir, "Resources", "rank.txt");
        }

        // Đọc danh sách xếp hạng từ file rank.txt
        public List<Rank> DocDanhSach()
        {
            List<Rank> ds = new List<Rank>();
            if (!File.Exists(_filePath)) return ds;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var p = line.Split('|');
                if (p.Length >= 4 &&
                    int.TryParse(p[1], out int thang) &&
                    int.TryParse(p[2], out int thua) &&
                    int.TryParse(p[3], out int hoa))
                {
                    ds.Add(new Rank(p[0], thang, thua, hoa));
                }
            }
            return ds;
        }
        // Ghi toàn bộ danh sách xếp hạng ra file rank.txt
        public void GhiDanhSach(List<Rank> ds)
        {
            File.WriteAllLines(_filePath, ds.Select(r => r.ToString()));
        }

        // Cập nhật cho 1 tài khoản
        public void CapNhatKetQua(string ten, string ketqua)
        {
            var ds = DocDanhSach();
            var nguoi = ds.FirstOrDefault(x => x.Ten.Equals(ten, StringComparison.OrdinalIgnoreCase));

            if (nguoi == null)
            {
                nguoi = new Rank(ten, 0, 0, 0);
                ds.Add(nguoi);
            }

            switch (ketqua.ToLower())
            {
                case "thang":
                    nguoi.Thang++;
                    break;
                case "thua":
                    nguoi.Thua++;
                    break;
                case "hoa":
                    nguoi.Hoa++;
                    break;
            }

            GhiDanhSach(ds);
        }
    }
}
