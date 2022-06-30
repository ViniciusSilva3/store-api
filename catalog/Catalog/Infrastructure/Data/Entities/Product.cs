using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Product { get; set; }
}