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
    public class TicketController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketsService;
        private readonly ITicketMailer _ticketMailer;
    
        private readonly INotifier _notifier;
      
        private readonly ITeamsService _teamsService;
       
     
        

        public TicketController(ITeamsService teamsService, IHelperClass helperClass, ITicketsService ticketsService, ITicketMailer ticketMailer, INotifier notifier)
        {
            _helperClass = helperClass;
            _ticketsService = ticketsService; 
            _ticketMailer = ticketMailer;
            _notifier = notifier;
            _teamsService = teamsService;
        }




        [HttpPost]
        [Route("submitTicket")]
        public async Task<IActionResult> SubmitTicket(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var ticket = result.ticket;
            var comment = result.comment;
            var user = result.user;
            var fileNames = result.fileNames;
            ticket.Actions = new List<ActionObject>();
            ticket.RaisedBy = user;
            ticket.MadeCloseRequest = false;
            string message;


            ticket.PrevHandler = user;
            EventType mailEvent;
            
            var ticketingHeads = await _helperClass.GetTicketingHeads(ticket);
            var thisTicketHead = ticketingHeads.FirstOrDefault(x => x.Location == ticket.Location);
            ticket.TicketingHead = thisTicketHead;
            ticket.Files = fileNames;

            if (ticket.Type == "service")
            {
                ticket.CurrentHandler = null;
                 mailEvent = EventType.SeekSupervisorApproval;
                ticket.ApprovalRequired = true;
                ticket.Status = "Ticket Submitted - Seeking Department Head's Approval";
                message = $"{user.EmpName} is asking for you approval to raise a service ticket for {ticket.Department} ";
              
            }
            else
            {
                ticket.CurrentHandler = null;
                mailEvent = EventType.SeekHigherAuthorityApproval;
                ticket.ApprovalRequired = false;
                ticket.Status = "Ticket Submitted";
                message = $"{user.EmpName} has raised a ticket for {ticket.Department} ";

            }

            ticket.RequestDate = _helperClass.GetCurrentTime();



            var action = await  _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.TicketRaised, file:fileNames); 
            
            ticket.Actions.Add(action);


            var subordinates = await _teamsService.GetConcernedUsers(ticket.Department);
              
        

            if(ticket.Genesis == false){
                var genesisTicket = await _ticketsService.GetAsync(ticket.GenesisId);
                genesisTicket.TimesRaised++;
                ticket.TimesRaised = genesisTicket.TimesRaised;
                await _ticketsService.UpdateAsync(genesisTicket.Id, genesisTicket);
            };


            ticket.InitialLocation = ticket.Location;
            ticket.InitialPriority = ticket.Priority;
            ticket.InitialType = ticket.TicketType;
            

            await _ticketsService.CreateAsync(ticket);
             
             ticket.Users = new List<string>();


            foreach (var subordinate in subordinates)
            {
                await _notifier.InsertNotification(action.Time, message, user, subordinate, ticket.Id);
                ticket.Users.Add(subordinate.MailAddress);

            }

            await _ticketsService.UpdateAsync(ticket.Id, ticket);


         
             var members = await _teamsService.GetTeamByName(ticket.Department);




            //  foreach(var x in members.Subordinates){
            //        _ticketMailer.SendMail(user, x.User, ticket.Department, "new ticket", ticket.Id, user);

            //  }

       

           


       


            



            return Ok(true);
          


        }

        
        [HttpPost]
        [Route("reOpen")]
        public async Task<IActionResult> ReOpen(IFormCollection data)
        {
           var result = await _helperClass.GetContent(data);
           var ticket = result.ticket;
           var comment = result.comment;
           var user = result.user;
           var fileNames = result.fileNames;;

           ticket.CurrentHandler = null;
           ticket.CurrentHandlerId = null;
           ticket.Status = "Ticket Submitted";
           ticket.MadeCloseRequest = false;
          


            EventType mailEvent;

           var message = $"{user.EmpName} has opened a ticket for the {ticket.Department} ";

         

           var action = await  _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.ReOpen, file:fileNames); 
           
           ticket.Actions.Add(action);

           ticket.AssignedTo = null;
           ticket.Genesis = false;
           ticket.GenesisId = ticket.Id;
        //    ticket.Id = null;
           var genesisTicket = await _ticketsService.GetAsync(ticket.GenesisId);
           genesisTicket.TimesRaised++;
           ticket.TimesRaised = genesisTicket.TimesRaised;

         

           var oldTicket = await _ticketsService.GetOldTicket(user, ticket.GenesisId);

           if(oldTicket != null){
            return Ok("exists");
           }else{

           

           await _ticketsService.UpdateAsync(genesisTicket.Id, genesisTicket);
           await _ticketsService.CreateAsync(ticket);

             var subordinates = await _teamsService.GetConcernedUsers(ticket.Department);
                 foreach (var subordinate in subordinates)
            {
                await _notifier.InsertNotification(action.Time, message, user, subordinate, ticket.Id);
              

            }


            
             var members = await _teamsService.GetTeamByName(ticket.Department);




            //  foreach(var x in members.Subordinates){
            //        _ticketMailer.SendMail(user, x.User, ticket.Department, "new ticket", ticket.Id, user);

            //  }

        



            return Ok(ticket.Id);
          
           }

        }



        [HttpPost]
        [Route("closeRequest")]
        public async Task<IActionResult> CloseRequest(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = result.ticket;
            var comment = result.comment;
            var filenames = result.fileNames;
            var info = result.info;

            ticket.PrevHandler = ticket.CurrentHandler;
            ticket.CurrentHandler = ticket.RaisedBy;
            ticket.MadeCloseRequest = true;

            ticket.Status = "Close Requested";

            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.CloseRequest, file:filenames, info:info);
            ticket?.Actions?.Add(action);
            await _ticketsService.UpdateAsync(ticket?.Id, ticket!);

            var message = $"{user.EmpName} has requested you to close the request you raised for {ticket?.Department} ";

            await _notifier?.InsertNotification(action.Time!, message, user, ticket?.CurrentHandler!, ticket?.Id);


            // _ticketMailer.SendMail(user, ticket.CurrentHandler, ticket.Department, "closed", ticket.Id, user);
            return Ok(true);
        }


        [HttpPost]
        [Route("closeTicket")]
        public async Task<IActionResult> CloseTicket(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = await _ticketsService.GetAsync(result.ticket.Id);
            var comment = result.comment;
            var filenames = result.fileNames;
            var rating = data["rating"];
            

            ticket.PrevHandler = ticket.CurrentHandler;
            ticket.PrevHandlerId = ticket.CurrentHandler.Id;
          

            ticket.Status = "Closed Ticket";
            ticket.MadeCloseRequest = false;

     
           
            var message = $"{user.EmpName} has closed the ticket you raised for the {ticket.Department} ";

            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.TicketClosed, file:filenames);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);
            await _notifier.InsertNotification(action.Time, message, user, ticket.RaisedBy, ticket.Id);
            // _ticketMailer.SendMail(user, ticket.RaisedBy, ticket.Department, "closed", ticket.Id, user);
            return Ok(true);
        }

        [HttpPost]
        [Route("closeRequestReject")]
        public async Task<IActionResult> CloseRequestReject(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = result.ticket;
            var comment = result.comment;
            var info = result.info;
            

            ticket.PrevHandler = ticket.CurrentHandler;
            ticket.CurrentHandler = ticket.AssignedTo;

            ticket.Status = "Open";
            ticket.MadeCloseRequest = false;

            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.CloseRequestReject, info:info);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);

            var message = $"{user.EmpName} has rejected your close request for the ticket he raised for {ticket.Department} ";

            await _notifier.InsertNotification(action.Time, message, user, ticket.CurrentHandler, ticket.Id);

          
            return Ok(true);
        }


        [HttpPost]
        [Route("getTicket")]
        public async Task<IActionResult> GetTicket(IFormCollection data)
        {
            var id = data["id"];

            var result = await _ticketsService.GetAsync(int.Parse(id));

            return Ok(result);


        }
 
        [HttpPost]
        [Route("updateTicket")]
        public async Task<IActionResult> UpdateTicket(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var ticket = JsonSerializer.Deserialize<Tickets>(data["ticket"]);
            ticket.RaisedById = ticket.RaisedBy.Id;
            await _ticketsService.UpdateAsync(ticket.Id, ticket);
           
            return Ok(ticket);
        
        
        }


             [HttpPost]
        [Route("resetTicket")]
        public async Task<IActionResult> ResetTicket(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var ticketId = int.Parse(data["id"]);

            var ticket  = await _ticketsService.GetAsync(ticketId);

            ticket!.Priority = ticket.InitialPriority;
            ticket.Location = ticket.InitialLocation;
            ticket.TicketType = ticket.InitialType;
            ticket.CurrentHandler = null;
            ticket.AssignedTo = null;
            ticket.CurrentHandlerId = null;
            ticket.AssignedToId = null;

            var team = await _teamsService.GetTeamByName(ticket.Department!);

            var ticketingHead = team.Leaders!.Where( x => x.Location == ticket.Location).FirstOrDefault();
            var supportUsers = team.Subordinates!.Where( x => x.Location == ticket.Location).Select(x => x.MailAddress).ToList();
   
            ticket.TicketingHead = ticketingHead;
            ticket.Users = supportUsers!;

            await _ticketsService.UpdateAsync(ticket.Id, ticket);
           
            return Ok(ticket);
        
        
        }

        


   





    }
}
