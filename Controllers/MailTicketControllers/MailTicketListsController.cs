using Microsoft.AspNetCore.Mvc;
using Eapproval.Models;
using Eapproval.services;

using System.Text.Json;
using Eapproval.Helpers;
using Eapproval.Services;
using IronPdf;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers;

    [Route("/")]
    [ApiController]
    public class MailTicketListsController : Controller
    {
    
         private readonly IMailTicket  _mailTicket;

        public MailTicketListsController(IMailTicket mailTicket)
        {  
            _mailTicket = mailTicket;
        }


        [HttpGet]
        [Route("/getMailTickets")]
        public async Task<IActionResult> getMailTickets(IFormCollection data){
            _mailTicket.GetNewTickets();
            return Ok(true);   
        }


}
