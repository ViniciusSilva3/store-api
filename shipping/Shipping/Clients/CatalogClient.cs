using Shipping.Domain;
using Shipping.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace Shipping.Clients;

public interface ICatalogClient
{
    Task<Result<Product>> GetProductById(string id);
}
public class CatalogClient : ICatalogClient
{
    private readonly HttpClient _httpClient;
    public CatalogClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<Product>> GetProductById(string id)
    {
        string path = $"catalog/{id}";
        Product product = null;
        HttpResponseMessage response = await _httpClient.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            product = await response.Content.ReadFromJsonAsync<Product>();
        }
        if (product != null)
        {
            return Result<Product>.Ok(product);
        }

        return Result.Fail<Product>("Could not retrieve product.");
    }
}