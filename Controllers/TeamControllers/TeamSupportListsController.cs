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
    public class TeamSupportListsController:Controller
    {

        private readonly ITeamsService _teamsService;
      
   

        public TeamSupportListsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

          [HttpPost]
        [Route("/getAllSupport")]
        public async Task<IActionResult> GetAllSupport(IFormCollection data)
        {
           

            var result = await _teamsService.GetAllSupport();
            return Ok(result);
        }

        [HttpPost]
        [Route("/getSupportFromHead")]
        public async Task<IActionResult> GetSupportFromHead(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);

            var result = await _teamsService.GetSupportFromHead(user);
            
            return Ok(result);
        }


         [HttpPost]
        [Route("/getSupportForDepartmentHead")]
        public async Task<IActionResult> GetSupportForDepartmentHead(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);

            var result = await _teamsService.GetSupportForDepartmentHead(user);
            
            return Ok(result);
        }
       

    }
}
