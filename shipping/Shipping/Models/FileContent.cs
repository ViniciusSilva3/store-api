namespace Store.Models;

public class FileOperation
{
    public string[] Content;
    public readonly string FileName;
    public readonly OperationType Type;
    public FileOperation(string[] content, string fileName, OperationType op)
    {
        Content = content;
        FileName = fileName;
        Type = op;
    }
}

public enum OperationType
{
    WRITE,
    UPDATE,
    GET,
    DELETE
}
public struct FileContent
{
    public string[] Content;
    public readonly string FileName;
    public FileContent(string[] content, string fileName)
    {
        Content = content;
        FileName = fileName;
    }
}