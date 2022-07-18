namespace Shipping.API.Entities;
using Microsoft.AspNetCore.Mvc;

public class CartModal
{
    public List<Product> ProductList { get; set; }
}
public class Product
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}