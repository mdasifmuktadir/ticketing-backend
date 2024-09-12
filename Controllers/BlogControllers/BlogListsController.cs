using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eapproval.Services;
using Eapproval.services;
using Eapproval.Models;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;

using System.Text.Json;
using MongoDB.Driver;

namespace Eapproval.Controllers.BlogControllers
{
    [Route("/")]
    [ApiController]
    public class BlogListsController : Controller
   {
        private IBlogService _blogsService;
        private IJwtTokenService _jwtTokenService;
        public BlogListsController(IBlogService blogsService, IJwtTokenService jwtTokenService)
        {
            _blogsService = blogsService;
            _jwtTokenService = jwtTokenService;
        
        }




        [HttpPost]
        [Route("getAllBlogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var result = await _blogsService.GetAllBlogs();
            return Ok(result);
        }



        [HttpPost]
        [Route("getFilteredBlogs")]
        public async Task<IActionResult> GetFilteredBlogs(IFormCollection data)
        {

            
            List<Blogs> result;
            if (data["search"] == "" || data["search"] == " ")
            {
                result = await _blogsService.GetAllBlogs();
            }
            else
            {
                result = await _blogsService.GetFilteredBlogs(data["search"]);
            }


            return Ok(result);
        }


        [HttpPost]
        [Route("getBlogsForUser")]
        public async Task<IActionResult> GetBlogsForUser(IFormCollection data)
        {
            var user = _jwtTokenService.ParseToken(data["token"]);

            var result = await _blogsService.GetBlogsForUser(user);
            return Ok(result);
        }


        [HttpPost]
        [Route("getFilteredBlogsForUser")]
        public async Task<IActionResult> GetFilteredBlogsForUser(IFormCollection data)
        {

            var user = _jwtTokenService.ParseToken(data["token"]);
            List<Blogs> result;
            if (data["search"] == "" || data["search"] == " ")
            {
                result = await _blogsService.GetBlogsForUser(user);
            }
            else
            {
                result = await _blogsService.GetFilteredBlogsForUser(data["search"], user);
            }
            

            return Ok(result);
        }




    }
}
