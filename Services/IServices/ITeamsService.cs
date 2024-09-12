using Eapproval.Models;

namespace Eapproval.Services.IServices;

public interface ITeamsService{
    Task<List<ProblemTypesClass>> GetProblemForUser(User user);
    Task RemoveTeam(Team team);
    Task UpdateTeam(int id, Team updatedTeam);
    Task CreateTeam(Team newTeam);
    Task<Team?> GetTeamById(int id);
    Task<List<User>> GetAllSupport();
    Task<List<User>> GetSupportForDepartmentHead(User user);
    Task<List<User>> GetSupportFromHead(User user);
    Task<List<Team>> GetTeamsForDepartmentHead(string email);
    Task<List<Team>> GetTeamsForHead(string email);
    Task<Team> GetTeamByName(string Name);
    Task<List<Team>> GetAllTeams();
    Task<List<User>> GetConcernedUsers(string name);
    Task<List<Team>> GetTeamsForMonitors(User user);
}