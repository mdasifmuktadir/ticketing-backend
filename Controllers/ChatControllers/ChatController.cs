using Eapproval.Helpers;
using Eapproval.Models;
using Eapproval.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.ChatControllers
{
    [Route("/")]
    [ApiController]
    public class ChatController : Controller
    {

        IChatService _chatService;
        IFileHandler _fileHandler;

        public ChatController(IChatService chatService, IFileHandler fileHandler) 
        {
            _chatService = chatService;
            _fileHandler = fileHandler;
        }



        [HttpPost]
        [Route("/getChat")]
        public async Task<IActionResult> GetChats(IFormCollection data)
        {
            var id = data["id"];    
            var result = await _chatService.GetChat(int.Parse(id));
            return Ok(result);
        }





    }
}
