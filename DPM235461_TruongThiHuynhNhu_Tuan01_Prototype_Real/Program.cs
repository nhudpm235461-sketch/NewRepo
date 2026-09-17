using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Prototype_Real
{
    // Interface Prototype cho phép sao chép đối tượng
    public interface INhanVienPrototype
    {
        INhanVienPrototype Clone();
    }

    // Lớp thông tin địa chỉ (đối tượng tham chiếu)
    public class DiaChi
    {
        public string Duong { get; set; }
        public string ThanhPho { get; set; }

        public DiaChi(string duong, string thanhPho)
        {
            Duong = duong;
            ThanhPho = thanhPho;
        }
    }

    // Lớp Nhân viên triển khai Prototype (hỗ trợ cả Shallow Copy và Deep Copy)
    public class NhanVien : INhanVienPrototype
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public DiaChi DiaChiThuongTru { get; set; }

        public NhanVien(string maNV, string hoTen, string chucVu, DiaChi diaChi)
        {
            MaNV = maNV;
            HoTen = hoTen;
            ChucVu = chucVu;
            DiaChiThuongTru = diaChi;
        }

        // Sao chép nông (Shallow Copy)
        public INhanVienPrototype Clone()
        {
            return (INhanVienPrototype)this.MemberwiseClone();
        }

        // Sao chép sâu (Deep Copy)
        public NhanVien DeepClone()
        {
            NhanVien cloned = (NhanVien)this.MemberwiseClone();
            cloned.DiaChiThuongTru = new DiaChi(this.DiaChiThuongTru.Duong, this.DiaChiThuongTru.ThanhPho);
            return cloned;
        }

        public void HienThiThongTin()
        {
            Console.WriteLine($"[Mã NV: {MaNV}] - Họ tên: {HoTen} - Chức vụ: {ChucVu} - Địa chỉ: {DiaChiThuongTru.Duong}, {DiaChiThuongTru.ThanhPho}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== DEMO PROTOTYPE REAL WORLD (QUẢN LÝ NHÂN VIÊN) ===");

            // 1. Khởi tạo đối tượng nhân viên mẫu (Prototype)
            DiaChi diaChiGoc = new DiaChi("123 Nguyễn Huệ", "TP. Hồ Chí Minh");
            NhanVien nvGoc = new NhanVien("NV001", "Trần Ngọc Ánh", "Lập trình viên", diaChiGoc);

            Console.WriteLine("\n-- Nhân viên gốc --");
            nvGoc.HienThiThongTin();

            // 2. Nhân bản nhân viên mẫu bằng Deep Copy để tạo nhân viên mới
            NhanVien nvSaoChep = nvGoc.DeepClone();
            nvSaoChep.MaNV = "NV002";
            nvSaoChep.HoTen = "Nguyễn Văn A";
            nvSaoChep.DiaChiThuongTru.Duong = "456 Lê Lợi"; // Thay đổi địa chỉ riêng cho NV mới

            Console.WriteLine("\n-- Sau khi nhân bản và cập nhật thông tin --");
            Console.Write("Nhân viên gốc: ");
            nvGoc.HienThiThongTin();

            Console.Write("Nhân viên mới (Bản sao): ");
            nvSaoChep.HienThiThongTin();

            Console.ReadLine();
        }
    }
}