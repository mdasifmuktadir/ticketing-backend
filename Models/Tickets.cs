using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.ComponentModel.DataAnnotations;

namespace Eapproval.Models;


    public class Tickets
    {
       
        [JsonPropertyName("_id")]
        [Key]
        public int Id { get; set; } 

       
        // Dictionary<string, TimeSpan> PriorityMap = new Dictionary<string, TimeSpan>();
        
    
    
        [JsonPropertyName("category")]
        public string Category { get; set; } = "Not Available";
        


   
        [JsonPropertyName("number")]
        public int Number { get; set; }

   
        [JsonPropertyName("department")]
        public string? Department { get; set; } = "Not Available";


        [JsonPropertyName("problemDetails")]
        public string? ProblemDetails { get; set; } = "Not Available";



  
        [JsonPropertyName("assigned")]
        public bool? Assigned { get; set; } = false;



        [JsonPropertyName("hasService")]
        public bool? HasService { get; set; } = false;


        [JsonPropertyName("location")]
        public string? Location { get; set; }



        [JsonPropertyName("phone")]
        public string? Phone { get; set; }


        [JsonPropertyName("extension")]
        public string? Extension { get; set; }



        [JsonPropertyName("email")]
        public string? Email { get; set; }


       
        
    // [JsonPropertyName("priorityId")]
    // public int? PriorityId { get; set; } 

        [JsonPropertyName("priority")]
        public PriorityClass? Priority { get; set; } = new PriorityClass();

    


        [JsonPropertyName("approvalRequired")]
        public bool? ApprovalRequired { get; set; } = false;


        // [JsonPropertyName("raisedForId")]
        // public int? RaisedForId { get; set; }

     
        // [JsonPropertyName("raisedFor")]
        // public virtual User? RaisedFor { get; set; } = new User();

      
        [JsonPropertyName("remarks")]
        public string? Remarks { get; set; } = "Not Available";


   
        [JsonPropertyName("closeRequested")]
        public bool? CloseRequested { get; set; } = false;


        // [JsonPropertyName("supervisorId")]
        // public int? SupervisorId { get; set; } = null;

        
        // [JsonPropertyName("supervisor")]
        // public virtual User? Supervisor { get; set; } = null;


        [JsonPropertyName("assignedToId")]
        public int? AssignedToId { get; set; } 

     
        [JsonPropertyName("assignedTo")]
        public virtual User? AssignedTo { get; set; } 


        // [JsonPropertyName("higherApproverId")]
        // public int? HigherApproverId { get; set; } = null;



        // [JsonPropertyName("higherApprover")]
        // public virtual User? HigherApprover { get; set; } = null;


        [JsonPropertyName("files")]
        public virtual List<File2>? Files { get; set; } 


        [JsonPropertyName("ticketType")]
        public string? TicketType { get; set; } = "Incident";


       
        [JsonPropertyName("raisedById")]
        public int? RaisedById { get; set; }


        [JsonPropertyName("raisedBy")]
        public  virtual User? RaisedBy { get; set; } 

  
        [JsonPropertyName("status")]
        public string? Status { get; set; } = "Not Available";




        [JsonPropertyName("prevStatus")]
        public string? PrevStatus { get; set; } = "Not Available";


        [JsonPropertyName("ask")]
        public bool? Ask { get; set; } 



        [JsonPropertyName("type")]
        public string? Type { get; set; } 


        


        [JsonPropertyName("infos")]
        public  List<string>? Infos { get; set; }

        [BsonElement("requestDate")]
        [JsonPropertyName("requestDate")]
        public string? RequestDate { get; set; } = "Not Available";


             
        [JsonPropertyName("currentHandlerId")]
        public int? CurrentHandlerId { get; set; }



        
        [JsonPropertyName("currentHandler")]
        public virtual User? CurrentHandler { get; set; } 


          
        [JsonPropertyName("ticketingHeadId")]
        public int? TicketingHeadId { get; set; }
       



        [JsonPropertyName("ticketingHead")]
        public virtual User? TicketingHead { get; set; }



        
        [JsonPropertyName("actions")]
        public virtual List<ActionObject>? Actions { get; set; }


        [JsonPropertyName("prevHandlerId")]
        public int? PrevHandlerId { get; set; } 

        

        [JsonPropertyName("prevHandler")]
        public virtual User? PrevHandler { get; set; } 


 
        [JsonPropertyName("serviceType")]
        public string? ServiceType { get; set; } = "Not Available";


      
        [JsonPropertyName("madeCloseRequest")]
        public bool? MadeCloseRequest { get; set; } = false;


     
        [JsonPropertyName("beenRejected")]
        public bool? BeenRejected { get; set; } = false;




        [JsonPropertyName("accepted")]
        public bool? Accepted { get; set; } = false;




   
        [JsonPropertyName("details")]
        public virtual List<DetailsClass>? Details { get; set; } 



        [JsonPropertyName("mentions")]
        public List<string>? Mentions { get; set; } 



        [JsonPropertyName("users")]
        public List<string>? Users { get; set; } 


   
        [JsonPropertyName("timesRaised")]
        public int TimesRaised {get; set;} = 1;




        [JsonPropertyName("genesis")]
        public bool Genesis {get; set;}


        
        [JsonPropertyName("genesisId")]
        public int? GenesisId {get; set;} 



   
        [JsonPropertyName("initialType")]
        public string? InitialType {get; set;}


        

        [JsonPropertyName("initialLocation")]
        public string? InitialLocation {get; set;}


        [JsonPropertyName("initialPriortiyId")]
        public int? InitialPriorityId {get; set;}
        

        [JsonPropertyName("initialPriority")]
        public virtual PriorityClass? InitialPriority {get; set;}


  
        [JsonPropertyName("source")]
        public string? Source {get; set;}



        [JsonPropertyName("chats")]
        public virtual List<Chat>? Chats {get; set;}



        [JsonPropertyName("notes")]
        public virtual List<Notes>? Notes {get; set;}



        [JsonPropertyName("notifications")]
        public virtual List<Notification>? Notifications {get; set;}







    }







