using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;
  
  
  public class ConnectionHolderClass
    {
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }

       
        [JsonPropertyName("_id")]
        [Key]
        public int? Id { get; set; }
       
        [JsonPropertyName("_connectionId")]
        public string? ConnectionId {get; set;}

        [JsonPropertyName("chatId")]
        public int? ChatId { get; set; }


        [JsonPropertyName("chat")]
        public virtual Chat? Chat { get; set; }
    }
