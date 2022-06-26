namespace Catalog.Db;

public interface IPersister
{
    void ApplyChanges();
    void ReadFileContent();
}

public class Persister : IPersister
{
    public Persister()
    {
    }

    public void ReadFileContent()
    {
        throw new NotImplementedException();
    }

    public void ApplyChanges()
    {
        throw new NotImplementedException();
    }
}
