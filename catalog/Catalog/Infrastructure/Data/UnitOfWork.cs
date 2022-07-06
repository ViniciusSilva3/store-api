using Catalog.Infrastructure.Data.Entities;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Product { get; }
    int Complete();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogContext _context;
    public IProductRepository Product { get; private set;}
    public UnitOfWork(CatalogContext context, IProductRepository product)
    {
        _context = context;
        Product = product;
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