using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Singleton_DP
{
    // Lớp áp dụng Singleton Pattern
    public sealed class Singleton
    {
        private static Singleton _instance = null;
        private static readonly object _lock = new object();

        // Constructor riêng tư (Private Constructor) để ngăn không cho tạo instance từ bên ngoài bằng 'new'
        private Singleton()
        {
            Console.WriteLine("--> Instance của Singleton đã được khởi tạo.");
        }

        // Phương thức toàn cục để truy cập vào instance duy nhất (Thread-safe)
        public static Singleton Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                    return _instance;
                }
            }
        }

        public void DoSomething(string message)
        {
            Console.WriteLine($"[Singleton Action] Thông điệp: {message}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== DEMO SINGLETON DESIGN PATTERN ===");

            // Lần truy cập 1: Tạo mới instance
            Console.WriteLine("Yêu cầu Instance lần 1:");
            Singleton s1 = Singleton.Instance;
            s1.DoSomething("Thực hiện công việc 1");

            Console.WriteLine("----------------------------------");

            // Lần truy cập 2: Sử dụng lại instance đã tạo
            Console.WriteLine("Yêu cầu Instance lần 2:");
            Singleton s2 = Singleton.Instance;
            s2.DoSomething("Thực hiện công việc 2");

            Console.WriteLine("----------------------------------");

            // Kiểm tra hai biến tham chiếu s1 và s2 có trỏ cùng vào một vùng nhớ hay không
            if (ReferenceEquals(s1, s2))
            {
                Console.WriteLine("KẾT QUẢ: s1 và s2 là cùng MỘT thể hiện (Instance) duy nhất.");
            }
            else
            {
                Console.WriteLine("KẾT QUẢ: s1 và s2 là hai thể hiện khác nhau.");
            }

            Console.ReadLine();
        }
    }
}