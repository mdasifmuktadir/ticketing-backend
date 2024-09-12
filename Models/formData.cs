using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace Eapproval.Models;

public class TicketData
{

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("ticket")]
    [BsonElement("ticket")]
    public Tickets? Ticket { get; set; }

    [JsonPropertyName("Team")]
    public Team? Team { get; set; }


    [JsonPropertyName("token")]
    public string? Token { get; set; }


}


public class TeamData
    {

    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("Team")]
    public Team? Team { get; set; }


    }



public class ApproverData : TicketData
{
    [JsonPropertyName("approver")]
    public Team? Approver { get; set; }


}

public class AdditionalInfoData : TicketData
{
    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; set; }
}


public class TokenData
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }
}

public class RegisterData
{
    [JsonPropertyName("mailAddress")]
    public string? MailAddress { get; set; }


    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("Team")]
    public Team? Team { get; set; }
}



public class LoginData
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }


    [JsonPropertyName("password")]
    public string? Password { get; set; }

}
