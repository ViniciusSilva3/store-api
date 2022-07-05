using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data.Entities
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
