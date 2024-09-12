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

public class TicketSupportService
{

    private IConnection _connection;
    private readonly ITeamsService _teamsService;
    private readonly CounterService _counterService;

    private TicketContext _context;
    private IMapper _mapper;


    

    private IHelperClass _helperClass;

    

    public TicketSupportService(IMapper mapper, TicketContext context, ITeamsService teamsService, CounterService counterService, IHelperClass helperClass, IConnection connection)
    {
        _teamsService = teamsService;
        _counterService = counterService;
        _helperClass = helperClass;
        _connection = connection;
        _context = context;
        _mapper = mapper;
    }

    //  public async Task<TicketsCountVM> GetAllTickets(User user, int page){
    //     int pageSize = 10;
    //     int skipValue = (page - 1) * pageSize;
    //     int totalCount = await _context.Tickets.CountAsync();

    //     var teams = await _context.Teams.AsNoTracking()
    //                       .Include(x => x.Subordinates)
    //                       .Where(x => x.Subordinates.Any(x => x.UserId == user.Id))
    //                       .ToListAsync();
        
    //     var result = await _context.Tickets
    //                  .AsNoTracking()
    //                  .Include(x => x.AssignedTo)
    //                  .Include(x => x.CurrentHandler)
    //                  .Include(x => x.TicketingHead)
    //                  .Include(x => x.RaisedBy)
    //                  .Where( x => teams.)
    //                  .OrderBy(x => x.Id)
    //                  .Skip(skipValue)
    //                  .Take(pageSize)
    //                  .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
    //                  .ToListAsync();
                     
    //      return new TicketsCountVM{Tickets = result, Count = totalCount};
        

    // }


     public async Task<TicketsCountVM> GetUnassignedTickets(User user, int page){
        int pageSize = 10;
        int skipValue = (page - 1) * pageSize;
        int totalCount = await _context.Tickets.CountAsync();

        var teams = await _context.Teams.AsNoTracking()
                          .Include(x => x.Subordinates)
                          .Where(x => x.Subordinates.Any(x => x.Id == user.Id))
                          .ToListAsync();
        
        var result = await _context.Tickets
                     .AsNoTracking()
                     .Include(x => x.AssignedTo)
                     .Include(x => x.CurrentHandler)
                     .Include(x => x.TicketingHead)
                     .Include(x => x.RaisedBy)
                     .Where( x => teams.Any(t => t.Name == x.Department))
                     .Where(x => x.AssignedTo == null)
                     .OrderBy(x => x.Id)
                     .Skip(skipValue)
                     .Take(pageSize)
                     .ProjectTo<TicketVM>(_mapper.ConfigurationProvider)
                     .ToListAsync();
                     
         return new TicketsCountVM{Tickets = result, Count = totalCount};
        

    }



    


}
