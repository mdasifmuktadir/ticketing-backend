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
    public class NoteListsController : ControllerBase
    {

        private readonly INotesService _notesService;
      
        
        public NoteListsController(INotesService notesService)
        {
            _notesService = notesService;
     
        }
       


       
        [HttpPost]
        [Route("/getNotes")]
        public async Task<IActionResult> GetNotes(IFormCollection data)
        {


           var result = await _notesService.GetNotesById(int.Parse(data["id"]));

            return Ok(result);


        }
    }
              
}
