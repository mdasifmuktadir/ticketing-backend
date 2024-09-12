using Eapproval.Models;

namespace Eapproval.Services.IServices;


public interface IChatService{

    Task<List<Chat>> GetChats();
    Task<Chat> GetChat(int id);
    Task InsertChat(Chat chat);
    Task UpdateChat(int? id, Chat chat);


}