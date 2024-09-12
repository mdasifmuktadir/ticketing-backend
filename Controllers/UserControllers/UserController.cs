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
    public class UserController : Controller
    {
        private readonly IUsersService _usersService;


        public UserController(IUsersService usersService)
        {
        
            _usersService = usersService;
      
        }


        [HttpPost]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            await _usersService.RemoveAsync(user.Id);
            return Ok(true);

        }


        [HttpPost]
        [Route("updateUserNormal")]
        public async Task<IActionResult> updateUserNormal(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            await _usersService.UpdateAsync(user.Id, user);

            return Ok(true);

        }





}
