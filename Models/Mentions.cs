using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Eapproval.Models;


public class Mentions{
    [JsonPropertyName("id")]
    [Key]
    public int Id { get; set; }


    [JsonPropertyName("name")]
    public string? Name { get; set; }


    [JsonPropertyName("notesId")]
    public int? NoteId { get; set; }

    [JsonPropertyName("notes")]
    public virtual Notes? Note { get; set; }


    [JsonPropertyName("notificationId")]
    public int? NotificationId { get; set; }

    [JsonPropertyName("notification")]
    public virtual Notification? Notification { get; set; }
}