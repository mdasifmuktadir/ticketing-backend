using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Eapproval.Models; 
 
 
 
 public class File2
    {
       
        [JsonPropertyName("_id")]
        public int? Id { get; set; }


        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }

    
        [JsonPropertyName("originalName")]
        public string? OriginalName { get; set; }


        [JsonPropertyName("conversationId")]
        public int? ConversationId { get; set; }

        [JsonPropertyName("conversation")]
        public virtual ConversationClass? Conversation { get; set; }


        [JsonPropertyName("noteId")]
        public int? NoteId { get; set; }

        [JsonPropertyName("note")]
        public virtual Notes? Note { get; set; }


        [JsonPropertyName("actionId")]
        public int? ActionId { get; set; }


        [JsonPropertyName("action")]
        public virtual ActionObject? Action { get; set; }


        [JsonPropertyName("ticketId")]
        public int? TicketId { get; set; }

        [JsonPropertyName("ticket")]
        public virtual Tickets? Ticket { get; set; }
    }


