namespace Catalog.Domain;

public class Product : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }

    // add price
    public double Price { get; set; }
}