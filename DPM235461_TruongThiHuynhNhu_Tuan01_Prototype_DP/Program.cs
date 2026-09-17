using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Prototype_DP
{
    // Prototype interface
    public abstract class Prototype
    {
        public string Id { get; set; }

        public Prototype(string id)
        {
            this.Id = id;
        }

        // Phương thức Clone để nhân bản đối tượng
        public abstract Prototype Clone();
    }

    // Concrete Prototype 1
    public class ConcretePrototype1 : Prototype
    {
        public string Feature { get; set; }

        public ConcretePrototype1(string id, string feature) : base(id)
        {
            this.Feature = feature;
        }

        public override Prototype Clone()
        {
            // Thực hiện Shallow Copy (sao chép nông)
            return (Prototype)this.MemberwiseClone();
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"[Prototype 1] ID: {Id}, Feature: {Feature}");
        }
    }

    // Concrete Prototype 2
    public class ConcretePrototype2 : Prototype
    {
        public int Capacity { get; set; }

        public ConcretePrototype2(string id, int capacity) : base(id)
        {
            this.Capacity = capacity;
        }

        public override Prototype Clone()
        {
            // Thực hiện Shallow Copy
            return (Prototype)this.MemberwiseClone();
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"[Prototype 2] ID: {Id}, Capacity: {Capacity}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== DEMO PROTOTYPE DESIGN PATTERN ===");

            // Tạo đối tượng gốc
            ConcretePrototype1 original1 = new ConcretePrototype1("P1_001", "Tính năng mẫu A");
            Console.Write("Gốc: ");
            original1.DisplayInfo();

            // Sao chép/Nhân bản đối tượng 1
            ConcretePrototype1 cloned1 = (ConcretePrototype1)original1.Clone();
            cloned1.Id = "P1_002"; // Thay đổi thuộc tính đối tượng bản sao
            Console.Write("Bản sao: ");
            cloned1.DisplayInfo();

            Console.WriteLine("----------------------------------");

            // Tạo đối tượng gốc 2
            ConcretePrototype2 original2 = new ConcretePrototype2("P2_100", 500);
            Console.Write("Gốc: ");
            original2.DisplayInfo();

            // Sao chép/Nhân bản đối tượng 2
            ConcretePrototype2 cloned2 = (ConcretePrototype2)original2.Clone();
            cloned2.Id = "P2_101";
            cloned2.Capacity = 1000;
            Console.Write("Bản sao: ");
            cloned2.DisplayInfo();

            Console.ReadLine();
        }
    }
}