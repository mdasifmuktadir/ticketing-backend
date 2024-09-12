using System.Formats.Asn1;
using Eapproval.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Org.BouncyCastle.Crypto.Operators;
using Eapproval.Factories.IFactories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Forms;
using Eapproval.Services.IServices;

namespace Eapproval.services;

public class UsersService:IUsersService
{

    private IConnection _connection;
    private TicketContext _context;

    public UsersService(IConnection connection, TicketContext context)
    {
        _connection = connection;
        _context = context;

    }


    public async Task<List<User>> GetAsync(){

       var result = await _context.Users.AsNoTracking().ToListAsync();

         return result;

        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<User>("SELECT * FROM Users");
        // return result.ToList();

    }
   

   public async Task<List<User>> GetSupportUsers(List<Team> teams){

    
    List<string> mails = new List<string>();

    foreach(var team in teams){
        foreach(var subordinate in team.Subordinates){
            mails.Add(subordinate.MailAddress);
        }
    }

    var result = await _context.Users.AsNoTracking()
             .Where(x => mails.Contains(x.MailAddress)).ToListAsync();

        return result;
    // await using var connection = _connection.GetConnection();
    // await connection.OpenAsync();
    // var sql = @"SELECT DISTINCT u.* FROM Users u
    //             WHERE u.MailAddress IN @Mails";

    // var result = await connection.QueryAsync<User>(sql, new { Mails = teams.Select(x => x.Subordinates.Select(y => y.User.MailAddress)).ToList() });

    // return result.ToList();  
   
   }

    public async Task<User> GetUserByMail(string mail)
    {
        
        var result = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.MailAddress == mail);
        return result;


        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE MailAddress = @MailAddress", new { MailAddress = mail });
        // return result;
        
    }


        public async Task<User> GetUserByName(string name)
    {


        var result = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.EmpName == name);
        return result;
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Name = @Name", new { Name = name });
        // return result;

        
    }


    public async Task<User> GetUserByMailAndPassword(string mail, string password)
    {
        
        var result = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.MailAddress == mail && x.Password == password);
        return result;

        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE MailAddress = @MailAddress AND Password = @Password", new { MailAddress = mail, Password = password });
        // return result;
    }

    public async Task<List<User>> GetAllNormalUsers()
    {

          var result = await _context.Users.AsNoTracking().Where(x => x.UserType != "Admin" && x.UserType != "Master Admin").ToListAsync();
          return result;
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<User>("SELECT * FROM Users WHERE UserType != 'Admin' AND UserType != 'Master Admin'");
        // return result.ToList();
        
    }


    public async Task<List<User>> GetUsersIncludingAdmin()
    {

            var result = await _context.Users.AsNoTracking().Where(x => x.UserType != "Master Admin").ToListAsync();
            return result;

        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<User>("SELECT * FROM Users WHERE UserType != 'Master Admin'");
        // return result.ToList();
      
    }

    public async Task<User?> GetOneUser(int? id){

        var result = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return result;

        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        // return result;
    }
    


    public async Task<List<User>> GetUsers(List<User> leaders){

        List<string> mails = new List<string>();    
        foreach(var leader in leaders){
            mails.Add(leader.MailAddress);
        }

        var result = await _context.Users.AsNoTracking()
             .Where(x => mails.Contains(x.MailAddress)).ToListAsync();

        return result;
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT u.* FROM Users u
        //             WHERE u.MailAddress IN @Mails";
        // var result = await connection.QueryAsync<User>(sql, new { Mails = leaders.Select(x => x.MailAddress).ToList() });
        // return result.ToList();
    }

    public async Task CreateAsync(User newUser){

        _context.Entry(newUser).State = EntityState.Added;
        await _context.SaveChangesAsync();
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @""; 
        // await connection.ExecuteAsync("INSERT INTO Users (Id, Name, MailAddress, Password, UserType, Numbers) VALUES (@Id, @Name, @MailAddress, @Password, @UserType, @Numbers)", new { Id = newTicket.Id, Name = newTicket.Name, MailAddress = newTicket.MailAddress, Password = newTicket.Password, UserType = newTicket.UserType, Numbers = newTicket.Numbers });
    }
        

    public async Task UpdateAsync(int? id, User updatedTicket){
          
            _context.Entry(updatedTicket).State = EntityState.Modified;        

           await _context.SaveChangesAsync();


        // await _user.ReplaceOneAsync(x => x.Id == id, updatedTicket);

    }

    public async Task UpdateUserNumber(User user)
    {

        user.Numbers = user.Numbers + 1;
        _context.Entry(user).Property(x => x.Numbers).IsModified = true;
        await _context.SaveChangesAsync();
        // await using var connection = _connection.GetConnection();
        // user.Numbers = user.Numbers + 1;

        // await connection.OpenAsync();
        // await connection.ExecuteAsync("UPDATE Users SET Numbers = @Numbers WHERE Id = @Id", new { Numbers = user.Numbers, Id = user.Id });
    
    }

    public async Task RemoveAsync(int? id){

        if(id != null){
            int newId = (int)id;
        _context.Entry(new User(){Id = newId }).State = EntityState.Deleted;
        }


        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });


    }

    // public async Task<User?> GetOneUserByGroups(string group) =>
    //     await _user.Find(x => x.Groups.Contains(group)).FirstOrDefaultAsync();



}
