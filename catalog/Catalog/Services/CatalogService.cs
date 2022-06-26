using Catalog.Db;
namespace Catalog.Services;

public interface ICatalogService
{
    void GetProduct();
}

public class CatalogService : ICatalogService
{  
    private readonly IPersister _persister;

    public CatalogService(IPersister persister)
    {
        _persister = persister;
    }
    
    public void GetProduct()
    {
        throw new NotImplementedException();
    }
}
