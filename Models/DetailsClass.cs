using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;





    public class DetailsClass
    {


        [JsonPropertyName("_id")]
        [Key]
        public int Id { get; set; }


    
        [JsonPropertyName("label")]
        public string? Label { get; set; } = "Not Available";


        [JsonPropertyName("input")]
        public string? Input { get; set; } = "Not Available";


        [JsonPropertyName("teamId")]
        public int? TeamId { get; set; } = null;

        [JsonPropertyName("team")]
        public virtual Team? Team { get; set; } = null;


        [JsonPropertyName("ticketId")]
        public int? TicketId { get; set; } = null;

        [JsonPropertyName("ticket")]
        public virtual Tickets? Ticket { get; set; } = null;
    }

 