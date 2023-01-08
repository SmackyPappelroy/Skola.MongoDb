using Mongo.Common;
using Mongo.Data.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


CreateMongoConnection(builder.Services);

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

void CreateMongoConnection(IServiceCollection services)
{
    try
    {
        var connectionString = "mongodb+srv://hmartin:Borodino123469!@cluster0.xk2rh80.mongodb.net/?retryWrites=true&w=majority";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("MyTest");

        services.AddSingleton(database);
        services.AddSingleton<IMongoService, MongoService>();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Felmeddelande: " + ex);
    }

}
