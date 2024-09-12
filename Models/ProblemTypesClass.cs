using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;



public class ProblemTypesClass{


    [JsonPropertyName("_id")]
    [Key]
    public int Id { get; set; }



    [JsonPropertyName("name")]
    public string Name { get; set; } = "Not Given";


 
    [JsonPropertyName("subs")]
    public List<string> Subs { get; set; } = new List<string>();


    [JsonPropertyName("teamId")]
    public int? TeamId { get; set; } = null;

    [JsonPropertyName("team")]
    public virtual Team? Team { get; set; } = null;


}








