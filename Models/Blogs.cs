using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Security.Cryptography.Xml;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;

public class Blogs
{
   
    [JsonPropertyName("_id")]
    [Key]
    public int Id { get; set; }


    [JsonPropertyName("authorId")]
    public int? AuthorId { get; set; } = null;


    [JsonPropertyName("author")]
    public virtual User? Author { get; set; } = null;


  
    [JsonPropertyName("content")]
    public string? Content { get; set; } = null;


   
    [JsonPropertyName("headline")]
    public string? Headline { get; set; } = null;


}
