using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;



public class User
{

  
    [JsonPropertyName("_id")]
    [Key]
    public int Id { get; set; }


    [JsonPropertyName("empName")]
    public string? EmpName { get; set; }



    [JsonPropertyName("empCode")]
    public string? EmpCode { get; set; }


    [JsonPropertyName("designation")]
    public string? Designation { get; set; }


    [JsonPropertyName("mailAddress")]
    public string? MailAddress { get; set; }

  
    [JsonPropertyName("unit")]
    public string? Unit { get; set; }


    [JsonPropertyName("section")]
    public string? Section { get; set; }


    [JsonPropertyName("wing")]
    public string? Wing { get; set; }

 
    [JsonPropertyName("team")]
    public string? Team { get; set; }

  
    [JsonPropertyName("groups")]
    public List<string>? Groups { get; set; }

   
    [JsonPropertyName("department")]
    public string? Department { get; set; }


    [JsonPropertyName("TeamType")]
    public string? TeamType { get; set; }

    
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    
   
    [JsonPropertyName("rank")]
    public int Rank { get; set; } = 2;

 
    [JsonPropertyName("userType")]
    public string? UserType { get; set; } = "normal";


    
   
    [JsonPropertyName("travelUserType")]
    public string? TravelUserType { get; set; } = "normal";

   
    [JsonPropertyName("available")]
    public bool? Available { get; set; } = true;


    [JsonPropertyName("rating")]
    public int? Rating { get; set; } = 0;


  
    [JsonPropertyName("raters")]
    public int? Raters { get; set; } = 0;


 
    [JsonPropertyName("extension")]
    public string? Extension { get; set; } = "Not Available";



   
    [JsonPropertyName("mobileNo")]
    public string? MobileNo { get; set; } = "Not Available";


   
    [JsonPropertyName("location")]
    public string? Location { get; set; } = "Not Available";


   
    [JsonPropertyName("numbers")]
    public int Numbers { get; set; } = 0;

    
    [JsonPropertyName("authored")]
    public virtual List<Blogs>? Authored { get; set; }


    [JsonPropertyName("TicketsAssigned")]
    public virtual List<Tickets>? TicketsAssigned { get; set; }


    [JsonPropertyName("TicketsRaised")]
    public virtual List<Tickets>? TicketsRaised { get; set; }


    [JsonPropertyName("TicketsCurrentlyHandled")]
    public virtual List<Tickets>? TicketsCurrentlyHandled { get; set; }


    [JsonPropertyName("TicketsHeaded")]
    public virtual List<Tickets>? TicketsHeaded { get; set; }


    [JsonPropertyName("TicketsPreviouslyHandled")]
    public virtual List<Tickets>? TicketsPreviouslyHandled { get; set; }
   



    [JsonPropertyName("conversations")]
    public virtual List<ConversationClass>? Conversations { get; set; }



    [JsonPropertyName("notes")]
    public virtual List<Notes>? Notes { get; set; }



    [JsonPropertyName("notificationTos")]
    public virtual List<Notification>? NotificationTos { get; set; }


    [JsonPropertyName("notificationFroms")]
    public virtual List<Notification>? NotificationFroms { get; set; }



    [JsonPropertyName("actionsRaised")]

    public virtual List<ActionObject>? ActionsRaised { get; set; }


    [JsonPropertyName("actionsReceived")]
    public virtual List<ActionObject>? ActionsReceived { get; set; }


    [JsonPropertyName("TeamMembers")]
    public virtual List<Team>? TeamMembers { get; set; }


    [JsonPropertyName("TeamsLeaded")]
    public virtual List<Team>? TeamsLeaded { get; set; }


    [JsonPropertyName("TeamsMonitored")]
    public virtual List<Team>? TeamsMonitored { get; set; }


    [JsonPropertyName("TeamsHeaded")]
    public virtual List<Team>? TeamsHeaded { get; set; }

}









