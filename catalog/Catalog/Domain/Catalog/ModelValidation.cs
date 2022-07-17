namespace Catalog.Domain;

public class ModelValidation
{
    public bool isValid { get; set; }
    public IList<string> errorMessage = new List<string>();
}