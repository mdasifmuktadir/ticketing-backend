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
    public class TicketRateController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketsService;

        private readonly IUsersService _usersService;
     
       
     
        

        public TicketRateController(IUsersService usersService, IHelperClass helperClass, ITicketsService ticketsService)
        {
            _helperClass = helperClass;
            _ticketsService = ticketsService; 
            _usersService = usersService;
       
        }






        [HttpPost]
        [Route("rate")]
        public async Task<IActionResult> Rate(IFormCollection data)
        {
            var result = await _helperClass.GetContent(data);
            var user = result.user;
            var ticket = await _ticketsService.GetAsync(result.ticket.Id);
            var comment = result.comment;
            var rating = data["rating"];
            

            ticket.PrevHandler = ticket.CurrentHandler;
            ticket.PrevHandlerId = ticket.CurrentHandler.Id;
            ticket.CurrentHandler = null;
            ticket.CurrentHandlerId = null;
        

            ticket.Status = "Closed Ticket";
            ticket.MadeCloseRequest = false;

            if (ticket.AssignedTo != null)
            {
                var handler = await _usersService.GetOneUser(ticket.AssignedTo.Id);
                var prevRating = handler.Rating;
                var prevRaters = handler.Raters;
                var newRating = int.Parse(rating);
                var newRaters = prevRaters + 1;
                var currentAvgRating = ((prevRaters * prevRating) + newRating) / newRaters;
                handler.Rating = currentAvgRating;
                handler.Raters = newRaters;

                _usersService.UpdateAsync(handler.Id, handler);

            }

           
            

            var action = await _helperClass.GetAction(ticket.Actions, user, ticket.CurrentHandler, comment, ActionType.Rated);
            ticket.Actions.Add(action);
            await _ticketsService.UpdateAsync(ticket.Id, ticket);
          
            return Ok(true);
        }




    }
}


