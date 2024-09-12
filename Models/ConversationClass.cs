using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models
{

    


    public class ConversationClass
    {

        [JsonPropertyName("_id")]
        [Key]
        public int? Id { get; set; }


        [JsonPropertyName("fromId")]
        public int? FromId { get; set; }


        [JsonPropertyName("from")]
        public virtual User? From { get; set; }

      
        [JsonPropertyName("message")]
        public string? Message { get; set; }


    
        [JsonPropertyName("files")]
        public virtual List<File2>? Files { get; set; } = new List<File2>();

   
        [JsonPropertyName("time")]
        public string? Time { get; set; } = null;

        
        [JsonPropertyName("type")]
        public string? Type { get; set; } = "text";

   
        [JsonPropertyName("chatId")]
        public int? ChatId { get; set; }


        [JsonPropertyName("chat")]
        public virtual Chat? Chat { get; set; }
       

    }
   
  
}
