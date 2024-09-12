using Eapproval.Models;

namespace Eapproval.Services.IServices;


public interface INotesService {
    Task InsertNote(Notes note);
    Task<List<Notes>> GetNotesById(int id);
}