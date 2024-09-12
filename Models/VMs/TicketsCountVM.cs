using System.Text.Json.Serialization;
using Eapproval.Models;

namespace Eapproval.Models.VMs;



public class TicketsCountVM{
    
    [JsonPropertyName("tickets")]
    public List<TicketVM> Tickets {get; set;} 

    [JsonPropertyName("count")]
    public int Count {get; set;} 

}