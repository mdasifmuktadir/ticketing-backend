using Eapproval.Models;

namespace Eapproval.Services.IServices;

public interface ILocationService
{
       Task DeleteLocation(int id);
       Task AddLocation(Location location);
       Task<List<Location>> GetAllLocations();
}