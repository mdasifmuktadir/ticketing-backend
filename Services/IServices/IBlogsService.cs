
using Eapproval.Models;
namespace Eapproval.Services.IServices;



public interface IBlogService{
    Task<Blogs> GetBlog(int id);
    Task<List<Blogs>> GetAllBlogs();
    Task<List<Blogs>> GetBlogsForUser(User user);
    Task InsertBlog(Blogs blog);
    Task DeleteBlog(Blogs blog);

    Task EditBlog(Blogs blog);
    Task<List<Blogs>> GetFilteredBlogs(string searchTerm);
    Task<List<Blogs>> GetFilteredBlogsForUser(string searchTerm, User user);

    


}