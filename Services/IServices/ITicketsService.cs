using Eapproval.Models;
using Eapproval.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace Eapproval.Services.IServices;


public interface ITicketsService{
    Task<JsonResult> GetHandlerStats();
    Task<JsonResult> GetTicketsByDepartment(string department, string location);
    Task<JsonResult> GetTicketsByStatus();
    Task<JsonResult> GetProjectedTicketsForHandlers(User user);
    Task<List<TicketVM>> GetTicketsForMonitors(User user, int page);
    Task<List<TicketVM>> GetTicketsForHandler(User user, int page);
    Task<List<TicketVM>> GetTicketsForHandler2(User user, int page);
    Task<List<Tickets>> GetDepartmentTickets(string userMail);
    Task<List<Tickets>> GetDepartmentHeadTickets(User user);
    Task<List<TicketVM>> GetTicketsRaisedByUser(User user);
    Task<Tickets?> GetAsync(int? id);
    Task CreateAsync(Tickets newTicket);
    Task<List<TicketVM>> GetAllTickets(int page);
    Task UpdateAsync(int? id, Tickets updatedTicket);
    Task RemoveAsync(string id);
    Task<List<TicketVM>> GetTicketsForLeader(User user);
    Task<List<TicketVM>> GetTicketsForNormal(User user);
    Task<List<TicketVM>> GetTicketsForSupport(User user);
    Task<List<TicketVM>> GetTicketsByDepartment(List<string> departments);
    Task<Tickets> GetOldTicket(User user, int? genesisId);

    Task UpdateAsyncWithPriority(int id, Tickets ticket);


}
