using Eapproval.Models;
using Eapproval.Models.VMs;
using AutoMapper;

namespace Eapproval.Mappings;
public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Tickets, TicketVM>().ReverseMap();
        CreateMap<Team, TeamVM>().ReverseMap();
    }
}