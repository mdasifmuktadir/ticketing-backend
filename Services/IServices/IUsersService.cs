using Eapproval.Models;

namespace Eapproval.Services.IServices;


public interface IUsersService{

    Task RemoveAsync(int? id);
    Task UpdateUserNumber(User user);
    Task UpdateAsync(int? id, User updatedTicket);
    Task CreateAsync(User newUser);
    Task<List<User>> GetUsers(List<User> leaders);
    Task<User?> GetOneUser(int? id);
    Task<List<User>> GetUsersIncludingAdmin();
    Task<List<User>> GetAllNormalUsers();
    Task<User> GetUserByMailAndPassword(string mail, string password);
    Task<User> GetUserByName(string name);
    Task<User> GetUserByMail(string mail);
    Task<List<User>> GetSupportUsers(List<Team> teams);
    Task<List<User>> GetAsync();




}