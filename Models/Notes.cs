using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models
{
    public class Notes
    {
        [Key]
        [JsonPropertyName("_id")]
        public int Id { get; set; } 


        
        [JsonPropertyName("ticketId")]
        public int? TicketId { get; set; }


        [JsonPropertyName("ticket")]
        public virtual Tickets? Ticket { get; set; }


 
        [JsonPropertyName("data")]
        public string? Data { get; set; }

        
        // [JsonPropertyName("takenById")]
        // public int? TakenById { get; set; }


        [JsonPropertyName("takenBy")]
        public string? TakenBy { get; set; }


        [BsonElement("date")]
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [BsonElement("type")]
        [JsonPropertyName("type")]
        public string? Type { get; set; } = "text";


        [BsonElement("files")]
        [JsonPropertyName("files")]
        public virtual List<File2>? Files { get; set; } = null;

        [BsonElement("caption")]
        [JsonPropertyName("caption")]
        public string? Caption { get; set; }


        [BsonElement("mentions")]
        [JsonPropertyName("mentions")]
        public virtual List<string>? Mentions { get;set; } = null; 
    }



    

}


