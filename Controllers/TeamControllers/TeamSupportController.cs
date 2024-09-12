using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Eapproval.Models;
using Eapproval.services;
using ZstdSharp.Unsafe;
using Eapproval.Helpers;
using System.Xml.Serialization;
using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Authorization;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.TeamControllers
{

    [ApiController]
    [Route("/")]
    public class TeamSupportController:Controller
    {

  
        private readonly IHelperClass _helperClass;
     
        

        public TeamSupportController(IHelperClass helperClass)
        {
        
            _helperClass = helperClass;
    
        
        }
     

        [HttpPost]
        [Route("/getSupport")]
        public async Task<IActionResult> GetSupport(IFormCollection data)
        {
            var ticket = JsonSerializer.Deserialize<Tickets>(data["ticket"]);

            var result = await _helperClass.GetSupport(ticket);
            
            return Ok(result);
        }

         
       

    }
}
