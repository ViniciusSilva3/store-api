using Catalog.Domain;
using Catalog.Infrastructure.Data;
using Catalog.Domain.Utils;
using Infrastructure.Data.Repositories;
using Catalog.Infrastructure.Data.UnitOfWork;

namespace Catalog.Services;

public interface IProductService
{
    void GetProduct();
    Task<Result> SaveProduct(ProductModel product);
}

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}
