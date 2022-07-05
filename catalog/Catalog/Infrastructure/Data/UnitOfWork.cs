using Catalog.Infrastructure.Data.Entities;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    int Complete();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogContext _context;
    public IProductRepository Products { get; private set;}
    public UnitOfWork(CatalogContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}