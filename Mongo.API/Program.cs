using Mongo.Common;
using Mongo.Data.Configuration;
using Mongo.Data.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


CreateMongoConnection(builder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

void CreateMongoConnection(WebApplicationBuilder builder)
{
    try
    {
        builder.Services.Configure<ShopItemDatabaseSettings>(builder.Configuration.GetSection("WebShopDatabase"));
        builder.Services.AddSingleton<IMongoService, MongoService>();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Felmeddelande: " + ex);
    }

}
