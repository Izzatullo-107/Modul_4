namespace _4_4_HttpClient.DTOs;

public class GetDto
{
    public Guid CarId { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Country { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public string FuelType { get; set; }
}
