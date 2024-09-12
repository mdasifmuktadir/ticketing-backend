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
    public class TicketDepartmentController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketsService;
   
   
        private readonly INotifier _notifier;

        private readonly ITeamsService _teamsService;
       
     
        

        public TicketDepartmentController(ITeamsService teamsService,  IHelperClass helperClass, ITicketsService ticketsService, INotifier notifier)
        {
            _helperClass = helperClass;
            _ticketsService = ticketsService; 
            _notifier = notifier;
            _teamsService = teamsService;
        }


        [HttpPost]
        [Route("setDepartment")]
        public async Task<IActionResult> SetDepartment(IFormCollection data)
        {
            var results = await _ticketsService.GetAsync(int.Parse(data["id"]));
            
            results.Department = data["department"];
            results.Assigned = false;
            results.AssignedTo = null;
            results.CurrentHandler = null;

            var user = results.RaisedBy;

            var team = await _teamsService.GetTeamByName(results.Department);


            if(results.Location != null){

            var ticketingHead = team.Leaders.Where( x => x.Location == results.Location).FirstOrDefault();
            results.TicketingHead = ticketingHead;
            
            }


            var action = await _helperClass.GetAction(results.Actions, user, results.CurrentHandler, "Not Available", ActionType.TicketRaised, null);


           
            var message = $"A new ticket has been assigned to your department";

            var subordinates = await _teamsService.GetConcernedUsers(results.Department);

               foreach (var subordinate in subordinates)
            {
                _notifier.InsertNotification(action.Time, message, user, subordinate, results.Id);
                results.Users.Add(subordinate.MailAddress);

            }

          

   
            await _ticketsService.UpdateAsync(int.Parse(data["id"]), results);
            return Ok(results.Location);
        }



   



    }
}


