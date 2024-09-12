using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Eapproval.Models
{
   

   
    


   
    public enum ActionType
    {
        [EnumMember(Value = "TicketRaised")]  //0
        TicketRaised,
        
        [EnumMember(Value = "SupervisorApprove")] //1
        SupervisorApprove,

        [EnumMember(Value = "SeekingHigherApproval")] //2
        SeekingHigherApproval,

        [EnumMember(Value = "HigherApprove")] //3
        HigherApprove,

        [EnumMember(Value = "AssignSelf")] //4
        AssignSelf,

        [EnumMember(Value = "AssignOther")] //5
        AssignOther,

        [EnumMember(Value = "AskInfo")] //6
        AskInfo,

        [EnumMember(Value = "GiveInfo")] //7
        GiveInfo,

        [EnumMember(Value = "Reject")] //8
        Reject,

        [EnumMember(Value = "Reassign")] //9
        Reassign,

        [EnumMember(Value = "CloseRequest")] //10
        CloseRequest,

        [EnumMember(Value = "TicketClosed")] //11
        TicketClosed,


        [EnumMember(Value = "CloseRequestReject")] //12
        CloseRequestReject,

        [EnumMember(Value = "Unassigned")] //13
        Unassigned,

        [EnumMember(Value = "rated")] //14
        Rated,



        [EnumMember(Value = "reOpen")] //15
        ReOpen
    }



   

}
