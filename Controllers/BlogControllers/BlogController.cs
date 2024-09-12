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
    public class BlogsController : Controller
   {
        private IBlogService _blogsService;
  
        public BlogsController(IBlogService blogsService, IJwtTokenService jwtTokenService)
        {
            _blogsService = blogsService;
        
        }



        [HttpPost]
        [Route("getBlog")]
        public async Task<IActionResult> GetBlog(IFormCollection data)
        {
            var id = data["id"]; 
            var result = await _blogsService.GetBlog(int.Parse(id));
            return Ok(result);
        }


        [HttpPost]
        [Route("createBlog")]
        public async Task<IActionResult> CreateBlog(IFormCollection data)
        {
            User user = JsonSerializer.Deserialize<User>(data["user"]);
            string content = data["content"];
            string headline = data["headline"];

            Blogs blog = new Blogs()
            {
                Author = user,
                Content = content,
                Headline = headline,
                AuthorId = user.Id
            };

       

            await _blogsService.InsertBlog(blog);
            return Ok(true);

        }


        [HttpPost]
        [Route("editBlog")]
        public async Task<IActionResult> EditBlog(IFormCollection data)
        {
         
            Blogs blog = JsonSerializer.Deserialize<Blogs>(data["blog"]);
            await _blogsService.EditBlog(blog);
            return Ok(blog);

        }


        [HttpPost]
        [Route("deleteArticle")]
        public async Task<IActionResult> DeleteBlog(IFormCollection data)
        {

            var blog = JsonSerializer.Deserialize<Blogs>(data["blog"]);



            await _blogsService.DeleteBlog(blog);
            return Ok(true);

        }



    }
}
