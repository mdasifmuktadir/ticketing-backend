using Eapproval.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Dapper;
using Eapproval.Factories.IFactories;
using Microsoft.EntityFrameworkCore;

using Eapproval.Services.IServices;
namespace Eapproval.services;

public class NotesService:INotesService
{

    private IConnection _connection;
    private TicketContext _context;

    public NotesService(IConnection connection, TicketContext context)
    {
        _connection = connection;
        _context = context;
    }
  

    public async Task<List<Notes>> GetNotesById(int id){

        var results = await _context.Notes.AsNoTracking()
        .Include(x => x.Files)
        .Where(x => x.TicketId == id).ToListAsync();

        return results;
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<Notes>("SELECT * FROM Notes WHERE TicketId = @Id", new { Id = id });
        // return result.ToList();
    }
   


    public async Task InsertNote(Notes note)
    {
        _context.Entry(note).State = EntityState.Added;
    

        await _context.SaveChangesAsync();

    if(note.Files != null){

    
        foreach(var file in note.Files){
            file.NoteId = note.Id;
            _context.Entry(file).State = EntityState.Added;
        }

    }

        await _context.SaveChangesAsync();
          
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // await connection.ExecuteAsync("INSERT INTO Notes (Id, TicketId, Note, Date, UserId, UserName, UserSurname, UserMailAddress, UserProfilePicture) VALUES (@Id, @TicketId, @Note, @Date, @UserId, @UserName, @UserSurname, @UserMailAddress, @UserProfilePicture)", note);
    }



    


}
