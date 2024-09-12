using Microsoft.AspNetCore.Mvc;
using Eapproval.Models;
using Eapproval.services;

using System.Text.Json;
using Eapproval.Helpers;
using Eapproval.Services;
using IronPdf;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.ReportControllers;

    [Route("/")]
    [ApiController]
    public class ReportController : Controller
    {
     
    
    
         private readonly IFileHandler _fileHandler;

         private readonly ITicketMailer _ticketMailer;
        

        public ReportController( ITicketMailer ticketMailer, IFileHandler fileHandler)
        {
       
            _fileHandler = fileHandler;
            _ticketMailer = ticketMailer;
        
        }



        [HttpPost]
        [Route("/sendReport")]
        public async Task<IActionResult> SendReport(IFormCollection data){
            var users = JsonSerializer.Deserialize<List<User>>(data["users"]);
          
            var file = data.Files["pdf"];
       
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads")); 
            var fileName = _fileHandler.GetUniqueFileName(file.FileName);
            var filePath = await _fileHandler.SaveFile(path, fileName, file);
            _ticketMailer.SendPdfToUsers(filePath, users);

            return Ok(true);



            
        }






}
