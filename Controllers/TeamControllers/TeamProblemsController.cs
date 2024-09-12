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
    public class TeamProblemsController:Controller
    {

        private readonly ITeamsService _teamsService;
    
  
    

        public TeamProblemsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;        
        }

        
           [HttpPost]
        [Route("/getProblems")]
        public async Task<IActionResult> GetProblems(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var result = await _teamsService.GetProblemForUser(user);
            return Ok(result);

        }

    }
}
