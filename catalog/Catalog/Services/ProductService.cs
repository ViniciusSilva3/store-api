using Catalog.Domain;
using Catalog.Infrastructure.Data;
using Catalog.Domain.Utils;
using Infrastructure.Data.Repositories;
using Catalog.Infrastructure.Data.UnitOfWork;

namespace Catalog.Services;

public interface IProductService
{
    Task<Result<Product>> GetProductById(string id);
    Task<Result> SaveProduct(ProductModel product);
    Result<IEnumerable<Product>> GetProducts();
    Task<Result> UpdateProduct(string id, double? price, double? weight);
}

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Product>> GetProductById(string id)
    {
        return await _unitOfWork.Product.Get(id);
    }

    public Result<IEnumerable<Product>> GetProducts()
    {
        return _unitOfWork.Product.GetAll();
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
            Weight = product.Weight,
            Price = product.Price
        };

        Result res = await _unitOfWork.Product.Add(newProduct);
        if (res.IsSuccess)
        {
            _unitOfWork.Complete();
        }

        return res;
    }

    public async Task<Result> UpdateProduct(string id, double? price = null, double? weight = null)
    {
        Result<Product> product = await _unitOfWork.Product.Get(id);

        if (product.IsNotSuccess)
        {
            return Result.Fail("404");
        }

        if (price != null)
            product.Value.Price = price.Value;
        if (weight != null)
            product.Value.Weight = weight.Value;
        
        Result res = await _unitOfWork.Product.Add(product.Value);
        if (res.IsSuccess)
        {
            _unitOfWork.Complete();
        }
        return res;
    }
}
