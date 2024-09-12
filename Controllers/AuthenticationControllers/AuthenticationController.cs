    using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using Eapproval.Helpers;
using Eapproval.services;
using Eapproval.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;

namespace Eapproval.Controllers.AuthenticationControllers
{
    [ApiController]
    [Route("/")]
    public class AuthenticationController : Controller
    {

        private readonly IUserApi _userApi;
        private readonly IUsersService _usersService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IHasher _hasher;

        public AuthenticationController(IUserApi userApi, IHasher hasher, IUsersService usersService, IJwtTokenService jwtTokenService)
        {
                _userApi = userApi;
             _usersService = usersService;
            _jwtTokenService = jwtTokenService;
            _hasher = hasher;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(IFormCollection data)
        {
            var result = await _usersService.GetUserByMail(data["MailAddress"]);

                if(result != null)
            {
                dynamic newData = new
                {
                    registered = false,
                    message = "This email is already registered"
                };

                      return new JsonResult(newData);
            }
            else
            {
                // var user = JsonSerializer.Deserialize<User>(data["user"]) ;
                var user = new User();
                user.MailAddress = data["MailAddress"];
                

                user.UserType = "normal";
                user.Password = _hasher.HashPassword(data["Password"]);
                user.UserType = data["UserType"];
              
                await _usersService.CreateAsync(user);
                return Ok(true);

            };

            
        }


       
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginData data)
        {
            var email = data.Email;
            var password = _hasher.HashPassword(data.Password);

      

            var result =await  _usersService.GetUserByMailAndPassword(email, password);

            if(result != null)
            {
                var token = _jwtTokenService.GenerateToken(result);
                

                dynamic newData = new
                {
                    result = result,
                    token = token,
                    success = true,

                };

                return new JsonResult(newData);
            }
            else
            {
                dynamic newData = new { success = false, message = "This user is not authorized" };
                return new JsonResult(newData);
            }
        }




    }
}
