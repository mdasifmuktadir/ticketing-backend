using Eapproval.Models;

namespace Eapproval.Services.IServices;


public interface IConnectionService{
    Task<Chat> GetConnection(int ticketId);
    Task AddConnection(int ticketId, ConnectionHolderClass connectionHolderClass);
}

