using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Singleton_Real
{
    // Lớp Logger quản lý ghi nhật ký ứng dụng (Singleton Real-world)
    public sealed class Logger
    {
        private static Logger _instance = null;
        private static readonly object _lock = new object();
        private int _logCount = 0;

        // Constructor private để ngăn khởi tạo từ bên ngoài
        private Logger()
        {
            Console.WriteLine("[System] Logger Service đã được khởi tạo thành công.");
        }

        // Truy cập instance duy nhất (Thread-safe)
        public static Logger Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                    return _instance;
                }
            }
        }

        // Phương thức ghi log
        public void Log(string message)
        {
            _logCount++;
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[LOG #{_logCount}] [{timeStamp}] {message}");
        }
    }

    // Mô phỏng dịch vụ Quản lý Đơn hàng
    public class OrderService
    {
        public void CreateOrder(string orderId)
        {
            // Sử dụng chung instance Logger
            Logger.Instance.Log($"OrderService: Đã tạo đơn hàng thành công với mã: {orderId}");
        }
    }

    // Mô phỏng dịch vụ Thanh toán
    public class PaymentService
    {
        public void ProcessPayment(string orderId, double amount)
        {
            // Sử dụng chung instance Logger
            Logger.Instance.Log($"PaymentService: Đã thanh toán {amount:N0} VND cho đơn hàng {orderId}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== DEMO SINGLETON REAL-WORLD (LOGGER SERVICE) ===");

            // Khởi tạo các dịch vụ độc lập
            OrderService orderService = new OrderService();
            PaymentService paymentService = new PaymentService();

            // Thực thi luồng ứng dụng
            Logger.Instance.Log("Ứng dụng bắt đầu chạy...");

            orderService.CreateOrder("DH1001");
            paymentService.ProcessPayment("DH1001", 1250000);

            Logger.Instance.Log("Ứng dụng kết thúc tác vụ.");

            Console.ReadLine();
        }
    }
}