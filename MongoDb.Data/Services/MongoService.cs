using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Data.Services;

public class MongoService : IMongoService
{
    private readonly IMongoDatabase _db;

    public MongoService(IMongoDatabase database)
    {
        _db = database;
    }

    public async Task<List<T>> GetAsync<T>(string collectionName) where T : class
    {
        var collection = _db.GetCollection<T>(collectionName);
        var result = await collection.FindAsync(Builders<T>.Filter.Empty);
        return result.ToList();
    }

    public async Task<bool> DeleteAsync<T>(string id, string collectionName) where T : class
    {
        var collection = _db.GetCollection<T>(collectionName);
        var result = await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        return result.IsAcknowledged;
    }


    public async Task<List<T>> GetSingleAsync<T>(string id, string collectionName) where T : class
    {
        var collection = _db.GetCollection<T>(collectionName);
        var result = collection.Find(Builders<T>.Filter.Eq("Id", id));
        return await result.ToListAsync();
    }

    public async Task InsertAsync<T>(string id, T entity, string collectionName) where T : class
    {
        var collection = _db.GetCollection<T>(collectionName);
        await collection.InsertOneAsync(entity);
    }

    public async Task<bool> UpdateAsync<T>(string id, T entity, string CollectionName) where T : class
    {
        var collection = _db.GetCollection<T>(CollectionName);
        var result = await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }


}
