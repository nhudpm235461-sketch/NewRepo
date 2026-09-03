using System;

namespace DPM235461_TruongThiHuynhNhu_Tuan01_Builder_Real_DB
{
    // 1. Sản phẩm (Product): Chuỗi cấu hình kết nối DB
    public class DatabaseConnection
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool IntegratedSecurity { get; set; }
        public int Timeout { get; set; }

        public string GetConnectionString()
        {
            if (IntegratedSecurity)
            {
                return $"Server={Server},{Port};Database={Database};Integrated Security=True;Connection Timeout={Timeout};";
            }
            return $"Server={Server},{Port};Database={Database};User Id={Username};Password={Password};Connection Timeout={Timeout};";
        }

        public void DisplayInfo()
        {
            Console.WriteLine("=== THÔNG TIN KẾT NỐI CƠ SỞ DỮ LIỆU ===");
            Console.WriteLine($"Server: {Server}:{Port}");
            Console.WriteLine($"Database: {Database}");
            Console.WriteLine($"Xác thực Windows: {(IntegratedSecurity ? "Có" : "Không")}");
            if (!IntegratedSecurity)
            {
                Console.WriteLine($"User: {Username}");
            }
            Console.WriteLine($"Timeout: {Timeout}s");
            Console.WriteLine($"ConnectionString: {GetConnectionString()}");
            Console.WriteLine("=======================================\n");
        }
    }

    // 2. Giao diện Builder (Abstract Builder)
    public interface IDbConnectionBuilder
    {
        IDbConnectionBuilder SetServer(string server);
        IDbConnectionBuilder SetPort(int port);
        IDbConnectionBuilder SetDatabase(string database);
        IDbConnectionBuilder SetCredentials(string username, string password);
        IDbConnectionBuilder UseWindowsAuthentication();
        IDbConnectionBuilder SetTimeout(int seconds);
        DatabaseConnection Build();
    }

    // 3. Builder cụ thể (Concrete Builder)
    public class SqlServerConnectionBuilder : IDbConnectionBuilder
    {
        private DatabaseConnection _connection = new DatabaseConnection();

        public SqlServerConnectionBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            _connection = new DatabaseConnection
            {
                Server = "localhost",
                Port = 1433, // Cổng mặc định SQL Server
                Timeout = 30,
                IntegratedSecurity = false
            };
        }

        public IDbConnectionBuilder SetServer(string server)
        {
            _connection.Server = server;
            return this;
        }

        public IDbConnectionBuilder SetPort(int port)
        {
            _connection.Port = port;
            return this;
        }

        public IDbConnectionBuilder SetDatabase(string database)
        {
            _connection.Database = database;
            return this;
        }

        public IDbConnectionBuilder SetCredentials(string username, string password)
        {
            _connection.Username = username;
            _connection.Password = password;
            _connection.IntegratedSecurity = false;
            return this;
        }

        public IDbConnectionBuilder UseWindowsAuthentication()
        {
            _connection.IntegratedSecurity = true;
            _connection.Username = string.Empty;
            _connection.Password = string.Empty;
            return this;
        }

        public IDbConnectionBuilder SetTimeout(int seconds)
        {
            _connection.Timeout = seconds;
            return this;
        }

        public DatabaseConnection Build()
        {
            DatabaseConnection result = _connection;
            this.Reset(); // Khởi tạo lại để sẵn sàng xây dựng chuỗi kết nối khác
            return result;
        }
    }

    // 4. Lớp điều khiển dựng sẵn cấu hình mẫu (Director)
    public class DbDirector
    {
        private IDbConnectionBuilder _builder;

        public DbDirector(IDbConnectionBuilder builder)
        {
            _builder = builder;
        }

        // Tạo cấu hình kết nối Local cho môi trường Dev
        public DatabaseConnection BuildLocalDevConnection(string dbName)
        {
            return _builder.SetServer("localhost")
                           .SetDatabase(dbName)
                           .UseWindowsAuthentication()
                           .SetTimeout(15)
                           .Build();
        }
    }

    // 5. Chương trình chính
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IDbConnectionBuilder builder = new SqlServerConnectionBuilder();

            // --- Cách 1: Tự xây dựng cấu hình kết nối Production bằng Method Chaining ---
            DatabaseConnection prodDb = builder.SetServer("192.168.1.100")
                                               .SetPort(1433)
                                               .SetDatabase("QL_NhanSu")
                                               .SetCredentials("admin_db", "P@ssword2026")
                                               .SetTimeout(60)
                                               .Build();

            prodDb.DisplayInfo();

            // --- Cách 2: Sử dụng Director tạo kết nối Dev nhanh ---
            DbDirector director = new DbDirector(builder);
            DatabaseConnection devDb = director.BuildLocalDevConnection("QL_NhanSu_Test");

            devDb.DisplayInfo();

            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}