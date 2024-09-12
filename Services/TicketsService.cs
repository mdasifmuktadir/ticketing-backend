using Amazon.SecurityToken.Model;
using Eapproval.Models;
using Eapproval.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Org.BouncyCastle.Tls;
using System.Linq;
using Eapproval.Helpers;
using Eapproval.Helpers.IHelpers;
using Eapproval.Factories.IFactories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Eapproval.Models.VMs;
using System.Diagnostics;
using Eapproval.Services.IServices;


namespace Eapproval.services;

public class TicketsService:ITicketsService
{

    private IConnection _connection;
    private readonly ITeamsService _teamsService;
    private readonly CounterService _counterService;

    private TicketContext _context;
    private IMapper _mapper;


    

    private IHelperClass _helperClass;

    

    public TicketsService(IMapper mapper, TicketContext context, ITeamsService teamsService, CounterService counterService, IHelperClass helperClass, IConnection connection)
    {
        _teamsService = teamsService;
        _counterService = counterService;
        _helperClass = helperClass;
        _connection = connection;
        _context = context;
        _mapper = mapper;
    }

    
    public async Task<JsonResult> GetHandlerStats(){

        var result = await _context.Tickets
        .AsNoTracking()
       .Where(t => t.AssignedTo != null)
       .GroupBy(t => t.AssignedTo.EmpName)
        .Select(g => new 
                {
        Handler = g.Key,
        Count = g.Count()
               })
       .ToListAsync();

             return new JsonResult(result);
    } 


    public async Task<JsonResult> GetTicketsByDepartment(string department, string location){

           var query = _context.Tickets.AsQueryable();

              if (department != "all")
              {
                  query = query.Where(t => t.Department == department);
              }
               
               if (location != "all")
               {
                   query = query.Where(t => t.Location == location);
               }
               
               var result = await query
              .AsNoTracking()
              .GroupBy(t => t.Department)
              .Select(group => group.First()) // This line is needed because SQL's GROUP BY doesn't have a direct equivalent in LINQ
              .ToListAsync();

return new JsonResult(result);
        
    
        
    }
 
    
    public async Task<JsonResult> GetTicketsByStatus(){
        
       var result = await _context.Tickets
    .GroupBy(t => t.Status)
    .Select(g => new 
    {
        Status = g.Key,
        Count = g.Count()
    })
    .ToListAsync();

return new JsonResult(result);


    }

    public async Task<JsonResult> GetProjectedTicketsForHandlers(User user)
    {

       var results = await _context.Tickets
      .Select(t => new 
    {
        Id = t.Id,
        ProblemDetails = t.ProblemDetails,
        RaisedByEmail = t.RaisedBy.MailAddress,
        RaisedByName = t.RaisedBy.EmpName,
        CurrentHandlerEmail = t.CurrentHandler.MailAddress,
        CurrentHandlerName = t.CurrentHandler.EmpName,
        Number = t.Number,
        Status = t.Status
    })
    .ToListAsync();

   return  new JsonResult(results);
    }


//  public void BulkReplaceTickets(List<Tickets> replacements)
//     {
//         var bulkWrites = new List<WriteModel<Tickets>>();

//         foreach (var replacement in replacements)
//         {
//             var filter = Builders<Tickets>.Filter.Eq(x => x.Id, replacement.Id); // Assuming 'Id' is your unique identifier
//             var replacementModel = new ReplaceOneModel<Tickets>(filter, replacement)
//             {
//                 IsUpsert = true // Set to true if you want to insert the document if it doesn't exist
//             };

//             bulkWrites.Add(replacementModel);
//         }

//         var bulkWriteOptions = new BulkWriteOptions { IsOrdered = false }; // Set IsOrdered to false for unordered bulk write

//         var result = _tickets.BulkWrite(bulkWrites, bulkWriteOptions);

//         // Access result if needed (result.ProcessedRequests, result.ModifiedCount, etc.)
//     }

   public async Task<List<TicketVM>> GetTicketsForMonitors(User user, int page){

       int pageSize = 10;
        int skipValue = (page - 1) * pageSize;
        // int totalCount = await _context.Tickets.CountAsync();


        var teams = await _context.Teams
                       .AsNoTracking()
                       .Include(x => x.Monitors)
                
                       .Where(x => x.Monitors.Any(m => m.MailAddress == user.MailAddress))
                       .ToListAsync();

        var names = new List<string>();

        foreach(var t in teams){
            names.Add(t.Name);
        }

        var result = await _context.Tickets
                     .AsNoTracking()
                     .Include(x => x.AssignedTo)
                     
                     .Where(x => names.Contains(x.Department))
                    //  .Skip(skipValue)
                    //  .Take(pageSize)
                     .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                     .ToListAsync();

        return result;
   }

