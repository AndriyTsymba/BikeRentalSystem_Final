namespace BikeRentalSystem_Final.Models
{
    public class Bicycle
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public decimal PricePerHour { get; set; }
    }
}