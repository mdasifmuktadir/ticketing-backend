using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MongoDB.Bson;
using Eapproval.services;
using Eapproval.Models;
using Eapproval.Helpers;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.NoteControllers
{
    [Route("/")]
    [ApiController]
    public class NoteFileController : ControllerBase
    {

        private readonly INotesService _notesService;
        private readonly IHelperClass _helperClass;

        
        public NoteFileController(IUsersService usersService, INotifier notifier, INotesService notesService, IHelperClass helperClass, ITicketsService ticketService)
        {
            _notesService = notesService;
            _helperClass = helperClass;
      
        }
      


        [HttpPost]
        [Route("/uploadCommentFiles")]
        public async Task<IActionResult> UploadCommentFiles(IFormCollection data)
        {

            var files = await _helperClass.GetFiles(data);
            var newNote = new Notes()
            {
                TicketId = int.Parse(data["id"]),
                TakenBy = data["userName"],
                Date = data["date"],
                Type = "file",
                Files = files,
                Caption = data["caption"]

            };
            await _notesService.InsertNote(newNote);
            return Ok(newNote);

        }


       
    }
              
}
