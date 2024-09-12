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
    public class NoteMentionController : ControllerBase
    {

        private readonly INotesService _notesService;
        private readonly IHelperClass _helperClass;
        private readonly ITicketsService _ticketService;
        private readonly INotifier _notifier;
        private readonly IUsersService _usersService;
        
        public NoteMentionController(IUsersService usersService, INotifier notifier, INotesService notesService, IHelperClass helperClass, ITicketsService ticketService)
        {
            _notesService = notesService;
            _helperClass = helperClass;
            _ticketService = ticketService;
            _notifier = notifier;
            _usersService = usersService;
        }



        [HttpPost]
        [Route("/makeMentions")]
        public async Task<IActionResult> MakeMentions(IFormCollection data)
        {

            var mentions = JsonSerializer.Deserialize<List<string>>(data["mentions"]);
            var ticket = JsonSerializer.Deserialize<Tickets>(data["ticket"]);
            var user = JsonSerializer.Deserialize<User>(data["user"]);

            var time = _helperClass.GetCurrentTime();
            var newNote = new Notes()
            {
                TicketId = ticket.Id,
                TakenBy = user.EmpName,
                Date = data["date"],
                Type = "mention",
                Mentions = mentions,
                Caption = data["message"]

            };

            ticket.Mentions = mentions;
            foreach (var mention in mentions)
            {
                var notificationUser = await _usersService.GetUserByName(mention);
                await _notifier.InsertNotification(time, "Notification", user, notificationUser, ticket.Id, mentions, "mention");
            }
           
            await _ticketService.UpdateAsync(ticket.Id, ticket);
            await _notesService.InsertNote(newNote);

            return Ok(newNote);





        }



    }
              
}
