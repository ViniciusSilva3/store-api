namespace Shipping.API.Entities;
using Microsoft.AspNetCore.Mvc;

public class CartModal
{
    public List<CartProduct> ProductList { get; set; }
}
public class CartProduct
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}