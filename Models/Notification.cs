using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Eapproval.Models
{
    public class Notification
    {

       
        [JsonPropertyName("_id")]
        [Key]
        public int? Id { get; set; }
     
        [JsonPropertyName("time")]
        public string? Time { get; set; }

     
        [JsonPropertyName("message")]
        public string? Message { get; set; }
   
        [JsonPropertyName("ticketId")]
        public int? TicketId { get; set; }

        [JsonPropertyName("fromId")]
        public int? FromId { get; set; }

        [JsonPropertyName("from")]
        public virtual User? From {get; set;}
      
        [JsonPropertyName("toId")]
        public int? ToId { get; set; } = null; 
        
        [JsonPropertyName("type")]
        public string? Type { get; set; } = "normal";


        [BsonElement("mentions")]
        [JsonPropertyName("mentions")]
        public  List<string>? Mentions { get; set; } = new List<string>();



    }





  
}
