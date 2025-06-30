using BikeRentalSystem_Final.DataAccess;
using BikeRentalSystem_Final.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Dapper;

namespace BikeRentalSystem_Final.Views
{
    public partial class LoginView : Window
    {
        private readonly DbConnectionFactory _factory = new DbConnectionFactory("Server=(localdb)\\MSSQLLocalDB;Database=BikeRentalDB;Trusted_Connection=True;");

        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using var connection = _factory.Create();
            var user = connection.QuerySingleOrDefault<User>(
                "SELECT * FROM Users WHERE Username = @Username",
                new { Username = UsernameBox.Text });

            if (user != null && user.PasswordHash == HashPassword(PasswordBox.Password))
            {
                var mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}