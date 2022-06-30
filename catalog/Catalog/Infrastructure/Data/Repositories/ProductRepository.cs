using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Utils;

namespace Infrastructure.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(string Id);
        Task<Result> SaveProduct(Product product);
    }   
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<Product> GetById(string Id)
        {
            return await _db.Product.SingleAsync(product => product.Id == Id);
        }

        public async Task<Result> SaveProduct(Product product)
        {
            try
            {
                _db.Product.Add(product);
                await _db.SaveChangesAsync();
                return Result.Ok();
            }
            catch(Exception)
            {
                return Result.Fail("Could not save product");
            }         
        }
    }
}