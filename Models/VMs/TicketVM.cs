using System.Text.Json.Serialization;
using Eapproval.Models;

namespace Eapproval.Models.VMs;



public class TicketVM{

    [JsonPropertyName("_id")]
    public int Id {get; set;}
    public string? RequestDate {get; set;}
    public string? Location {get; set;}
    public string? Status {get; set;}
    public int? Number {get; set;}
    public string? Department {get; set;}
    public List<string>? Users {get; set;}
    public User? Requester {get; set;}
    public User? AssignedTo {get; set;}
    public User? CurrentHandler {get; set;}
    public User? RaisedBy {get; set;}
    public int? GenesisId {get; set;}
    public string? ProblemDetails {get; set;}
       
    [JsonPropertyName("ticketType")]
    public string? TicketType { get; set; } = "Incident";
       
    [JsonPropertyName("category")]
    public string Category { get; set; } = "Not Available";

    [JsonPropertyName("actions")]
    public List<ActionObject>? Actions {get; set;}

    [JsonPropertyName("priority")]
    public PriorityClass? Priority { get; set; } = new PriorityClass();

    
    [JsonPropertyName("hasService")]
    public bool? HasService { get; set; } = false;

    



}