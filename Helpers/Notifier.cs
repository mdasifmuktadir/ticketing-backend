using Eapproval.Models;
using Eapproval.Services;
using Eapproval.Services.IServices;
using Eapproval.Helpers.IHelpers;



using System.Text.Json;

namespace Eapproval.Helpers
{
    public class Notifier:INotifier
    {

        private INotificationService _notificationService;

        private IHelperClass _helperClass;

        public Notifier(INotificationService notificationService, IHelperClass helperClass) {
        
            _notificationService = notificationService;
            _helperClass = helperClass;
          
        
        }

        public async Task InsertNotification(string time, string message, User from, User to, int? ticketId, List<string> mentions = null, string type = "message", string section = "ticketing")
        {

            var newNotification = new Notification
            {
                Time = time,
                Message = message,
                TicketId = ticketId,
                Type = type,
                FromId = from.Id,
                ToId = to.Id,
                Mentions = mentions,
         
            };


            await _notificationService.InsertNotification(newNotification);



        }











       
    }
}
