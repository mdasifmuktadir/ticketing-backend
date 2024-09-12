namespace Eapproval.Helpers.IHelpers;

using Eapproval.Models;



public interface IHasher{
    public string HashPassword(string password);
    public bool VerifyPassword(string password, string hashedPassword);
    
}