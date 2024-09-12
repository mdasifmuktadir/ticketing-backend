using Eapproval.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.IO;
using Eapproval.Factories.IFactories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Eapproval.Services.IServices;


namespace Eapproval.services;

public class ChatService:IChatService
{

    private IConnection _connection;

    private TicketContext _context;

    public ChatService(IConnection connection, TicketContext context)
    {
        _connection = connection;
        _context = context;
    }
    

    public async Task<List<Chat>> GetChats()
    {

        var results = await _context.Chats
        .Include(c => c.Ticket)
        .Include(c => c.ConnectionHolders)
        .Include(c => c.Conversation)
        .AsNoTracking().ToListAsync();

        return results;
        
        
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<Chat>("SELECT * FROM Chats");
        // return result.ToList();
        
       
    }

    public async Task<Chat> GetChat(int id)
    {


        var result = await _context.Chats
        .Include(c => c.Ticket)
        .Include(c => c.ConnectionHolders)
        .Include(c => c.Conversation)
        .ThenInclude(c => c.From)
        .Include(c => c.Conversation)
        .ThenInclude(x => x.Files)
    
        .AsNoTracking().FirstOrDefaultAsync(x => x.TicketId == id);


        return result;
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryFirstOrDefaultAsync<Chat>("SELECT * FROM Chats WHERE Id = @Id", new { Id = id }); 
        // return result; 
    }

    public async Task InsertChat(Chat chat)
    {

        _context.Entry(chat).State = EntityState.Added;
        _context.Entry(chat.Ticket).State = EntityState.Unchanged;
        _context.Entry(chat.ConnectionHolders).State = EntityState.Unchanged;
        _context.Entry(chat.Conversation).State = EntityState.Unchanged;

        await _context.SaveChangesAsync(); 

        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // await connection.ExecuteAsync("INSERT INTO Chats (Id, Name, Description, Date, Messages) VALUES (@Id, @Name, @Description, @Date, @Messages)", chat);
  
    }


    public async Task UpdateChat(int? id, Chat chat)
    {

        _context.Entry(chat).State = EntityState.Modified;
         
        foreach(var conversation in chat.Conversation){
            if(conversation.Id == null || conversation.Id == 0){
                _context.Entry(conversation).State = EntityState.Added;

                foreach(var file in conversation.Files){
                    _context.Entry(file).State = EntityState.Added;
                }
            }
        }
    
      

        await _context.SaveChangesAsync(); 
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // await connection.ExecuteAsync("UPDATE Chats SET Name = @Name, Description = @Description, Date = @Date, Messages = @Messages WHERE Id = @Id", new { Id = id, Name = chat.Name, Description = chat.Description, Date = chat.Date, Messages = chat.Messages });
    }
   



}
