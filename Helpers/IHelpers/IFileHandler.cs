namespace Eapproval.Helpers.IHelpers;

using Eapproval.Models;



public interface IFileHandler{
    string GetUniqueFileName(string fileName);
    Task<string> SaveFile(string path, string filename, IFormFile file);
    
}