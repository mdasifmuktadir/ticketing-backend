namespace Eapproval.Helpers.IHelpers;

using Eapproval.Models;



public interface IHelperClass{
   Task<List<User?>> GetSupport(Tickets ticket);
   Task<List<User>?> GetTicketingHeads(Tickets ticket);
   Task<ActionObject> GetAction(List<ActionObject>? Actions, User raisedBy, User? forwardedTo, string comment, ActionType action, string? info = "Not Available" , List<File2> file = null);
   Task<(User user, Tickets ticket, string comment, List<File2> fileNames, string info)> GetContent(IFormCollection data);
   Task<List<File2>> GetFiles(IFormCollection data);
   string GetCurrentTime();
    
}