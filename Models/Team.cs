using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;


public class Team
{
  
    [JsonPropertyName("_id")]
    public int Id { get; set; } 


   


    [JsonPropertyName("hasServices")]
    public bool? HasServices { get; set; } 



    [JsonPropertyName("name")]
    public string? Name { get; set; } 


  
    [JsonPropertyName("leaders")]
    public virtual List<User>? Leaders { get; set; } = new List<User>();


   
    [JsonPropertyName("problemTypes")]
    public virtual List<ProblemTypesClass>? ProblemTypes { get; set; } = new List<ProblemTypesClass>();





   
    [JsonPropertyName("monitors")]
    public virtual List<User>? Monitors { get; set; } = new List<User>();


    
    [JsonPropertyName("headId")]
    public int? HeadId { get; set; } 

    [JsonPropertyName("head")]
    public virtual User? Head { get; set; } = null;





    [BsonElement("subordinates")]
    [JsonPropertyName("subordinates")]
    public virtual List<User>? Subordinates { get; set; } = new List<User>();



    [BsonElement("details")]
    [JsonPropertyName("details")]
    public virtual List<DetailsClass>? Details { get; set; } = new List<DetailsClass>();


}



