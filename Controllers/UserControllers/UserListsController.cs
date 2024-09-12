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
    public class UserListsController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IUserApi _usersApi;


        public UserListsController(IUsersService usersService, IUserApi userApi)
        {
        
            _usersService = usersService;
            _usersApi = userApi;
       
        }


        [HttpPost]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersService.GetAllNormalUsers();
            return Ok(users);
            
        }


        [HttpPost]
        [Route("getUsersIncludingAdmin")]
        public async Task<IActionResult> GetUsersIncludingAdmin()
        {
            var users = await _usersService.GetUsersIncludingAdmin();
            return Ok(users);

        }


        // [HttpGet]
        // [Route("/api/getUsers")]
        // public async Task<IActionResult> ApiUsers()
        // {
        //     var results = await _usersApi.GetTeams();

        //     return Ok(results);

        // }


      



      




        
   


}
