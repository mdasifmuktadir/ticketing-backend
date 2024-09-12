using Eapproval.Models;
using Eapproval.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.NotificationControllers
{

    [ApiController]
    [Route("/")]
    public class NotificationListsController : Controller
    {
        private readonly INotificationService _notificationService;
     
        public NotificationListsController(INotificationService notificationService) {

            _notificationService = notificationService;
           
        }


        [HttpPost]
        [Route("/getNotifications")]
        public async Task<IActionResult> GetNotifications(IFormCollection data)
        {

            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var result = await _notificationService.GetNotificationsByUser(user.MailAddress, user.EmpName, user.Id);

            return Ok(result);
                
        }

    }
}
