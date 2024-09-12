using Microsoft.EntityFrameworkCore;

using Eapproval.Models.Configurations;




namespace Eapproval.Models;





public class  TicketContext: DbContext
{   

    static readonly string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=Ticketing;Trusted_Connection=true;TrustServerCertificate=True";


    public DbSet<Blogs> Blogs { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Notes> Notes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Tickets> Tickets { get; set; }

    // public DbSet<PriorityClass> Priorities {get; set;}



    // public DbSet<SubordinatesClass> Subordinates { get; set; }
    
   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Replace "YourConnectionString" with your MySQL connection string
            

         
        //  optionsBuilder.EnableSensitiveDataLogging();t
         optionsBuilder.UseSqlServer(ConnectionString);
   

  
      
    }





    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

    
      modelBuilder.ApplyConfiguration(new BlogsConfiguration());
      modelBuilder.ApplyConfiguration(new ConversationConfiguration());
      modelBuilder.ApplyConfiguration(new ConnectionConfiguration());
      modelBuilder.ApplyConfiguration(new ChatConfiguration());
      modelBuilder.ApplyConfiguration(new LocationConfiguration());
      modelBuilder.ApplyConfiguration(new NotesConfiguration());
      modelBuilder.ApplyConfiguration(new MentionsConfiguration());
      modelBuilder.ApplyConfiguration(new NotificationConfiguration());
      modelBuilder.ApplyConfiguration(new FileConfiguration());
      modelBuilder.ApplyConfiguration(new ActionConfiguration());
      modelBuilder.ApplyConfiguration(new ProblemConfiguration());
      // modelBuilder.ApplyConfiguration(new SubordinateConfiguration());
      modelBuilder.ApplyConfiguration(new TeamConfiguration());
      // modelBuilder.ApplyConfiguration(new PriorityConfiguration());
      modelBuilder.ApplyConfiguration(new DetailsConfiguration());
      modelBuilder.ApplyConfiguration(new TicketConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration()); 



 


      
   

      




  




        


        base.OnModelCreating(modelBuilder);

       
    }
}