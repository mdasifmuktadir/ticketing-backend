using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;



public class SubordinatesClass
{
   
    [JsonPropertyName("_id")]
    [Key]
    public int Id { get; set; }

 
    [JsonPropertyName("userId")]
    public int? UserId { get; set; } = null;

    [JsonPropertyName("user")]
    public virtual User? User { get; set; } = null;


    [JsonPropertyName("TeamId")]
    public int? TeamId { get; set; } = null;

    [JsonPropertyName("Team")]
    public virtual Team? Team { get; set; } = null;


 
    [JsonPropertyName("rank")]
    public int? Rank { get; set; } = 2;
}