    public async Task<List<TicketVM>> GetTicketsForHandler(User user, int page){

        int pageSize = 10;
        int skipValue = (page - 1) * pageSize;
        // int totalCount = await _context.Tickets.CountAsync();
         
         var result = await _context.Tickets
                      .AsNoTracking()
                      .Include(x => x.AssignedTo)
                      .Include(x => x.CurrentHandler)
                      .Include(x => x.TicketingHead)
                      .Include(x => x.RaisedBy)
                      
                      .Where(x => (x.AssignedTo.MailAddress == user.MailAddress 
                      || x.CurrentHandler.MailAddress == user.MailAddress 
                      || x.TicketingHead.MailAddress == user.MailAddress) && x.Location == user.Location)
                    //   .Skip(skipValue)
                    //   .Take(pageSize)
                      .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                      .ToListAsync();

        return result;





    }
   
    public async Task<List<TicketVM>> GetTicketsForHandler2(User user, int page){
        int pageSize = 10;
        int skipValue = (page - 1) * pageSize;
        // int totalCount = await _context.Tickets.CountAsync();

        var teams = await _context.Teams.AsNoTracking()
                          .Include(x => x.Subordinates)
                        
                          .Where(x => x.Subordinates.Any(x => x.Id == user.Id))
                          .ToListAsync();

        List<string> teamNames = new();

        foreach(var team in teams){
            teamNames.Add(team.Name);
        }

        
        var result = await _context.Tickets
                     .AsNoTracking()
                     .Include(x => x.AssignedTo)
                     .Include(x => x.CurrentHandler)
                     .Include(x => x.TicketingHead)
                     
                     .Include(x => x.RaisedBy)
                     .Where( x => x.AssignedTo.MailAddress == user.MailAddress
                     || x.CurrentHandler.MailAddress == user.MailAddress
                     || x.TicketingHead.MailAddress == user.MailAddress
                     || x.RaisedBy.MailAddress == user.MailAddress
                     || teamNames.Contains(x.Department)
                     
                     )
                    //  .OrderBy(x => x.Id)
                    //  .Skip(skipValue)
                    //  .Take(pageSize)
                     .Select(x => new TicketVM{
                        Id = x.Id,
                        RequestDate = x.RequestDate,
                        Status = x.Status,
                        ProblemDetails = x.ProblemDetails,
                        RaisedBy =x.RaisedBy != null ? new User{
                            Id = x.RaisedById != null ? (int)x.RaisedById : 0,
                            EmpName = x.RaisedBy.EmpName,
                            MailAddress = x.RaisedBy.MailAddress
                        }:null,
                        CurrentHandler = x.CurrentHandler != null ? new User{
                              Id = x.CurrentHandlerId != null ? (int)x.CurrentHandlerId : 0,
                            EmpName = x.CurrentHandler.EmpName,
                            MailAddress = x.CurrentHandler.MailAddress
                        } : null,
                        AssignedTo = x.AssignedTo != null ? new User {
                               Id = x.AssignedToId != null ? (int)x.AssignedToId : 0,
                            EmpName = x.AssignedTo.MailAddress,
                            MailAddress = x.AssignedTo.MailAddress
                        }:null

                     })
                     .ToListAsync();
                     
         return result;
        

    }

    
    public async Task<List<Tickets>> GetDepartmentTickets(string userMail)
    {
        var teams = await _context.Teams.AsNoTracking()
        .Include(x => x.Leaders)
        .ToListAsync();

        var finalTeams = teams.Where(x => x.Leaders!.Any(l => l.MailAddress == userMail) ).ToList();

        var results = await _context.Tickets.AsNoTracking()
        .Include(x => x.Actions)
        
        .ToListAsync();

        var finalResults = results.Where(x => finalTeams.Any(t => t.Name == x.Department)).ToList();

        return finalResults;


    }

    public async Task<List<Tickets>> GetDepartmentHeadTickets(User user){

        var teams = await _context.Teams.AsNoTracking()
        .Include(x => x.Monitors)
        .ToListAsync();
        var finalTeams = teams.Where(team => team.Monitors!.Any(m => m.MailAddress == user.MailAddress)).ToList();

        var results = await _context.Tickets.AsNoTracking()
        .Include(x=> x.Actions)
        
        .ToListAsync();

        var finalResults = results.Where(ticket => finalTeams.Any(f => f.Name == ticket.Department)).ToList();

        return finalResults;                

        
    }

    public async Task<List<TicketVM>> GetTicketsRaisedByUser(User user)
    {

        var result = await _context.Tickets.AsNoTracking()
                     .Include(x => x.RaisedBy)
                     .Include(x => x.Actions)
                     
                     .Where(x => x.RaisedBy.MailAddress == user.MailAddress)
                     .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                     .ToListAsync();

        return result;


       
    }



