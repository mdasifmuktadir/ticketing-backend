using System.Globalization;
using System.IO;
using Eapproval.Models;
using Eapproval.services;
using System.Text.Json;
using Eapproval.Helpers.IHelpers;
using Org.BouncyCastle.Crypto.Operators;
using Eapproval.Services.IServices;

namespace Eapproval.Helpers
{
    public class HelperClass:IHelperClass
    {
        Dictionary<string, string> departmentHeads = new Dictionary<string, string>();
        IUsersService _usersService;
        IFileHandler _fileHandler;
        ITeamsService _teamsService;

    

       
        public HelperClass(IUsersService usersService, IFileHandler fileHandler, ITeamsService teamsService) {
            this._usersService = usersService;
            this._fileHandler = fileHandler;
            this._teamsService = teamsService;

            departmentHeads.Add("Administration", "Ticketing Head Admin");
            departmentHeads.Add("ERP", "Ticketing Head ERP");
            departmentHeads.Add("Information Technology", "Ticketing Head IT");


        }





        public string GetCurrentTime()
        {
            var currentDate = DateTime.Now;
            var options = new CultureInfo("en-US").DateTimeFormat;
         
            options.ShortDatePattern = "ddd, MMM d, yyyy";
            options.ShortTimePattern = "h:mm:ss tt";
            string time = currentDate.ToString("f", options);
            return time;
        }


        public async Task<List<File2>> GetFiles(IFormCollection data)
        {
            var fileNames = new List<File2>();
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads")); ;
            if (data.Files.Count > 0)
            {

                foreach (var file in data.Files)
                {
                    var newName = _fileHandler.GetUniqueFileName(file.FileName);
                    await _fileHandler.SaveFile(path, newName, file);

                    fileNames.Add(new File2 { OriginalName = file.FileName, FileName = newName });


                }

            

            }else{
               fileNames = null;
            }
           
           return fileNames;
      

        }


        public async Task<(User user, Tickets ticket, string comment, List<File2> fileNames, string info)> GetContent(IFormCollection data)
        {
            var user = JsonSerializer.Deserialize<User>(data["user"]);
            var ticket = JsonSerializer.Deserialize<Tickets>(data["ticket"]);
            var comment = data["comment"];
            var info = data["additionalInfo"];
            var fileNames = new List<File2>();

            

            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads")); ;
            if (data.Files.Count > 0 )
            {
                
                    foreach (var file in data.Files)
                    {
                    var newName = _fileHandler.GetUniqueFileName(file.FileName);
                    await _fileHandler.SaveFile(path, newName, file);

                     fileNames.Add(new File2 { OriginalName = file.FileName, FileName = newName });


                    }

            }
            else
            {
                fileNames = null;
            }
            return (user, ticket, comment, fileNames, info);
        }  


        public async Task<ActionObject> GetAction(List<ActionObject>? Actions, User raisedBy, User? forwardedTo, string comment, ActionType action, string? info = "Not Available" , List<File2> file = null)
        {
            int? serial;
            if(Actions.Count < 1) {
                serial = 1;
            }
            else
            {
            serial = Actions[Actions.Count - 1].Serial + 1;

            }
            var actionObject = new ActionObject
            {   Serial= serial,
                Time = this.GetCurrentTime(),
                Type = action,
                RaisedBy = raisedBy,
                ForwardedTo = forwardedTo,
                AdditionalInfo = info,
                Files = file,
                Comments = comment,

            };
            return actionObject;
        }

        public async Task<List<User>?> GetTicketingHeads(Tickets ticket)
        {

            string? departmentHead;
            List<SubordinatesClass>? subordinateList = new List<SubordinatesClass>();
            
            if (ticket.HasService == false)
            {
                departmentHead = ticket.Department;
            }
            else
            {
                departmentHead = ticket.ServiceType;
            }

              

                Team? result = await _teamsService.GetTeamByName(departmentHead);


          
                var leaders = await _usersService.GetUsers(result.Leaders);


                return leaders;

       
          
        



            }          

             
              
          
            



        public async Task<List<User?>> GetSupport(Tickets ticket)
        {

            string? departmentHead;
            List<User>? subordinateList = new List<User>();
           
            departmentHead = ticket.Department;
           
           



            Team? result = await _teamsService.GetTeamByName(departmentHead);

        
            
         
                return result.Subordinates.ToList();

          




        }

    }




    }

