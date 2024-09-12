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
    public class TeamListsController:Controller
    {

        private readonly ITeamsService _teamsService;

      
        private readonly ITicketsService _ticketsService;

        public TeamListsController(ITicketsService ticketsService, ITeamsService teamsService)
        {
            _teamsService = teamsService;
            _ticketsService = ticketsService;
        }


        [HttpPost]
        [Route("/getTeams")]
        public async Task<IActionResult> GetTeams(IFormCollection data)
        {
            
            var result = await _teamsService.GetAllTeams();
            return Ok(result);
        }

         [HttpPost]
        [Route("/getTeamsForLeaders")]
        public async Task<IActionResult> GetTeamsForLeaders(IFormCollection data)
        {
            var email = data["user"];
            var user = JsonSerializer.Deserialize<User>(data["totalUser"]);

            List<Team>? result;
            if(user.UserType == "Ticket Manager (Department)"){
                   result = await _teamsService.GetTeamsForMonitors(user);
            }else{

             result = await _teamsService.GetTeamsForHead(email);
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("/getDepartmentsAndTickets")]
        public async Task<IActionResult> GetDepartmentsAndTickets(IFormCollection data)
        {   
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var email = user.MailAddress;
            
            List<Team> teams;

            if(user.UserType == "Ticket Manager (Department)"){
                teams = await _teamsService.GetTeamsForDepartmentHead(email);
            }else{
               teams = await _teamsService.GetTeamsForHead(email);
            }

            List<string> departments = new List<string>();
            foreach(var t in teams){
                departments.Add(t.Name);
            };

            var tickets = await _ticketsService.GetTicketsByDepartment(departments);

            var result = new {
                departments = teams,
                tickets = tickets,
            };
            
            return Ok(result);
        }


    }
}
