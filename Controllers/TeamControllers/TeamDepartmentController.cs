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
    public class TeamDepartmentController:Controller
    {

        private readonly ITeamsService _teamsService;
        private readonly ITicketsService _ticketsService;


        public TeamDepartmentController(ITicketsService ticketsService, ITeamsService teamsService)
        {
            _teamsService = teamsService;
            _ticketsService = ticketsService;
        }

       
       
       [HttpPost]
        [Route("reassignDepartment")]
        public async Task<IActionResult> ReassignDepartment(IFormCollection data)
        {   
            var ticket = JsonSerializer.Deserialize<Tickets>(data["ticket"]);
            var department = data["department"];
            var team = await _teamsService.GetTeamByName(department);
            var ticketingHead = team.Leaders.Where(x => x.Location == ticket.Location).FirstOrDefault();
            ticket.TicketingHead = ticketingHead;
            ticket.Department = department;  
            await _ticketsService.UpdateAsync(ticket.Id, ticket);
            return Ok(department);

        }




      






    


    




   








    }
}
