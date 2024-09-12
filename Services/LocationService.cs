using Eapproval.Models;
using Eapproval.Factories.IFactories;
using MongoDB.Driver;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Eapproval.Services.IServices;

namespace Eapproval.Services
{
    public class LocationService:ILocationService
    {

        private readonly IConnection _connection;
        private TicketContext _context;

        public LocationService(IConnection connection, TicketContext context)
        {
            _connection = connection;
            _context = context;
        }


        public async Task<List<Location>> GetAllLocations(){
            var results = await _context.Locations.AsNoTracking().ToListAsync();
            return results;


            // await using var connection = _connection.GetConnection();
            // await connection.OpenAsync();
            // var result = await connection.QueryAsync<Location>("SELECT * FROM Locations");
            // return result.ToList();
        }
       


        public async Task AddLocation(Location location)
        {
           
           _context.Entry(location).State = EntityState.Added;
            await _context.SaveChangesAsync();

            // await using var connection = _connection.GetConnection();
            // await connection.OpenAsync();
            // await connection.ExecuteAsync("INSERT INTO Locations (Id, Name, Address, City, Country, ZipCode, Latitude, Longitude) VALUES (@Id, @Name, @Address, @City, @Country, @ZipCode, @Latitude, @Longitude)", location);
        }


        public async Task DeleteLocation(int id)
        {    var locationToDelete = new Location(){Id = id};
            _context.Entry(locationToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }






    }
}
