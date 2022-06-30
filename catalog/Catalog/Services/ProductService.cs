using Catalog.Domain;
using Catalog.Infrastructure.Data;
using Catalog.Domain.Utils;
using Infrastructure.Data.Repositories;

namespace Catalog.Services;

public interface IProductService
{
    void GetProduct();
    Task<Result> SaveProduct(ProductModel product);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public void GetProduct()
    {
        throw new NotImplementedException();
    }

    public async Task<Result> SaveProduct(ProductModel product)
    {
        string id = Guid.NewGuid().ToString();
        DateTime creationDate = DateTime.UtcNow;

        Product newProduct = new Product()
        {
            Id = id,
            CreationDate = creationDate,
            Name = product.Name,
            Weight = product.Weight
        };
        
        return await _productRepository.SaveProduct(newProduct);    
    }
}
