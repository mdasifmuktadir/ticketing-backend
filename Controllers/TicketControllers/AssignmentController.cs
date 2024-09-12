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
    public class AssignmentController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketsService;
        private readonly ITicketMailer _ticketMailer;
    
        private readonly INotifier _notifier;
        private readonly IUsersService _usersService;
     
       
     
        

        public AssignmentController( IUsersService usersService, IHelperClass helperClass, ITicketsService ticketsService, ITicketMailer ticketMailer, INotifier notifier)
        {
            _helperClass = helperClass;
            _ticketsService = ticketsService; 
            _ticketMailer = ticketMailer;
            _notifier = notifier;
            _usersService = usersService;
           
        }



        [HttpPost]
        [Route("assignSelf")]
        public async Task<IActionResult> AssignSelf(IFormCollection data)
        {

            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var id = int.Parse(data["ticketId"]!);
            var ticket = await _ticketsService.GetAsync(id);
            var comment = data["comment"];


            await _usersService.UpdateUserNumber(user);
            ticket.Assigned = true;
            ticket.AssignedTo = user;
            ticket.AssignedToId = user.Id;
            ticket.PrevHandler = ticket.CurrentHandler;
            ticket.PrevHandlerId = ticket.CurrentHandler?.Id;
            ticket.CurrentHandler = ticket.AssignedTo;
            ticket.CurrentHandlerId = ticket.AssignedTo.Id;
            ticket.Accepted = true;
            ticket.Status = "Assigned";
            ticket.MadeCloseRequest = false;
            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.AssignSelf);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);
          
            return Ok(ticket);


        }


        [HttpPost]
        [Route("assign")]
        public async Task<IActionResult> AssignOther(IFormCollection data)
        {       

            var id = data["ticketId"];
            var ticket = await _ticketsService.GetAsync(int.Parse(id!));
            
            var approver = JsonSerializer.Deserialize<User>(data["approver"]);
            var comment = data["comment"];
            var user = JsonSerializer.Deserialize<User>(data["user"]);

            await _usersService.UpdateUserNumber(approver!);
            ticket!.PrevHandler = user;
            ticket.PrevHandlerId = user?.Id;
            ticket.CurrentHandler = approver;
            ticket.CurrentHandlerId = approver?.Id;
            ticket.Assigned = true;
            ticket.AssignedTo = approver;
            ticket.AssignedToId = approver?.Id;
            ticket.MadeCloseRequest = false;
            ticket.Status = "Assigned";
            ticket.Accepted = false;
            ticket.Location = ticket.AssignedTo?.Location;
       
            var action = await _helperClass.GetAction(ticket.Actions, user!, ticket.CurrentHandler, comment!, ActionType.AssignOther);
            ticket.Actions?.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);


            var message = $"{user?.EmpName} has assigned you the ticket raised for {ticket.Department} by {ticket.RaisedBy?.EmpName}";

            _notifier?.InsertNotification(action.Time!, message, user!, ticket.CurrentHandler!, ticket.Id);


            return Ok(true);

        }

        [HttpPost]
        [Route("unassign")]
        public async Task<IActionResult> Unassign(IFormCollection data)
        {

            var id = data["ticketId"];
            var ticket = await _ticketsService.GetAsync(int.Parse(id));

            var prevassignee = JsonSerializer.Deserialize<User>(data["prevAssignee"]);
            var comment = data["comment"];
            var user = JsonSerializer.Deserialize<User>(data["user"]);

           
            ticket!.PrevHandler = ticket.CurrentHandler;
            ticket.PrevHandlerId = ticket.CurrentHandler!.Id;
            ticket.CurrentHandlerId = null;
            ticket.CurrentHandler = null;
            ticket.AssignedToId = null;
            ticket.Assigned = false;
            ticket.AssignedTo = null;
            ticket.AssignedToId = null;
            ticket.MadeCloseRequest = false;
            ticket.Status = "Ticket Submitted";
            ticket.Accepted = false;
            
        
            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.Unassigned);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);


            var message = $"{user.EmpName} has unassigned a ticket from you which was raised for {ticket.Department} by {ticket.RaisedBy.EmpName}";

            _notifier.InsertNotification(action.Time, message, user, prevassignee, ticket.Id);

            //  _ticketMailer.SendMail(user, ticket.PrevHandler, ticket.Department, "unassigned", ticket.Id, user);
            return Ok(true);



        }


        [HttpPost]
        [Route("reassign")]
        public async Task<IActionResult> Reassign(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var id = data["ticketId"];
            var ticket = await _ticketsService.GetAsync(int.Parse(id));
            var approver = JsonSerializer.Deserialize<User>(data["approver"]);
            var comment = data["comment"];

            ticket.PrevHandler = ticket.CurrentHandler;
            ticket.CurrentHandler = approver;
            ticket.Assigned = true;
            ticket.AssignedTo = approver;
            ticket.MadeCloseRequest = false;
            ticket.Status = "Assigned";
            ticket.Location = ticket.AssignedTo.Location;
            ticket.Accepted = false;
            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.AssignOther);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);

            var message = $"{user.EmpName} has assigned you the ticket raised for {ticket.Department} by {ticket.RaisedBy.EmpName}";

            _notifier.InsertNotification(action.Time, message, user, ticket.CurrentHandler, ticket.Id);

            // _ticketMailer.SendMail(user, ticket.CurrentHandler, ticket.Department, "assigned", ticket.Id, user);
            return Ok(true);


        }





    }
}
