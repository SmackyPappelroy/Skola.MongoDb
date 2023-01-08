using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Reflection.Emit;

namespace Mongo.Common;

public class ShopItem : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("item")]
    public string Item { get; set; }

    [BsonElement("price")]
    public int Price { get; set; }
}