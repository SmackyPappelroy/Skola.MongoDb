namespace Mongo.Data.Services
{
    public interface IMongoService
    {
        Task<bool> DeleteAsync<T>(string id, string collectionName) where T : class;
        Task<List<T>> GetAsync<T>(string collectionName) where T : class;
        Task<List<T>> GetSingleAsync<T>(string id, string collectionName) where T : class;
        Task InsertAsync<T>(string id, T entity, string collectionName) where T : class;
        Task<bool> UpdateAsync<T>(string id, T entity, string CollectionName) where T : class;
    }
}