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
    public class TicketLocationController : Controller
    {


        private readonly ITicketsService _ticketsService;
     
     
    
        private readonly ITeamsService _teamsService;
       
     
        

        public TicketLocationController(ITeamsService teamsService,  ITicketsService ticketsService)
        {
       
            _ticketsService = ticketsService; 
            _teamsService = teamsService;
        }



        [HttpPost]
        [Route("setLocation")]
        public async Task<IActionResult> SetLocation(IFormCollection data)
        {
            var results = await _ticketsService.GetAsync(int.Parse(data["id"]));
            results.Location = data["location"];
            results.Assigned = false;
            results.AssignedTo = null;
            results.AssignedToId = null;
            results.CurrentHandler = null;
            results.CurrentHandlerId = null;


            var team = await _teamsService.GetTeamByName(results.Department);

            var ticketingHead = team.Leaders.Where( x => x.Location == results.Location).FirstOrDefault();
            var supportUsers = team.Subordinates.Where( x => x.Location == results.Location).Select(x => x.MailAddress).ToList();
   
            results.TicketingHead = ticketingHead;
            results.Users = supportUsers;
            await _ticketsService.UpdateAsync(int.Parse(data["id"]), results);
          

            return Ok(results);
        }



        
     




     
    



       




    }
}
