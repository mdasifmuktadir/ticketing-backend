using Eapproval.services;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Core.Connections;
using Eapproval.Models;
using System.Text.Json;
using Eapproval.Helpers;
using Eapproval.Services;
using Humanizer;

namespace Eapproval.signalR;

public class ChatHub:Hub
{

    private IChatService _chatService;
    private ITicketsService _ticketsService;
    public IConnectionService _connectionsService;
    private IHelperClass _helperClass;
    private IFileHandler _fileHandler;
    private INotificationService _notificationsService;

    public ChatHub (INotificationService notificationsService, IChatService chatService, ITicketsService ticketsService, IConnectionService connectionsService, IHelperClass helperClass, IFileHandler fileHandler)
    {
        _chatService = chatService;
        _ticketsService = ticketsService;
        _connectionsService = connectionsService;
        _helperClass = helperClass;
        _fileHandler = fileHandler;
        _notificationsService = notificationsService;
    }

    public async Task SendMessage(string message, string user, int ticketId)
    {
 

      

        var from = JsonSerializer.Deserialize<User>(user);

        var time = _helperClass.GetCurrentTime();


        var newMessage = new ConversationClass()
        {
            From = from,
            Message = message,
            Time = time,
        };
        var messageString = JsonSerializer.Serialize(newMessage);


        var connection = await _connectionsService.GetConnection(ticketId);
        
        connection.Conversation.Add(newMessage);
        await _chatService.UpdateChat(connection.Id, connection);

        foreach(var x in connection.ConnectionHolders)
        {
            Console.WriteLine("connections send");
            Console.WriteLine(x);
        
            await Clients.Client(x.ConnectionId).SendAsync("Receive", messageString);
        }

     

    }



    public async Task UploadFile(string files, string user, int ticketId)
    {
        var id = Context.ConnectionId;



        var from = JsonSerializer.Deserialize<User>(user);

        var time = _helperClass.GetCurrentTime();

        var fileNames = JsonSerializer.Deserialize<List<File2>>(files);

        var newMessage = new ConversationClass()
        {
            From = from,
            Message = null,
            Time = time,
            Type = "files",
            Files = fileNames

        };
        var messageString = JsonSerializer.Serialize(newMessage);

        Console.WriteLine(messageString);

        var connection = await _connectionsService.GetConnection(ticketId);
        connection.Conversation.Add(newMessage);
        await _chatService.UpdateChat(connection.Id, connection);

        foreach (var x in connection.ConnectionHolders)
        {
            Console.WriteLine("connections send");
            Console.WriteLine(x);
            await Clients.Client(x.ConnectionId).SendAsync("Receive", messageString);
        }

   


    }





    public async Task Subscribe(int ticketId, string name)
    {

        
        var id = Context.ConnectionId;

        var newConnectionHolder = new ConnectionHolderClass()
        {
            ConnectionId = id,
            Name = name,
        };

        await _connectionsService.AddConnection(ticketId, newConnectionHolder);

     }


    public async Task SendNotificationFromClient(string message, string from, string to, int ticketId)
    {

        var From = JsonSerializer.Deserialize<User>(from);
        var To = JsonSerializer.Deserialize<User>(to);
            

        var newNotification = new Notification()
        {
            Time = _helperClass.GetCurrentTime(),
            Message = message,
            FromId = From.Id,
            ToId = To.Id,
            TicketId = ticketId,
            Type="chat"
        };

        await _notificationsService.InsertNotification(newNotification); 

        var notificationString = JsonSerializer.Serialize(newNotification);

        var connection = await _connectionsService.GetConnection(ticketId);


        foreach(var x in connection.ConnectionHolders)
        {
            if(x.Name == To.EmpName)
            {
                Console.WriteLine(x.Name);
                await Clients.Client(x.Id.ToString()).SendAsync("NotificationReceive", notificationString);
            }
        }


        
    }


    public async Task MakeCall(string name, string TicketId, string callerId)
    {
        var connection = await _connectionsService.GetConnection(int.Parse(TicketId));

        foreach (var x in connection.ConnectionHolders)
        {
            if (x.Name == name)
            {
                Console.WriteLine(x.Name);
                await Clients.Client( x.Id.ToString()).SendAsync("CallAlert", callerId);
            }
        }


    }




   
    
}
