


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
       builder.HasMany(x => x.Conversation).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.Cascade);
       builder.HasMany(x => x.ConnectionHolders).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.Cascade);
       
        
   
    }



}