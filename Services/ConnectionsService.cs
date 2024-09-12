using Eapproval.Models;
using System.Text.Json;
using System.Collections.Generic;
using Dapper;
using Eapproval.Factories.IFactories;
using Microsoft.EntityFrameworkCore;
using Eapproval.Services.IServices;

namespace Eapproval.services
{
    public class ConnectionsService:IConnectionService
    {
        private IConnection _connection;
        private IChatService _chatService;

        private TicketContext _context;
        public ConnectionsService(IConnection connection, IChatService chatService, TicketContext context) 
        {
        
            _connection = connection;
            _chatService = chatService;
            _context = context;
        
        }
     


        public async Task AddConnection(int ticketId, ConnectionHolderClass connectionHolderClass)
        {


             var connection = await _context.Chats.Include(c => c.ConnectionHolders)
             .AsNoTracking().FirstOrDefaultAsync(x => x.TicketId == ticketId);
             
             if(connection == null){
                var newConnection = new Chat();
                newConnection.TicketId = ticketId;
                newConnection.ConnectionHolders.Add(connectionHolderClass);
  
                _context.Entry(newConnection).State = EntityState.Added;

                foreach(var connectionHolder in newConnection.ConnectionHolders){
                    _context.Entry(connectionHolder).State = EntityState.Added;
                }
                await _context.SaveChangesAsync();
             }else{
                var existingCon = connection.ConnectionHolders.Find(x => x.Id == connectionHolderClass.Id);
                if (existingCon == null)
                {
                    connection.ConnectionHolders.Add(connectionHolderClass);
                    _context.Entry(connection).State = EntityState.Modified;
                    _context.Entry(connectionHolderClass).State = EntityState.Added;
                   
                }

               
                    
                 

             }



              await _context.SaveChangesAsync();
            
        }

        public async Task<Chat> GetConnection(int ticketId)
        {
            var connection = await _context.Chats.AsNoTracking()
            .Include(x => x.ConnectionHolders)
            .FirstOrDefaultAsync(x => x.TicketId == ticketId);
            return connection;
            // var connection = await _chatService.getChat(ticketId);
            // return connection;

        }
    }
}