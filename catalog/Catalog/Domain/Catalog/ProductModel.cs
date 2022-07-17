namespace Catalog.Domain;

public class ProductModel
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }

    public ModelValidation validation
    {
        get
        {
            ModelValidation response = new ModelValidation();
            if (String.IsNullOrEmpty(Name))
            {
                response.errorMessage.Add("Product name cannot be empty.");
                response.isValid = false;
            }
            
            if (Weight < 0 || Price < 0)
            {
                response.errorMessage.Add("Weight or Price cannot be a negative value.");
                response.isValid = false;
            }
            
            return response;
        }
    }
}