using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Eapproval.Models;


 public class ActionObject
    {
        
        [JsonPropertyName("_id")]
        public int? Id { get; set; }


        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [BsonElement("serial")]
        [JsonPropertyName("serial")]
        public int? Serial { get; set; } = 0;




   
        [JsonPropertyName("type")]
       
        public ActionType? Type { get; set; }


        [JsonPropertyName("raisedById")]
        public int? RaisedById { get; set; }


        [JsonPropertyName("raisedBy")]
        public virtual User? RaisedBy { get; set; }


        [JsonPropertyName("forwardedToId")]
        public int? ForwardedToId { get; set; }

        
        [JsonPropertyName("forwardedTo")]
        public virtual User? ForwardedTo { get; set; }

    
        [JsonPropertyName("comments")]
        public string? Comments { get; set; }




   
        [JsonPropertyName("additionalInfo")]
        public string? AdditionalInfo { get; set; }


        [JsonPropertyName("files")]
        public virtual List<File2?>? Files { get; set; }


        [JsonPropertyName("ticketId")]
        public int? TicketId { get; set; }

        [JsonPropertyName("ticket")]
        public virtual Tickets? Ticket { get; set; }
    }
