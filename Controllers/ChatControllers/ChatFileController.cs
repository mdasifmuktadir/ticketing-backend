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
    public class ChatFileController : Controller
    {

        IChatService _chatService;
        IFileHandler _fileHandler;
        
        public ChatFileController(IChatService chatService, IFileHandler fileHandler) 
        {
            _chatService = chatService;
            _fileHandler = fileHandler;
        }



        [HttpPost]
        [Route("/uploadFiles")]
        public async Task<IActionResult> UploadFiles(IFormCollection data)
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads")); ;

            var fileNames = new List<File2>();
                foreach (var file in data.Files)
                {
                    var newName = _fileHandler.GetUniqueFileName(file.FileName);
                    await _fileHandler.SaveFile(path, newName, file);

                    fileNames.Add(new File2 { OriginalName = file.FileName, FileName = newName });


                }

                

                return Ok(fileNames);

            
         
        }

    }
}
