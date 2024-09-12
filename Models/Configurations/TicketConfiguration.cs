


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Eapproval.Models.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Tickets>
{
    public void Configure(EntityTypeBuilder<Tickets> builder)
    {
       
       builder.HasOne(x => x.CurrentHandler)
       .WithMany(x => x.TicketsCurrentlyHandled)
       .HasForeignKey(x => x.CurrentHandlerId)
       .OnDelete(DeleteBehavior.NoAction);

         builder.HasOne(x => x.AssignedTo)
       .WithMany(x => x.TicketsAssigned)
       .HasForeignKey(x => x.AssignedToId)
       .OnDelete(DeleteBehavior.NoAction);


         builder.HasOne(x => x.PrevHandler)
       .WithMany(x => x.TicketsPreviouslyHandled)
       .HasForeignKey(x => x.PrevHandlerId)
       .OnDelete(DeleteBehavior.NoAction);
    


        builder.HasMany(x => x.Files)
        .WithOne(x => x.Ticket)
        .HasForeignKey(x => x.TicketId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Infos)
         .HasConversion(
                    v => string.Join(',', v),  
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
                )
                .HasColumnType("varchar(max)");


        builder.HasMany(x => x.Actions)
        .WithOne(x => x.Ticket)
        .HasForeignKey(x => x.TicketId)
        .OnDelete(DeleteBehavior.Cascade);

        // builder.Property(x => x.Groups)
        //     .HasConversion(
        //                 v => string.Join(',', v),  
        //                 v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
        //             )
        //             .HasColumnType("varchar(max)");

        builder.HasMany(x => x.Details)
        .WithOne(x => x.Ticket)
        .HasForeignKey(x => x.TicketId)
        .OnDelete(DeleteBehavior.NoAction);


       
       builder.Property(x => x.Mentions)
       .HasConversion(
                    v => string.Join(',', v),  
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
                )
                .HasColumnType("varchar(max)");


          builder.Property(e => e.Priority)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<PriorityClass>(v, JsonSerializerOptions.Default)
                );

              builder.Property(e => e.InitialPriority)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<PriorityClass>(v, JsonSerializerOptions.Default)
                );

        builder.Property(x => x.Users)
        .HasConversion(
                    v => string.Join(',', v),  
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
                )
                .HasColumnType("varchar(max)");



        builder.HasMany(x => x.Chats)
        .WithOne(x => x.Ticket)
        .HasForeignKey(x => x.TicketId)
        .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(x => x.Notes)
        .WithOne(x => x.Ticket)
        .HasForeignKey(x => x.TicketId)
        .OnDelete(DeleteBehavior.Cascade);


        // builder.HasMany(x => x.Notifications)
        // .WithOne(x => x.Ticket)
        // .HasForeignKey(x => x.TicketId)
        // .OnDelete(DeleteBehavior.Cascade);

        


             
    }



}