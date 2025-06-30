using BikeRentalSystem_Final.DataAccess;
using BikeRentalSystem_Final.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BikeRentalSystem_Final
{
    public partial class MainWindow : Window
    {
        private readonly User _currentUser;
        private readonly DbConnectionFactory _factory = new DbConnectionFactory("Server=(localdb)\\MSSQLLocalDB;Database=BikeRentalDB;Trusted_Connection=True;");

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadData();
            AddBtn.Visibility = _currentUser.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoadData()
        {
            using var connection = _factory.Create();
            var bicycles = connection.Query<Bicycle>("SELECT * FROM Bicycles").ToList();
            BicycleGrid.ItemsSource = bicycles;
        }

        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            if (BicycleGrid.SelectedItem is Bicycle bike && bike.Status == "Available")
            {
                using var connection = _factory.Create();
                connection.Execute("UPDATE Bicycles SET Status = 'Rented', UserId = @UserId WHERE Id = @Id", new { Id = bike.Id, UserId = _currentUser.Id });
                LoadData();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if (BicycleGrid.SelectedItem is Bicycle bike && bike.UserId == _currentUser.Id)
            {
                using var connection = _factory.Create();
                connection.Execute("UPDATE Bicycles SET Status = 'Available', UserId = NULL WHERE Id = @Id", new { Id = bike.Id });
                LoadData();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using var connection = _factory.Create();
            connection.Execute("INSERT INTO Bicycles (Name, Brand, Model, Status, PricePerHour) VALUES ('New Bike', 'BrandX', 'ModelY', 'Available', 5.0)");
            LoadData();
        }
    }
}