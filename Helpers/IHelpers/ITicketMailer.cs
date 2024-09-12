namespace Eapproval.Helpers.IHelpers;

using Eapproval.Models;



public interface ITicketMailer{
   Task SendMail(User from, User to, string department, string _event, string id, User raiser);
   Task  SendPdfToUsers(string pdfFilePath, List<User> users);
    
}