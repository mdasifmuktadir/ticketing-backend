using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Eapproval.Models
{
    public class Counter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("_id")]
        public string? Id { get; set; }


        [BsonElement("count")]
        [JsonPropertyName("count")]
        public int Count { get; set; } = 0;
    }
}
