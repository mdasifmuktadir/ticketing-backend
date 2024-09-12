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
    public class NotesController : ControllerBase
    {

        private readonly INotesService _notesService;
        
        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
         
        }

        [HttpPost]
        [Route("/insertNote")]
        public async Task<IActionResult> InsertNote(IFormCollection data)
        {

            var newNote = new Notes()
            {
                TicketId = int.Parse(data["id"]),
                Data = data["note"],
                TakenBy = data["userName"],
                Date = data["date"]

            };

            await _notesService.InsertNote(newNote);

            return Ok(newNote);
            




        }


     
    }
              
}
