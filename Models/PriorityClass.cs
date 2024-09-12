using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;


public class PriorityClass
{
    [JsonPropertyName("_id")]
    [Key]
    public int? Id { get; set; }
    

    [JsonPropertyName("priority")]
    public string? Priority { get; set; } = "Priority 1";


    [JsonPropertyName("responseTime")]
    public TimeSpan? ResponseTime { get; set; } = TimeSpan.FromMinutes(15);



    [JsonPropertyName("resolutionTime")]
    public TimeSpan? ResolutionTime { get; set; } = TimeSpan.FromMinutes(44);








}