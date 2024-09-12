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
    public class TicketListsController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketsService;
    
 

  
       
     
        

        public TicketListsController( IHelperClass helperClass, ITicketsService ticketsService)
        {
            _helperClass = helperClass;
            _ticketsService = ticketsService; 
       
        
     
          
        }



        [HttpPost]
        [Route("getTickets")]
        public async Task<IActionResult> GetTickets(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var page = 2;

            var result = await _ticketsService.GetTicketsForHandler(user, page);

       
            return Ok(result);


        }



        [HttpPost]
        [Route("getTicketsForMonitors")]
        public async Task<IActionResult> GetTicketsForMonitors(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var page = int.Parse(data["page"]);

            var result = await _ticketsService.GetTicketsForMonitors(user, page);

       
            return Ok(result);


        }


        [HttpPost]
        [Route("getTickets2")]
        public async Task<IActionResult> GetTickets2(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            // var page = int.Parse(data["page"]);

            var result = await _ticketsService.GetTicketsForHandler2(user, 1);


            return Ok(result);


        }


      


        

        [HttpPost]
        [Route("getAllTickets")]
        public async Task<IActionResult> GetAllTickets(IFormCollection data)
        {

            var page = int.Parse(data["page"]);
            var result = await _ticketsService.GetAllTickets(page);

            return Ok(result);


        }

        [HttpPost]
        [Route("getDepartmentTickets")]
        public async Task<IActionResult> GetDepartmentTickets(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["totalUser"]);

            List<Tickets>? results;

            if(user.UserType == "Ticket Manager (Department)"){
                 
                 results = await _ticketsService.GetDepartmentHeadTickets(user);

            }else{

                 results = await _ticketsService.GetDepartmentTickets(data["user"]);
            }

            return Ok(results);
        }

        

        [HttpPost]
        [Route("getTicketsRaisedByUser")]
        public async Task<IActionResult> GetTicketsRaisedByUser(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var results = await _ticketsService.GetTicketsRaisedByUser(user);
            return Ok(results);
        }


        [HttpPost]
        [Route("getTicketsForLeader")]
        public async Task<IActionResult> GetTicketsForLeader(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var results = await _ticketsService.GetTicketsForLeader(user);
            return Ok(results);
           
        }

        [HttpPost]
        [Route("getTicketsForNormal")]
        public async Task<IActionResult> GetTicketsForNormal(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var results = await _ticketsService.GetTicketsForNormal(user);
            return Ok(results);

        }

        [HttpPost]
        [Route("getTicketsForSupport")]
        public async Task<IActionResult> GetTicketsForSupport(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var results = await _ticketsService.GetTicketsForSupport(user);
            return Ok(results);
        
        }



        
    




    }
}
