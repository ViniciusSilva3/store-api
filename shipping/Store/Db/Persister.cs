using Store.Models;
namespace Store.Db;

public interface IPersister
{
    void ApplyChanges(FileOperation operation);
    FileContent ReadFileContent(string fileName);
}

public class Persister : IPersister
{
    public Persister()
    {
    }

    public FileContent ReadFileContent(string fileName)
    {
        return new FileContent(File.ReadAllLines(fileName), fileName);
    }

    public void ApplyChanges(FileOperation operation)
    {
        switch (operation.Type)
        {
            case OperationType.WRITE:
                File.WriteAllLines(operation.FileName, operation.Content);
                return;
            case OperationType.UPDATE:
                File.WriteAllLines(operation.FileName, operation.Content);
                return;
            default:
                throw new InvalidOperationException();
        }
    }
}
