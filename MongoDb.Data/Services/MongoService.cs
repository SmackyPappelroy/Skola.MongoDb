using Microsoft.Extensions.Options;
using Mongo.Data.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Data.Services;

public class MongoService : IMongoService
{
    private readonly IMongoClient client;
    private readonly IMongoDatabase database;
    private readonly IOptions<ShopItemDatabaseSettings> settings;

    public MongoService(IOptions<ShopItemDatabaseSettings> shopItemSettings)
    {
        this.client = new MongoClient(shopItemSettings.Value.ConnectionString);
        settings = shopItemSettings;
        database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public async Task<List<T>> GetAsync<T>(string collectionName) where T : class
    {
        var collection = database.GetCollection<T>(settings.Value.ShopItemCollectionName);
        var result = await collection.FindAsync(Builders<T>.Filter.Empty);
        return result.ToList();
    }

    public async Task<bool> DeleteAsync<T>(string id, string collectionName) where T : class
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        var collection = database.GetCollection<T>(settings.Value.ShopItemCollectionName);
        var result = await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        return result.IsAcknowledged;
    }


    public async Task<List<T>> GetSingleAsync<T>(string id, string collectionName) where T : class
    {
        var collection = database.GetCollection<T>(collectionName);
        var result = collection.Find(Builders<T>.Filter.Eq("Id", id));
        return await result.ToListAsync();
    }

    public async Task InsertAsync<T>(string id, T entity, string collectionName) where T : class
    {
        var collection = database.GetCollection<T>(collectionName);
        await collection.InsertOneAsync(entity);
    }

    public async Task<bool> UpdateAsync<T>(string id, T entity, string CollectionName) where T : class
    {
        var collection = database.GetCollection<T>(CollectionName);
        var result = await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }


}
