using Microsoft.AspNetCore.Mvc;
using Eapproval.Models;
using Eapproval.services;

using System.Text.Json;
using Eapproval.Helpers;
using Eapproval.Services;
using IronPdf;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.UserControllers;

    [Route("/")]
    [ApiController]
    public class UserApiController: Controller
    {
    
        private readonly IUserApi _usersApi;
  

        public UserApiController(IUserApi userApi)
        {
         
            _usersApi = userApi;
         
        }



        [HttpGet]
        [Route("/api/getUsers")]
        public async Task<IActionResult> ApiUsers()
        {
            var results = await _usersApi.GetTeams();

            return Ok(results);

        }


      



      




        
   


}
