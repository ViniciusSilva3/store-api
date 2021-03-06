using Shipping.Clients;
using Shipping.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<CatalogClient>(c => c.BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings.Get("catalogClientUrl")));
builder.Services.AddSingleton<ICatalogClient, CatalogClient>();
builder.Services.AddSingleton<IShippingService, ShippingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
