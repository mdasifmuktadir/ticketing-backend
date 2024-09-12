using Eapproval.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Eapproval.Factories.IFactories;
using Eapproval.Factories.IFactories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Eapproval.Services.IServices;


namespace Eapproval.services;

public class BlogsService:IBlogService
{

    private IConnection _connection;

    private TicketContext _context;

    public BlogsService(IConnection connection, TicketContext context)
    {
        _connection = connection;
        _context = context;
      
    }

    public async Task<Blogs> GetBlog(int id)
{
         var result = await _context.Blogs.AsNoTracking()
         .Include(x => x.Author)
         .FirstOrDefaultAsync(x => x.Id == id);
        
        return result;
    }

    public async Task<List<Blogs>> GetAllBlogs()
    {
        var results = await _context.Blogs.AsNoTracking()
        .Include(x => x.Author)
        .ToListAsync();
        
        return results;
    }


    public async Task<List<Blogs>> GetBlogsForUser(User user)
    {   

       var results = await _context.Blogs
       .AsNoTracking()
       .Include( x => x.Author)
       .Where(x => x.AuthorId == user.Id).ToListAsync();
       
        return results;
    }


    public async Task InsertBlog(Blogs blog)
    {
       _context.Entry(blog).State = EntityState.Added;
       await _context.SaveChangesAsync();
    }


    public async Task DeleteBlog(Blogs blog)
    {
         _context.Entry(blog).State = EntityState.Deleted;
     
         await _context.SaveChangesAsync();
        
    }


    public async Task EditBlog(Blogs blog)
    {
        _context.Entry(blog).State = EntityState.Modified;
        _context.Entry(blog.Author).State = EntityState.Unchanged;
        await _context.SaveChangesAsync();
    }


    public async Task<List<Blogs>> GetFilteredBlogs(string searchTerm)
    {


        var results = await _context.Blogs
        .Include(b => b.Author)
        .AsNoTracking().Where(x => x.Content.Contains(searchTerm)).ToListAsync();

        return results;



        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<Blogs>("SELECT * FROM Blogs WHERE Content LIKE @SearchTerm", new { SearchTerm = "%" + searchTerm + "%" });
        
        // return result.ToList();
    }


    public async Task<List<Blogs>> GetFilteredBlogsForUser(string searchTerm, User user)
    {


         var results = await _context.Blogs
        .Include(b => b.Author)
        .AsNoTracking().Where(x => x.Content.Contains(searchTerm) && x.AuthorId == user.Id).ToListAsync();


        return results;
        // await using var connection = _connection.GetConnection();
        // await connection.OpenAsync();
        // var result = await connection.QueryAsync<Blogs>("SELECT * FROM Blogs WHERE Content LIKE @SearchTerm AND AuthorsId = @Id", new { SearchTerm = "%" + searchTerm + "%", Id = user.Id });
       
        // return result.ToList();
    }




   



}