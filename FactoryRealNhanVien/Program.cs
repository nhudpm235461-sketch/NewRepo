using System;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Factory_NhanVien
{
    // 1. Giao diện Sản phẩm trừu tượng (Abstract Product)
    public interface INhanVien
    {
        string GetChucVu();
        double TinhLuong(int soNgayCong);
    }

    // 2. Sản phẩm cụ thể 1: Nhân viên Lập trình viên (Concrete Product)
    public class LapTrinhVien : INhanVien
    {
        public string GetChucVu()
        {
            return "Lap trinh vien (Developer)";
        }

        public double TinhLuong(int soNgayCong)
        {
            // Lương 800,000 VNĐ / ngày
            return soNgayCong * 800000;
        }
    }

    // 3. Sản phẩm cụ thể 2: Nhân viên Kiểm thử (Concrete Product)
    public class KiemThuVien : INhanVien
    {
        public string GetChucVu()
        {
            return "Kiem thu vien (Tester)";
        }

        public double TinhLuong(int soNgayCong)
        {
            // Lương 600,000 VNĐ / ngày
            return soNgayCong * 600000;
        }
    }

    // 4. Lớp Nhà máy trừu tượng (Abstract Creator / Factory)
    public abstract class NhanVienFactory
    {
        // Phương thức nhà máy (Factory Method)
        public abstract INhanVien CreateNhanVien();

        // Thao tác nghiệp vụ
        public void InThongTinLuong(int soNgayCong)
        {
            INhanVien nv = CreateNhanVien();
            Console.WriteLine($"Chuc vu: {nv.GetChucVu()}");
            Console.WriteLine($"So ngay cong: {soNgayCong} ngay");
            Console.WriteLine($"Tong luong: {nv.TinhLuong(soNgayCong):N0} VND");
            Console.WriteLine("-----------------------------------");
        }
    }

    // 5. Nhà máy cụ thể 1 (Concrete Creator)
    public class LapTrinhVienFactory : NhanVienFactory
    {
        public override INhanVien CreateNhanVien()
        {
            return new LapTrinhVien();
        }
    }

    // 6. Nhà máy cụ thể 2 (Concrete Creator)
    public class KiemThuVienFactory : NhanVienFactory
    {
        public override INhanVien CreateNhanVien()
        {
            return new KiemThuVien();
        }
    }

    // 7. Chương trình chính
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== HE THONG QUAN LY NHAN VIEN (FACTORY METHOD) ===\n");

            // Tạo lập trình viên từ Factory tương ứng
            NhanVienFactory devFactory = new LapTrinhVienFactory();
            devFactory.InThongTinLuong(22);

            // Tạo kiểm thử viên từ Factory tương ứng
            NhanVienFactory testerFactory = new KiemThuVienFactory();
            testerFactory.InThongTinLuong(20);

            Console.WriteLine("Nhan pham bat ky de thoat...");
            Console.ReadKey();
        }
    }
}