using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

namespace Mongo.Common;

public class ShopItem : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("item")]
    [JsonPropertyName("item")]
    public string Item { get; set; }

    [BsonElement("price")]
    [JsonPropertyName("price")]
    public int Price { get; set; }
}