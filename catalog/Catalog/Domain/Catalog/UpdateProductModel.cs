namespace Catalog.Domain;

public class UpdateProductModel
{
    public double? Weight { get; set; }
    public double? Price { get; set; }

    public ModelValidation validation
    {
        get
        {
            ModelValidation response = new ModelValidation();
            
            if (Weight == null || Price == null)
            {
                response.errorMessage.Add("Values cannot be null.");
                response.isValid = false;
                return response;
            }
            if (Weight < 0)
            {
                response.errorMessage.Add("Weight cannot be a negative value.");
                response.isValid = false;
            }

            if (Price < 0)
            {
                response.errorMessage.Add("Price cannot be a negative value.");
                response.isValid = false;
            }
            
            return response;
        }
    }
}