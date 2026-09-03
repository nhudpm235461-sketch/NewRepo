using System;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Abstract_DP
{
    // =========================================================
    // 1. ABSTRACT PRODUCTS (Các sản phẩm trừu tượng)
    // =========================================================

    // Sản phẩm A: Nút bấm
    public interface IButton
    {
        void Paint();
    }

    // Sản phẩm B: Ô nhập liệu
    public interface ITextBox
    {
        void Render();
    }

    // =========================================================
    // 2. CONCRETE PRODUCTS (Các sản phẩm cụ thể)
    // =========================================================

    // Windows Products
    public class WinButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("[WinButton] Hien thi nut bấm theo phong cach Windows.");
        }
    }

    public class WinTextBox : ITextBox
    {
        public void Render()
        {
            Console.WriteLine("[WinTextBox] Hien thi ô nhap lieu theo phong cach Windows.");
        }
    }

    // Mac Products
    public class MacButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("[MacButton] Hien thi nut bấm theo phong cach macOS.");
        }
    }

    public class MacTextBox : ITextBox
    {
        public void Render()
        {
            Console.WriteLine("[MacTextBox] Hien thi ô nhap lieu theo phong cach macOS.");
        }
    }

    // =========================================================
    // 3. ABSTRACT FACTORY (Nhà máy trừu tượng)
    // =========================================================
    public interface IGUIFactory
    {
        IButton CreateButton();
        ITextBox CreateTextBox();
    }

    // =========================================================
    // 4. CONCRETE FACTORIES (Các nhà máy cụ thể)
    // =========================================================

    // Nhà máy tạo UI Windows
    public class WinFactory : IGUIFactory
    {
        public IButton CreateButton()
        {
            return new WinButton();
        }

        public ITextBox CreateTextBox()
        {
            return new WinTextBox();
        }
    }

    // Nhà máy tạo UI Mac
    public class MacFactory : IGUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ITextBox CreateTextBox()
        {
            return new MacTextBox();
        }
    }

    // =========================================================
    // 5. CLIENT (Lớp ứng dụng người dùng)
    // =========================================================
    public class Application
    {
        private readonly IButton _button;
        private readonly ITextBox _textBox;

        public Application(IGUIFactory factory)
        {
            _button = factory.CreateButton();
            _textBox = factory.CreateTextBox();
        }

        public void RenderUI()
        {
            _button.Paint();
            _textBox.Render();
        }
    }

    // =========================================================
    // 6. PROGRAM (Chương trình chính)
    // =========================================================
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- Chạy giao diện trên Windows ---
            Console.WriteLine("=== VE GIAO DIEN WINDOWS ===");
            IGUIFactory winFactory = new WinFactory();
            Application winApp = new Application(winFactory);
            winApp.RenderUI();

            Console.WriteLine();

            // --- Chạy giao diện trên macOS ---
            Console.WriteLine("=== VE GIAO DIEN MACOS ===");
            IGUIFactory macFactory = new MacFactory();
            Application macApp = new Application(macFactory);
            macApp.RenderUI();

            Console.WriteLine("\nNhan pham bat ky de thoat...");
            Console.ReadKey();
        }
    }
}