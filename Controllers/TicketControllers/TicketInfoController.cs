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
    public class TicketInfoController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketsService;
        private readonly ITicketMailer _ticketMailer;

        private readonly INotifier _notifier;
     
     
       
     
        

        public TicketInfoController( IHelperClass helperClass, ITicketsService ticketsService, ITicketMailer ticketMailer, INotifier notifier)
        {
            _helperClass = helperClass;
            _ticketsService = ticketsService; 
            _ticketMailer = ticketMailer;
      
            _notifier = notifier;
         
        }



        [HttpPost]
        [Route("askInfo")]
        public async Task<IActionResult> AskInfo(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var comment = data["comment"];
            var ticketFront = JsonSerializer.Deserialize<Tickets>(data["ticket"]);
            var ticket = await _ticketsService.GetAsync(ticketFront.Id);
            var informer = JsonSerializer.Deserialize<User>(data["approver"]);
         



            ticket.PrevHandler = user;
            ticket.PrevHandlerId = user.Id;
            ticket.CurrentHandler = informer;
            ticket.CurrentHandlerId = informer.Id;


            ticket.Ask = true;
            ticket.MadeCloseRequest = false;
            ticket.Status = "Open (Seeking Information...)";
            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.AskInfo);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);

            var message = $"{user.EmpName} is asking for more information regarding a ticket raised for {ticket.Department} by {ticket.RaisedBy.EmpName}";

            await _notifier.InsertNotification(action.Time, message, user, ticket.CurrentHandler, ticket.Id);

            // _ticketMailer.SendMail(user, ticket.CurrentHandler, ticket.Department, "seeking information", ticket.Id, user);
            return Ok(true);


        }


        [HttpPost]
        [Route("giveInfo")]
        public async Task<IActionResult> GiveInfo(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var ticket = await _ticketsService.GetAsync(result.ticket.Id);
            var comment = result.comment;
            var user = result.user;
            var filenames = result.fileNames;
            var info = result.info;

            ticket.Ask = false;

            ticket.Status = "Open (Information Sent)";
            ticket.CurrentHandler = ticket.PrevHandler;
            ticket.CurrentHandlerId = ticket.PrevHandler.Id;
            ticket.PrevHandler = user;
            ticket.PrevHandlerId = user.Id;
            ticket.MadeCloseRequest = false;
            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.GiveInfo, file:filenames, info: info);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);

            var message = $"{user.EmpName} has given you more information regarding the ticket raised for {ticket.Department} ";

            await _notifier.InsertNotification(action.Time, message, user, ticket.CurrentHandler, ticket.Id);

            // _ticketMailer.SendMail(user, ticket.CurrentHandler, ticket.Department, "given information", ticket.Id, user);
            return Ok(true);

        }






    }
}
