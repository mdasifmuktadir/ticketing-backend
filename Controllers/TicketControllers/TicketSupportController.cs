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
    public class TicketSupportController : Controller
    {

        private readonly IHelperClass _helperClass;
        private readonly TicketSupportService _ticketsSupportService;
    
 

  
       
     
        

        public TicketSupportController( IHelperClass helperClass, TicketSupportService ticketsService)
        {
            _helperClass = helperClass;
            _ticketsSupportService = ticketsService; 
          
        }


    //   [HttpPost]
    //   [Route("getAllSupportTickets")]
    //   public async Task<IActionResult> GetAllSupportTickets(IFormCollection data){

    //      var user = JsonSerializer.Deserialize<User>(data["user"]);
    //      var page = int.Parse(data["page"]);

    //     var result = await _ticketsSupportService.GetAllTickets(user, page);


    //     return Ok(result);
           
    //   }


    //  public async Task<IActionResult> GetUnassignedTickets(IFormCollection data){
    //          var user = JsonSerializer.Deserialize<User>(data["user"]);
    //      var page = int.Parse(data["page"]);

    //     var result = await _ticketsSupportService.GetAllTickets(user, page);


    //     return Ok(result);
    //   }


    public async Task<IActionResult> TicketsAssignedToMe(){
        return Ok(true);

    }


    public async Task<IActionResult> InformationRequestedByMe(){
        return Ok(true);
    }
        



      


        
    




    }
}
