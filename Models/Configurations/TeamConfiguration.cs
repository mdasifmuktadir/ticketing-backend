


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
       
       builder.HasMany(x => x.Leaders)
       .WithMany(x => x.TeamsLeaded)
       .UsingEntity(e => e.ToTable("TeamLeaders"));


       builder.HasMany(x => x.Monitors)
       .WithMany(x => x.TeamsMonitored)
       .UsingEntity(e => e.ToTable("TeamMonitors"));


       builder.HasMany(x => x.ProblemTypes)
       .WithOne(x => x.Team)
       .HasForeignKey(x => x.TeamId)
       .OnDelete(DeleteBehavior.Cascade);


       builder.HasMany(x => x.Subordinates)
       .WithMany(x => x.TeamMembers)
       .UsingEntity(e => e.ToTable("TeamSuboridinates")); 

       builder.HasMany(x => x.Details)
       .WithOne(x => x.Team)
       .HasForeignKey(x => x.TeamId)
       .OnDelete(DeleteBehavior.Cascade);
        
   
    }



}