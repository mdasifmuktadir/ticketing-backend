


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.Property( h => h.Groups)
        .HasConversion(
                        v => string.Join(',', v),  
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
                    )
                    .HasColumnType("varchar(max)");


        builder.HasMany( x => x.Authored)
        .WithOne(x => x.Author)
        .HasForeignKey(x => x.AuthorId)
        .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany( x => x.TicketsAssigned)
        .WithOne(x => x.AssignedTo)
        .HasForeignKey(x => x.AssignedToId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.TicketsRaised)
        .WithOne(x => x.RaisedBy)
        .HasForeignKey(x => x.RaisedById)
        .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.TicketsCurrentlyHandled)
        .WithOne(x => x.CurrentHandler)
        .HasForeignKey(x => x.CurrentHandlerId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.TicketsHeaded)
        .WithOne(x => x.TicketingHead)
        .HasForeignKey(x => x.TicketingHeadId)
        .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(x => x.TicketsPreviouslyHandled)
        .WithOne(x => x.PrevHandler)
        .HasForeignKey(x => x.PrevHandlerId)
        .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(x => x.Conversations)
        .WithOne(x => x.From)
        .HasForeignKey(x => x.FromId)
        .OnDelete(DeleteBehavior.NoAction);

        // builder.HasMany(x => x.Notes)
        // .WithOne(x => x.TakenBy)
        // .HasForeignKey(x => x.TakenById)
        // .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(x => x.NotificationFroms)
        .WithOne(x => x.From)
        .HasForeignKey(x => x.FromId)
        .OnDelete(DeleteBehavior.NoAction);

        // builder.HasMany(x => x.NotificationTos)
        // .WithOne(x => x.To)
        // .HasForeignKey(x => x.ToId)
        // .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.ActionsRaised)
        .WithOne(x => x.RaisedBy)
        .HasForeignKey(x => x.RaisedById);

        builder.HasMany(x => x.ActionsReceived)
        .WithOne(x => x.ForwardedTo)
        .HasForeignKey(x => x.ForwardedToId);


      


        builder.HasMany(x => x.TeamsHeaded)
        .WithOne(x => x.Head)
        .HasForeignKey(x => x.HeadId)
        .OnDelete(DeleteBehavior.NoAction);







        
    
       
        
   
    }



}