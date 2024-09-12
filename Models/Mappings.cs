namespace Eapproval.Models;

    public class Mappings
    {
        public static Dictionary<string, TimeSpan> PriorityResponseMap = new Dictionary<string, TimeSpan>
        {
            { "Priority 1", TimeSpan.FromMinutes(15) },
            { "Priority 2", TimeSpan.FromMinutes(45) },
            { "Priority 3", TimeSpan.FromHours(2) },
            { "Priority 4", TimeSpan.FromHours(3) },
            { "Priority 5", TimeSpan.FromHours(6) },
            { "Priority 6", TimeSpan.FromHours(24) },
        

        };

    public static Dictionary<string, TimeSpan> PriorityResolutionMap = new Dictionary<string, TimeSpan>
    {
        {"Priority 1", TimeSpan.FromMinutes(44) },
        {"Priority 2", TimeSpan.FromHours(2) },
        {"Priority 3", TimeSpan.FromHours(5) },
        {"Priority 4", TimeSpan.FromHours(24) },
        { "Priority 5", TimeSpan.FromHours(72) },
        { "Priority 6", TimeSpan.FromHours(168) },
    };


   

    
   }

