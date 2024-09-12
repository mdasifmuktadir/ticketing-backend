using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models
{

    
   
  


     public class Chat
    {
      
        [JsonPropertyName("_id")]
        [Key]
        public int? Id { get; set; }


        [JsonPropertyName("ticketId")]
        public int? TicketId { get; set; }

        
        [JsonPropertyName("ticket")]
        public virtual Tickets? Ticket { get; set; }


   
        [JsonPropertyName("conversation")]
        public virtual List<ConversationClass> Conversation { get; set; } = new List<ConversationClass>();


        [BsonElement("connectionHolder")]
        [JsonPropertyName("connectionHoler")]
        public virtual List<ConnectionHolderClass> ConnectionHolders { get; set; } = new List<ConnectionHolderClass>();


  }


  
}
