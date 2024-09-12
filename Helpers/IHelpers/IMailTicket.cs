namespace Eapproval.Helpers.IHelpers;

using MimeKit;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;

using Eapproval.Models;



public interface IMailTicket{

     Task GetNewTickets();
    
}