using Eapproval.Helpers;
using Microsoft.AspNetCore.Mvc;
using Eapproval.Models;
using Eapproval.services;
using System.Text.Json;
using MongoDB.Bson;
using System.Runtime.CompilerServices;
using MongoDB.Driver.Core.Authentication;
using Org.BouncyCastle.Ocsp;
using System.IO;
using MongoDB.Driver.Core.Operations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using Eapproval.Services;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Eapproval.Models.VMs;
using Eapproval.Mappings;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.TicketControllers
{
    [ApiController]
    [Route("/")]
    public class ApprovalController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketsService;
        private readonly ITicketMailer _ticketMailer;
        private readonly INotifier _notifier;
     
     
       
     
        

        public ApprovalController(IHelperClass helperClass, ITicketsService ticketsService, ITicketMailer ticketMailer, INotifier notifier)
        {
            _helperClass = helperClass;
            _ticketsService = ticketsService; 
            _ticketMailer = ticketMailer; 
            _notifier = notifier;
       
        }



        [HttpPost]
        [Route("supervisorApprove")]
        public async Task<IActionResult> SupervisorApprove(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = result.ticket;
            var comment = result.comment;
            var files = result.fileNames;

            ticket.PrevHandler = user;
            ticket.CurrentHandler = ticket.TicketingHead;
            ticket.Files = files;
            ticket.ApprovalRequired = false;

            ticket.Status = "Ticket Submitted - Department Head's Approval Given";
            ticket.MadeCloseRequest = false;

            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.SupervisorApprove);

            ticket.Actions.Add(action);

            await _ticketsService.UpdateAsync(ticket.Id, ticket);


            var message = $"{user.EmpName} has approved a ticket to be raised for {ticket.Department}";

            await _notifier.InsertNotification(action.Time, message, user, ticket.CurrentHandler, ticket.Id);

            // _ticketMailer.SendMail(user, ticket.CurrentHandler, ticket.Department, "approved", ticket.Id, user);

            
            
            
            return Ok(true);

        }


        [HttpPost]
        [Route("askApproval")]
        public async Task<IActionResult> SeekHigherApproval(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = await _ticketsService.GetAsync(result.ticket.Id);
            var comment = result.comment;
            var approver = JsonSerializer.Deserialize<User>(data["approver"]);
            
            ticket.PrevHandler = user;
            ticket.PrevHandlerId = user.Id;
            ticket.CurrentHandler = approver;
            ticket.CurrentHandlerId = approver.Id;
            ticket.MadeCloseRequest = false;
            ticket.Status = "Ticket Submitted - Seeking Additional Approval";
            ticket.ApprovalRequired = true;
          
            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.SeekingHigherApproval);
            ticket.Actions.Add(action);

            await _ticketsService.UpdateAsync(ticket.Id, ticket);
           
            var message = $"{user.EmpName} is seeking your approval to deal with a ticket raised for {ticket.Department}";

            _notifier.InsertNotification(action.Time, message, user, ticket.CurrentHandler, ticket.Id);


            // _ticketMailer.SendMail(user, ticket.CurrentHandler, ticket.Department, "seeking approval", ticket.Id, user);

            return Ok(true);

        }


        [HttpPost]
        [Route("higherApprove")]
        public async Task<IActionResult> HigherApprove(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = await _ticketsService.GetAsync(result.ticket.Id);
            var comment = result.comment;
            var fileNames = result.fileNames;

            ticket.Status = "Ticket Submitted - Additional Approval Given";
            ticket.CurrentHandler = ticket.PrevHandler;
            ticket.CurrentHandlerId = ticket.PrevHandler.Id;
            ticket.PrevHandler = user;
            ticket.PrevHandlerId = user.Id;

            ticket.ApprovalRequired = false;
          
          
            ticket.MadeCloseRequest = false;
            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.HigherApprove, file:fileNames);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);
          

            var message = $"{user.EmpName} has approved the ticket raised for {ticket.Department}";

            _notifier.InsertNotification(action.Time, message, user, ticket.CurrentHandler, ticket.Id);


            // _ticketMailer.SendMail(user, ticket.CurrentHandler, ticket.Department, "approved", ticket.Id, user);
            return Ok(true);

            

        }



        [HttpPost]
        [Route("rejectTicket")]
        public async Task<IActionResult> RejectTicket(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = result.ticket;
            var comment = result.comment;
            


            ticket.PrevHandler = ticket.CurrentHandler;
            ticket.CurrentHandler = null;
            ticket.CurrentHandlerId = null;

            ticket.Status = "Rejected";

            ticket.MadeCloseRequest = false;

            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.RaisedBy, comment, ActionType.Reject);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);


            var message = $"{user.EmpName} has rejected the ticket you raised for {ticket.Department}";

            _notifier.InsertNotification(action.Time, message, user, ticket.RaisedBy, ticket.Id);
            // _ticketMailer.SendMail(user, ticket.RaisedBy, ticket.Department, "rejected", ticket.Id, user);
            return Ok(true);
        }

   
}

}
