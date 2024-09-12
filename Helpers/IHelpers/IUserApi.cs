namespace Eapproval.Helpers.IHelpers;

using Eapproval.Models;



public interface IUserApi{
      Task<string?> GetTeams();
}