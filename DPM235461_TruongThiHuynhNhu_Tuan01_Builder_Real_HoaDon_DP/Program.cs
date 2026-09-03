using System;
using System.Collections.Generic;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Builder_Real_HoaDon_DP
{
    // 1. Sản phẩm (Product)
    public class HoaDon
    {
        public string MaHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime NgayLap { get; set; }
        public List<string> DanhSachSanPham { get; set; } = new List<string>();
        public double TongTien { get; set; }
        public double ChietKhau { get; set; }
        public string PhuongThucThanhToan { get; set; }

        public void HienThi()
        {
            Console.WriteLine("================ HOÁ ĐƠN =============");
            Console.WriteLine($"Mã hóa đơn: {MaHoaDon}");
            Console.WriteLine($"Khách hàng: {TenKhachHang}");
            Console.WriteLine($"Ngày lập: {NgayLap:dd/MM/yyyy HH:mm}");
            Console.WriteLine("Sản phẩm:");
            foreach (var sp in DanhSachSanPham)
            {
                Console.WriteLine($"  - {sp}");
            }
            Console.WriteLine($"Tổng tiền ban đầu: {TongTien:N0} VND");
            Console.WriteLine($"Chiết khấu: {ChietKhau}%");

            double thanhTien = TongTien * (1 - ChietKhau / 100);
            Console.WriteLine($"Thành tiền: {thanhTien:N0} VND");
            Console.WriteLine($"Phương thức thanh toán: {PhuongThucThanhToan}");
            Console.WriteLine("======================================\n");
        }
    }

    // 2. Giao diện Builder (Abstract Builder)
    public interface IHoaDonBuilder
    {
        IHoaDonBuilder DatMaHoaDon(string maHD);
        IHoaDonBuilder DatKhachHang(string tenKH);
        IHoaDonBuilder DatNgayLap(DateTime ngayLap);
        IHoaDonBuilder ThemSanPham(string tenSP, double gia);
        IHoaDonBuilder DatChietKhau(double chietKhau);
        IHoaDonBuilder DatPhuongThucThanhToan(string phuongThuc);
        HoaDon Build();
    }

    // 3. Builder cụ thể (Concrete Builder)
    public class HoaDonBuilder : IHoaDonBuilder
    {
        private HoaDon _hoaDon = new HoaDon();

        public HoaDonBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            _hoaDon = new HoaDon
            {
                NgayLap = DateTime.Now,
                PhuongThucThanhToan = "Tiền mặt",
                ChietKhau = 0
            };
        }

        public IHoaDonBuilder DatMaHoaDon(string maHD)
        {
            _hoaDon.MaHoaDon = maHD;
            return this;
        }

        public IHoaDonBuilder DatKhachHang(string tenKH)
        {
            _hoaDon.TenKhachHang = tenKH;
            return this;
        }

        public IHoaDonBuilder DatNgayLap(DateTime ngayLap)
        {
            _hoaDon.NgayLap = ngayLap;
            return this;
        }

        public IHoaDonBuilder ThemSanPham(string tenSP, double gia)
        {
            _hoaDon.DanhSachSanPham.Add($"{tenSP} ({gia:N0} VND)");
            _hoaDon.TongTien += gia;
            return this;
        }

        public IHoaDonBuilder DatChietKhau(double chietKhau)
        {
            _hoaDon.ChietKhau = chietKhau;
            return this;
        }

        public IHoaDonBuilder DatPhuongThucThanhToan(string phuongThuc)
        {
            _hoaDon.PhuongThucThanhToan = phuongThuc;
            return this;
        }

        public HoaDon Build()
        {
            HoaDon result = _hoaDon;
            this.Reset(); // Re-initialize để builder tiếp tục dùng cho lần sau
            return result;
        }
    }

    // 4. Lớp điều khiển dựng sẵn mẫu (Director - Tuỳ chọn)
    public class HoaDonDirector
    {
        private IHoaDonBuilder _builder;

        public HoaDonDirector(IHoaDonBuilder builder)
        {
            _builder = builder;
        }

        // Tạo hóa đơn bán lẻ nhanh
        public HoaDon TaoHoaDonBanLe(string maHD, string tenKH, string tenSP, double gia)
        {
            return _builder.DatMaHoaDon(maHD)
                           .DatKhachHang(tenKH)
                           .ThemSanPham(tenSP, gia)
                           .DatPhuongThucThanhToan("Tiền mặt")
                           .Build();
        }
    }

    // 5. Chương trình chính
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- Cách 1: Sử dụng Builder trực tiếp (Method Chaining) ---
            IHoaDonBuilder builder = new HoaDonBuilder();

            HoaDon hdVIP = builder.DatMaHoaDon("HD001")
                                 .DatKhachHang("Trương Thị Huỳnh Như")
                                 .ThemSanPham("Laptop Dell XPS 15", 35000000)
                                 .ThemSanPham("Chuột Wireless Logi", 800000)
                                 .DatChietKhau(10) // Giảm 10% cho khách VIP
                                 .DatPhuongThucThanhToan("Chuyển khoản")
                                 .Build();

            hdVIP.HienThi();

            // --- Cách 2: Sử dụng Director để tạo mẫu hóa đơn bán lẻ nhanh ---
            HoaDonDirector director = new HoaDonDirector(builder);
            HoaDon hdBanLe = director.TaoHoaDonBanLe("HD002", "Khách Bán Lẻ", "Bàn phím cơ", 1200000);

            hdBanLe.HienThi();

            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}