using System;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Factory_DB
{
    // 1. Giao diện sản phẩm (Product Interface)
    public interface IButton
    {
        void Render();
        void OnClick(Action action);
    }

    // 2. Sản phẩm cụ thể 1 (Concrete Product)
    public class WindowsButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("[Windows Button] Hien thi nut theo kieu Windows OS.");
        }

        public void OnClick(Action action)
        {
            Console.WriteLine("[Windows Button] Gan su kien nhap chuot cua Windows OS.");
            action?.Invoke();
        }
    }

    // 3. Sản phẩm cụ thể 2 (Concrete Product)
    public class HTMLButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("[HTML Button] Tra ve va hien thi dinh dang HTML.");
        }

        public void OnClick(Action action)
        {
            Console.WriteLine("[HTML Button] Gan su kien click cua trinh duyệt web.");
            action?.Invoke();
        }
    }

    // 4. Lớp Creator trừu tượng (Abstract Factory Class)
    public abstract class Dialog
    {
        // Phương thức nhà máy (Factory Method) trừu tượng
        public abstract IButton CreateButton();

        // Logic nghiệp vụ cốt lõi sử dụng đối tượng do Factory Method trả về
        public void Render()
        {
            // Gọi Factory Method để tạo nút
            IButton okButton = CreateButton();

            // Gán sự kiện và hiển thị nút
            okButton.OnClick(CloseDialog);
            okButton.Render();
        }

        private void CloseDialog()
        {
            Console.WriteLine("-> Hop thoại da duoc dong.");
        }
    }

    // 5. Creator cụ thể 1
    public class WindowsDialog : Dialog
    {
        public override IButton CreateButton()
        {
            return new WindowsButton();
        }
    }

    // 6. Creator cụ thể 2
    public class WebDialog : Dialog
    {
        public override IButton CreateButton()
        {
            return new HTMLButton();
        }
    }

    // 7. Lớp ứng dụng để chạy mã
    internal class Program
    {
        private static Dialog dialog;

        private static void Initialize(string osType)
        {
            if (osType == "Windows")
            {
                dialog = new WindowsDialog();
            }
            else if (osType == "Web")
            {
                dialog = new WebDialog();
            }
            else
            {
                throw new Exception("Loi! He dieu hanh khong hop le.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- CHAY CHUONG TRINH TREN WINDOWS ---");
            Initialize("Windows");
            dialog.Render();

            Console.WriteLine("\n--- CHAY CHUONG TRINH TREN WEB ---");
            Initialize("Web");
            dialog.Render();

            Console.ReadLine();
        }
    }
}