    public async Task<Tickets?> GetAsync(int? id){

        var result = await _context.Tickets.AsNoTracking().AsSplitQuery()
        .Include(x => x.AssignedTo)
        .Include(x => x.TicketingHead)
        .Include(x => x.Actions)
        .ThenInclude(x => x.RaisedBy)
        .Include(x => x.Actions)
        .ThenInclude(x => x.ForwardedTo)
        .Include(x => x.Actions)
        .ThenInclude(x => x.Files)
        .Include(x => x.RaisedBy)
        .Include(x => x.CurrentHandler)
        .Include(x => x.PrevHandler)
        .FirstOrDefaultAsync(x => x.Id == id);
        
        
        return result;

    }
        


    public async Task CreateAsync(Tickets newTicket) {

    
        newTicket.Id = 0;
     
        _context.Entry(newTicket).State = EntityState.Added;


       
    

        await _context.SaveChangesAsync();


    }


    public async Task<List<TicketVM>> GetAllTickets(int page){

        int pageSize = 10;
        int skipValue = (page - 1) * pageSize;
        // int totalCount = await _context.Tickets.CountAsync();


        var result = await _context.Tickets.AsNoTracking()
        .Include(x => x.CurrentHandler)
        .Include(x => x.RaisedBy)
                  
        .Include(x => x.AssignedTo)
        .Include(x => x.Actions)
        
        
        .OrderBy(x => x.Id)
        // .Skip(skipValue)
        // .Take(pageSize)
        .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return result;

    }
   



    public async Task UpdateAsync(int? id, Tickets updatedTicket){

        _context.Entry(updatedTicket).State = EntityState.Modified;

        foreach(var action in updatedTicket.Actions){
            if(action.Id == 0 || action.Id == null){   
            _context.Entry(action).State = EntityState.Added;
            }

            await _context.SaveChangesAsync();
             
             if(action.Files != null){

             
            foreach(var file in action.Files){
                file.ActionId = action.Id;
                if(file.Id == 0 || file.Id == null){
                _context.Entry(file).State = EntityState.Added;

                }else{
                    _context.Entry(file).State = EntityState.Modified;
                }
            }
             }
        }

        // foreach(var file in updatedTicket.Files){
        //     file.TicketId = updatedTicket.Id;
        //     _context.Entry(file).State = EntityState.Added;
        // }

        

        await _context.SaveChangesAsync();




    }

        

    public async Task RemoveAsync(string id){

        _context.Entry(new Tickets { Id = int.Parse(id) }).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

    }
     




    public async Task<List<TicketVM>> GetTicketsForLeader(User user)
    {
        var result = await _context.Tickets.AsNoTracking()
                     .Include(x => x.TicketingHead)
                               
                     .Where(x => x.TicketingHead.MailAddress == user.MailAddress)
                     .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                     .ToListAsync();

        return result;



       

    }

    public async Task<List<TicketVM>> GetTicketsForNormal(User user)
    {   
        var result = await _context.Tickets.AsNoTracking()
                     .Include(x => x.RaisedBy)
                               
                     .Where(x => x.RaisedBy.MailAddress == user.MailAddress)
                     .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                     .ToListAsync();

        return result;



        
    }


    public async Task<List<TicketVM>> GetTicketsForSupport(User user)
    {

       var result = await _context.Tickets
                          .AsNoTracking()
                          .Include(x => x.AssignedTo)
                                    
                          .Where(x => (x.AssignedTo.MailAddress == user.MailAddress || x.AssignedTo.EmpName == user.EmpName) && x.Accepted == true)
                          .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                          .ToListAsync();

        return result;

     
    }



    public async Task<List<TicketVM>> GetTicketsByDepartment(List<string> departments){

        var result = await _context.Tickets
                     .AsNoTracking()
                               
                     .Where(x => departments.Contains(x.Department) && x.Status != "Closed Ticket")
                     .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                     .ToListAsync();

        return result;

     
    }

    public async Task<Tickets> GetOldTicket(User user, int? genesisId){
        var result = await _context.Tickets
                    .AsNoTracking()
                              
                    .Where(x => (x.GenesisId == genesisId && x.Status != "Closed Ticket") || (x.Id == genesisId && x.Status != "Closed Ticket"))
                    .FirstOrDefaultAsync();

        return result;
    }

    public async Task UpdateAsyncWithPriority(int id, Tickets ticket)
    {
        _context.Entry(ticket).State = EntityState.Modified;

        foreach(var action in ticket.Actions){
            if(action.Id == 0 || action.Id == null){   
            _context.Entry(action).State = EntityState.Added;
            }
        }

        await _context.SaveChangesAsync();
    }
}
