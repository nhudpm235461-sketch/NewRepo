using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Abstract_Real_XlDonHang_DP
{
    // --- ABSTRACT PRODUCTS ---
    public interface IThanhToan
    {
        void XuLyThanhToan(double soTien);
    }

    public interface IGiaoHang
    {
        void ThucHienGiaoHang();
    }

    // --- CONCRETE PRODUCTS: Online ---
    public class ThanhToanOnline : IThanhToan
    {
        public void XuLyThanhToan(double soTien)
        {
            Console.WriteLine($"[Online] Thanh toan qua cong VNPay/Momo: {soTien:N0} VND");
        }
    }

    public class GiaoHangGietTietkiem : IGiaoHang
    {
        public void ThucHienGiaoHang()
        {
            Console.WriteLine("[Online] Giao hang qua don vi van chuyen (GHTK/GHN).");
        }
    }

    // --- CONCRETE PRODUCTS: Tại cửa hàng (Instore) ---
    public class ThanhToanTienMat : IThanhToan
    {
        public void XuLyThanhToan(double soTien)
        {
            Console.WriteLine($"[Tại cửa hàng] Thanh toan bang tien mat/Quet POS: {soTien:N0} VND");
        }
    }

    public class NhanHangTaiKho : IGiaoHang
    {
        public void ThucHienGiaoHang()
        {
            Console.WriteLine("[Tại cửa hàng] Khach hang nhan hang truc tiep tai quay.");
        }
    }

    // --- ABSTRACT FACTORY ---
    public interface IDonHangFactory
    {
        IThanhToan TaoThanhToan();
        IGiaoHang TaoGiaoHang();
    }

    // --- CONCRETE FACTORIES ---
    public class OnlineOrderFactory : IDonHangFactory
    {
        public IThanhToan TaoThanhToan() => new ThanhToanOnline();
        public IGiaoHang TaoGiaoHang() => new GiaoHangGietTietkiem();
    }

    public class InstoreOrderFactory : IDonHangFactory
    {
        public IThanhToan TaoThanhToan() => new ThanhToanTienMat();
        public IGiaoHang TaoGiaoHang() => new NhanHangTaiKho();
    }

    // --- CLIENT ---
    public class DonHangService
    {
        private readonly IThanhToan _thanhToan;
        private readonly IGiaoHang _giaoHang;

        public DonHangService(IDonHangFactory factory)
        {
            _thanhToan = factory.TaoThanhToan();
            _giaoHang = factory.TaoGiaoHang();
        }

        public void XuLyDonHang(double soTien)
        {
            _thanhToan.XuLyThanhToan(soTien);
            _giaoHang.ThucHienGiaoHang();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== XỬ LÝ ĐƠN HÀNG ONLINE ===");
            IDonHangFactory onlineFactory = new OnlineOrderFactory();
            DonHangService onlineOrder = new DonHangService(onlineFactory);
            onlineOrder.XuLyDonHang(500000);

            Console.WriteLine("\n=== XỬ LÝ ĐƠN HÀNG TẠI CỬA HÀNG ===");
            IDonHangFactory instoreFactory = new InstoreOrderFactory();
            DonHangService instoreOrder = new DonHangService(instoreFactory);
            instoreOrder.XuLyDonHang(250000);

            Console.ReadLine();
        }
    }
}