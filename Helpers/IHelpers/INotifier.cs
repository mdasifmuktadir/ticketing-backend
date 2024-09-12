namespace Eapproval.Helpers.IHelpers;

using Eapproval.Models;



public interface INotifier{

    Task InsertNotification(string time, string message, User from, User to, int? ticketId, List<string> mentions = null, string type = "message", string section = "ticketing");
}