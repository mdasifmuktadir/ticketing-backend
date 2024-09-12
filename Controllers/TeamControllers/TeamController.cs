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
    public class TeamController:Controller
    {

        private readonly ITeamsService _teamsService;
      
      


        public TeamController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
     
        }


        [HttpPost]
        [Route("/createTeam")]
        public async Task<IActionResult> CreateTeam(IFormCollection data)
        {
            var team = JsonSerializer.Deserialize<Team>(data["team"]);
            await _teamsService.CreateTeam(team);
            return Ok(true);
        }

     


        [HttpPost]
        [Route("/getTeam")]
        public async Task<IActionResult> GetTeam(IFormCollection data)
        {
            var id = data["id"];
            var result = await _teamsService.GetTeamById(int.Parse(id));
            return Ok(result);
        }





        [HttpPost]
        [Route("/editTeam")]
        public async Task<IActionResult> EditTeam(IFormCollection data)
        {
            var team = JsonSerializer.Deserialize<Team>(data["team"]);


           

             await _teamsService.UpdateTeam(team.Id, team);
            return Ok(true);
        }




        [HttpPost]
        [Route("/deleteTeam")]
        public async Task<IActionResult> DeleteTeam(IFormCollection data)
        {
            var team = JsonSerializer.Deserialize<Team>(data["team"]);
            
           await _teamsService.RemoveTeam(team);
            return Ok(team.Id);
        }


        
        [HttpPost]
        [Route("/updateRanks")]
        public async Task<IActionResult> UpdateRanks(IFormCollection data)
        {
            var team = JsonSerializer.Deserialize<Team>(data["team"]);

            await _teamsService.UpdateTeam(team.Id, team);

            return Ok(team);            

    


   

        }



      






    


    




   








    }
}
