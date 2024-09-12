using Eapproval.Models;

namespace Eapproval.Services.IServices;

public interface IPriorityService{
 Task<List<PriorityClass>> GetPriorities();
 Task InsertPriority(PriorityClass priority);
 Task UpdatePriority(PriorityClass priority);
 Task DeletePriority(PriorityClass priority);

}