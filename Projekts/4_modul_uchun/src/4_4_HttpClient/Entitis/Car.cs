namespace _4_4_HttpClient.Entitis
{
    public class Car
    {
        public Guid CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string FuelType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
