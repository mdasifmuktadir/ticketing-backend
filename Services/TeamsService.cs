using Eapproval.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.ComponentModel;
using Eapproval.Factories.IFactories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Eapproval.Services.IServices;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Eapproval.services;

public class TeamsService:ITeamsService
{
 
    private readonly IConnection _connection;
    private readonly IUsersService _usersService;

    private TicketContext _context;

       

    public TeamsService( IUsersService usersService, IConnection connection, TicketContext context)
    {
        _connection = connection;
        _usersService = usersService;
        _context = context;
    }


    public async Task<List<User>> GetConcernedUsers(string name)
    {   
        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.Subordinates)
                
                     .Where(t => t.Name == name)
                     .SelectMany( t => t.Subordinates)
                     .ToListAsync();

        return result;                     



        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql  = @"SELECT s.* FROM Subordinates s
        //             INNER JOIN Teams t ON t.Id = s.TeamId
        //             WHERE t.Name = @Name";
        // var results = await connection.QueryAsync<SubordinatesClass>(sql, new { Name = name });
        // return results.ToList();
    }


    public async Task<List<Team>> GetAllTeams(){
       
        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.ProblemTypes)
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                
                     .ToListAsync();
        
        return result;



        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<Team>("SELECT * FROM Teams");
        // return result.ToList();
    }
    

   
   public async Task<List<Team>> GetTeamsForMonitors(User user){

        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
              
                     .Where(t => t.Monitors.Any(m => m.MailAddress == user.MailAddress))
                     .ToListAsync();

        return result;




    // await using var connection = _connection.GetConnection();
    // await connection.OpenAsync();
    // var sql = @"SELECT DISTINCT t.* FROM Teams t
    //             INNER JOIN Monitors m ON m.TeamId = t.Id
    //             WHERE m.Email = @Email";
    // var results = await connection.QueryAsync<Team>(sql, new { Email = user.MailAddress });
    
    // return results.ToList();
   }

    public async Task<Team> GetTeamByName(string Name)
    {    
        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                  
                     .Where(t => t.Name == Name)
                     .FirstOrDefaultAsync();

        return result;


        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT  t.* FROM Teams t 
        //             INNER JOIN Services s ON s.TeamId = t.Id
        //             WHERE s.Name = @Name OR t.Name = @Name";
        // var result = await connection.QueryFirstOrDefaultAsync<Team>(sql, new { Name = Name });
        // return result;
    }


    public async Task<List<Team>> GetTeamsForHead(string email)
    {

        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                 
                     
                     .Where(t => t.Leaders.Any(l => l.MailAddress == email))
                     .ToListAsync();

        return result;

        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT t.* FROM Teams t
        //             INNER JOIN Leaders l ON l.TeamId = t.Id
        //             WHERE l.Email = @Email";
        // var results = await connection.QueryAsync<Team>(sql, new { Email = email });
        // return results.ToList();
        
    }


    public async Task<List<Team>> GetTeamsForDepartmentHead(string email){
        
        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                   
                     .Where(t => t.Monitors.Any(m => m.MailAddress == email))
                     .ToListAsync();

        return result;


        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT t.* FROM Teams t
        //             INNER JOIN Monitors m ON m.TeamId = t.Id
        //             WHERE m.Email = @Email";
        // var results = await connection.QueryAsync<Team>(sql, new { Email = email });
        // return results.ToList();
    
    }

    public async Task<List<User>> GetSupportFromHead(User user)
    {
        
        var result = await _context.Teams
                    .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)

                    .Where(t => t.Leaders.Any(l => l.MailAddress == user.MailAddress))
                    .SelectMany(t => t.Subordinates)
                
                    .ToListAsync();

        return result;



        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT su.* FROM Users L
        //             INNER JOIN Teams t on L.TeamLeaderId = t.Id
        //             INNER JOIN Subordinates S ON s.TeamId = t.Id
        //             INNER JOIN Users su ON su.SubordinateId = s.Id
        //             WHERE l.Email = @Email";
                    
                    
        // var result = await connection.QueryAsync<User>(sql, new { Email = user.MailAddress });
        
        // return result.ToList();
    }


    public async Task<List<User>> GetSupportForDepartmentHead(User user){


        var result = await _context.Teams
                    .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                    .Where(t => t.Monitors.Any(m => m.MailAddress == user.MailAddress))
                    .SelectMany(t => t.Subordinates)
                
                    .ToListAsync();

        return result;


        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT su.* FROM Users L
        //             INNER JOIN Monitors m on L.monitorId = m.Id
        //             INNER JOIN Teams t ON m.TeamId = t.Id
        //             INNER JOIN Subordinates S ON s.TeamId = t.Id
        //             INNER JOIN Users su ON su.SubordinateId = s.Id
        //             WHERE L.Email = @Email";

        // var result = await connection.QueryAsync<User>(sql, new { Email = user.MailAddress });
        // return result.ToList();
    }


    public async Task<List<User>> GetAllSupport()
    {
        var result = await _context.Teams
                    .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                    .SelectMany(t => t.Subordinates)
                    .ToListAsync();


        return result;




        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT su.* FROM Users S
        //             INNER JOIN Subordinates su on s.SubordinateId = su.Id";

        // var result = await connection.QueryAsync<User>(sql);
        // return result.ToList();
                    
    }
    



    public async Task<Team?> GetTeamById(int id)
    {
        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                     .Include(x => x.ProblemTypes)
                    

                     .Where(t => t.Id == id)

                     .FirstOrDefaultAsync();

        return result;


        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryFirstOrDefaultAsync<Team>("SELECT * FROM Teams WHERE Id = @Id", new { Id = id });
        // return result;
    }

    public async Task CreateTeam(Team newTeam){


      _context.Entry(newTeam).State = EntityState.Added;

      foreach(var s in newTeam.Subordinates){
        if(s.Id != 0){
            _context.Entry(s).State = EntityState.Modified;
        }else{
            _context.Entry(s).State = EntityState.Added;
        }
      }

      foreach(var p in newTeam.ProblemTypes){
        _context.Entry(p).State = EntityState.Added;
      }

      foreach(var l in newTeam.Leaders){
        _context.Entry(l).State = EntityState.Modified;
      }

      foreach(var m in newTeam.Monitors){

      _context.Entry(m).State = EntityState.Modified;


      }

  
        await _context.SaveChangesAsync();



        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"INSERT INTO Teams (Id, Name, Description, ProblemTypes, Subordinates, Leaders, Monitors)
        //             VALUES (@Id, @Name, @Description, @ProblemTypes, @Subordinates, @Leaders, @Monitors)  ";
        // await connection.ExecuteAsync(sql, newTeam);


    }
    

    public async Task UpdateTeam(int id, Team updatedTeam){


        var original = await _context.Teams
                     .Include( x => x.Leaders)
                     .Include(x => x.ProblemTypes)
                     .Include( x => x.Monitors)
                     .Include(x => x.Subordinates)
                     .Include(x => x.Details)
                     .Where(t => t.Id == id)
                     .FirstOrDefaultAsync();

        original.Name = updatedTeam.Name;

        var originalMonitorsCopy = original.Monitors.ToList();

        originalMonitorsCopy.ForEach(monitor=>{
             var exist = updatedTeam.Monitors.Any(x => x.Id == monitor.Id);
             if(exist == false){
                original.Monitors.Remove(monitor);
                monitor.TeamsMonitored.Remove(original);
             }
        });

        updatedTeam.Monitors.ForEach(monitor => {
            var exist = original.Monitors.Find(x => x.Id == monitor.Id);
            if(exist == null){
                original.Monitors.Add(monitor);
                monitor.TeamsMonitored = new List<Team>();
                monitor.TeamsMonitored.Add(original);
                _context.Entry(monitor).State = EntityState.Modified;
            }
        });


        var originalLeadersCopy = original.Leaders.ToList();

        originalLeadersCopy.ForEach(leader=>{
             var exist = updatedTeam.Leaders.Any(x => x.Id == leader.Id);
             if(exist == false){
                original.Leaders.Remove(leader);
                leader.TeamsLeaded.Remove(original);
             }
        });

        updatedTeam.Leaders.ForEach(leader => {
            var exist = original.Leaders.Find(x => x.Id == leader.Id);
            if(exist == null){
                original.Leaders.Add(leader);
                leader.TeamsLeaded = new List<Team>();
                leader.TeamsLeaded.Add(original);
                _context.Entry(leader).State = EntityState.Modified;
            }
        });




        var originalSubordinatesCopy = original.Subordinates.ToList();

        originalSubordinatesCopy.ForEach(subordinate=>{
             var exist = updatedTeam.Subordinates.Any(x => x.Id == subordinate.Id);
             if(exist == false){
                original.Subordinates.Remove(subordinate);
                subordinate.TeamMembers.Remove(original);
             }
        });

        updatedTeam.Subordinates.ForEach(subordinate => {
            var exist = original.Subordinates.Find(x => x.Id == subordinate.Id);
            if(exist == null){
                original.Subordinates.Add(subordinate);
                subordinate.TeamMembers ??= new List<Team>();
                subordinate.TeamMembers.Add(original);
                _context.Entry(subordinate).State = EntityState.Modified;
            }
        });





         var originalProblemTypes = original.ProblemTypes.ToList();

        originalProblemTypes.ForEach(problemType=>{
             var exist = updatedTeam.ProblemTypes.Any(x => x.Id == problemType.Id);
             if(exist == false){
                original.ProblemTypes.Remove(problemType);
             }
        });

        updatedTeam.ProblemTypes.ForEach(problemType => {
            var exist = original.ProblemTypes.Find(x => x.Id == problemType.Id);
            if(exist == null){
                original.ProblemTypes.Add(problemType);
            }else{
                exist.Subs = problemType.Subs;
                exist.Name = problemType.Name;
            }
        });


        var originalDetails = original.Details.ToList();

        originalDetails.ForEach(detail=>{
             var exist = updatedTeam.Details.Any(x => x.Id == detail.Id);
             if(exist == false){
                original.Details.Remove(detail);
             }
        });

        updatedTeam.Details.ForEach(detail => {
            var exist = original.Details.Find(x => x.Id == detail.Id);
            if(exist == null){
                original.Details.Add(detail);
            }else{
                exist.Label = detail.Label;
                exist.Input = detail.Input;
            }
        });



         

       
        

    //   original.Subordinates.ForEach(s => {
    //        var exist = updatedTeam.Subordinates.Any(x => x.Id == s.Id);
    //        if(!exist){
    //            _context.Entry(s).State = EntityState.Deleted;
    //        }else{
    //             _context.Entry(s).State = EntityState.Modified;
    //        }
    //     });


    //     original.Leaders.ForEach(l => {
    //        var exist = updatedTeam.Leaders.Any(x => x.Id == l.Id);
    //        if(!exist){
    //            _context.Entry(l).State = EntityState.Deleted;
    //        }else{
    //             _context.Entry(l).State = EntityState.Modified;
    //        }
    //     });

    //     var originalMonitorsCopy = original.Leaders.ToList();


    //     originalMonitorsCopy.ForEach(m => {
    //        var exist = updatedTeam.Monitors.Any(x => x.Id == m.Id);
    //        if(!exist){
    //            original.Monitors.Remove(m);
    //            m.TeamsMonitored.Remove(original);
    //            _context.Entry(m).State = EntityState.Modified;

    //        }else{
    //             _context.Entry(m).State = EntityState.Modified;
    //        }
    //     });


    //     original.Details.ForEach(d => {
    //        var exist = updatedTeam.Details.Any(x => x.Id == d.Id);
    //        if(!exist){
    //            _context.Entry(d).State = EntityState.Deleted;
    //        }else{
    //             _context.Entry(d).State = EntityState.Modified;
    //        }
    //     });
        

    //     original.ProblemTypes.ForEach(p => {
    //         var exist = updatedTeam.ProblemTypes.Any(x => p.Id == x.Id );

    //         if(!exist){
    //             _context.Entry(p).State = EntityState.Added;
    //         }else{
    //             _context.Entry(p).State = EntityState.Modified;
    //         }
    //     });


        // _context.Entry(original).State = EntityState.Modified;




        await _context.SaveChangesAsync();


        



      

    }
        

    public async Task RemoveTeam(Team team){

            _context.Entry(team).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
    }

    public async Task<List<ProblemTypesClass>> GetProblemForUser(User user){
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var sql = @"SELECT DISTINCT p.* FROM ProblemTypes p
        //             INNER JOIN Teams t ON t.Id = p.TeamId
        //             INNER JOIN Subordinates s ON s.TeamId = t.Id
        //             INNER JOIN Users u on u.SubordinateId = s.Id
        //             WHERE u.Email = @Email";
        // var result = await connection.QueryAsync<ProblemTypesClass>(sql, new { Email = user.MailAddress });
        // return result.ToList();


        var result = await _context.Teams
                     .AsNoTracking()
                     .Include(x => x.Monitors)
                     .Include(x => x.Leaders)
                     .Include(x => x.Subordinates)
                     .Include(x => x.ProblemTypes)

                     .Where(t => t.Subordinates.Any(s => s.MailAddress == user.MailAddress))
                     .SelectMany(t => t.ProblemTypes)
                     .ToListAsync();

        return result;

    }





}
