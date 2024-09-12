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
    public class TeamUserController:Controller
    {

     

        private readonly IUsersService _usersService;

    


        public TeamUserController(IUsersService usersService)
        {
            _usersService = usersService;
       
        }

       
       
        [HttpPost]
        [Route("/demoteTemporaryLeader")]
        public async Task<IActionResult> DemoteTemporaryLeader(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            user.UserType = "support";

             await _usersService.UpdateAsync(user.Id, user);
            return Ok(true);
        }



      






    


    




   








    }
}
