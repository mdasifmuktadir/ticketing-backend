using Eapproval.DatabaseSettings;
using Eapproval.services;
using Eapproval.Helpers;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Eapproval.Models;
using System.Text.Json;
using Newtonsoft;
using System.Text.Json.Serialization;
using Eapproval.Services;
using Eapproval.signalR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Eapproval.Factories;
using Eapproval.Factories.IFactories;
using AutoMapper;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;








var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddControllers();

//services
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<INotesService, NotesService>();
builder.Services.AddScoped<CounterService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITicketsService, TicketsService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IBlogService, BlogsService>();
builder.Services.AddScoped<IConnectionService, ConnectionsService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
// builder.Services.AddScoped<IPriorityService, PriorityService>();





//helpers
builder.Services.AddScoped<TicketSupportService>();

builder.Services.AddScoped<IHelperClass, HelperClass>();
builder.Services.AddScoped<IFileHandler, FileHandler>();
builder.Services.AddScoped<INotifier, Notifier>();
builder.Services.AddScoped<IUserApi, UserApi>();
builder.Services.AddScoped<IMailTicket, MailTicket>();
builder.Services.AddScoped<ITicketMailer, TicketMailer>();
builder.Services.AddScoped<IHasher, Hasher>();




// connection
builder.Services.AddScoped<IConnection, Connection>(provider => new Connection(
    builder.Configuration.GetConnectionString("DefaultConnection")
));


//dbcontext
builder.Services.AddTransient<TicketContext>();


//others
builder.Services.AddCors((options) =>
{
    options.AddPolicy("FeedClientApp",
        new CorsPolicyBuilder()
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build());
});

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(p =>
{
    var key = Encoding.UTF8.GetBytes("secretKeyadfsssssssssssssssssssssssweewfewwwwwwwwwwwwwwwwwwwwwwwwwwwwwweqeqwewqeqweqweqweqweqwe");
   
    p.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.RequireClaim("userType", "admin"));
    options.AddPolicy("normal", policy => policy.RequireClaim("userType", "normal"));
    options.AddPolicy("support", policy => policy.RequireClaim("userType", "support"));
    options.AddPolicy("leader", policy => policy.RequireClaim("userType", "leader"));
    options.AddPolicy("power", policy => policy.RequireClaim("userType", "power"));
    options.AddPolicy("powerDepartment", policy => policy.RequireClaim("userType", "departmentPower"));
    options.AddPolicy("test", policy => policy.RequireClaim("empName", "Rabiul Islam"));
    options.AddPolicy("allpower", policy => policy.RequireClaim("userType", "departmentPower", "power", "admin"));
    options.AddPolicy("allHeads", policy => policy.RequireClaim("userType", "Ticket Manager (Department)", "leader"));
    
    
}); 



builder.Services.AddMvcCore().AddJsonOptions( x =>  x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles );

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(Program)); 




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseDefaultFiles();



app.UseHttpsRedirection();


app.UseRouting();
app.UseCors("FeedClientApp");
app.UseAuthentication();
app.UseAuthorization();







app.MapHub<ChatHub>("/chat");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();
