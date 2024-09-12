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
    public class TicketTypeController : Controller
    {

    
        private readonly ITicketsService _ticketsService;
   
       
     
        

        public TicketTypeController( ITicketsService ticketsService)
        {
       
            _ticketsService = ticketsService; 
         
        }


        [HttpPost]
        [Route("setTicketType")]
        public async Task<IActionResult> SetTicketType(IFormCollection data)
        {
            var results = await _ticketsService.GetAsync(int.Parse(data["id"]));
            results.TicketType = data["ticketType"];
           
            await _ticketsService.UpdateAsync(int.Parse(data["id"]), results);
            return Ok(results.TicketType);
        }




    }
}


