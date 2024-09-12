using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models
{
    public class Location
    {
        [Key]
        [JsonPropertyName("_id")]
        public int? Id { get; set; }


        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
