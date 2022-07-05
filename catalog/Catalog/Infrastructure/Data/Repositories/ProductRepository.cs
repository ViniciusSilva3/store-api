using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Utils;
using Catalog.Infrastructure.Data.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IProductRepository
    {
        
    }   
    public class ProductRepository : Repository<CatalogContext>, IProductRepository
    {
        public ProductRepository(CatalogContext context)
        :base(context)
        {
        }

        public CatalogContext CatalogContext
        {
            get { return _context as CatalogContext; }
        }
    }
}