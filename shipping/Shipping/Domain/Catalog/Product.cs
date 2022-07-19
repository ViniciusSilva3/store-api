namespace Shipping.Domain;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }
    public DateTime CreationDate { get; set; }
}