


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class ConversationConfiguration : IEntityTypeConfiguration<ConversationClass>
{
    public void Configure(EntityTypeBuilder<ConversationClass> builder)
    {
       
       builder.HasMany(x => x.Files).WithOne(x => x.Conversation).HasForeignKey(x => x.ConversationId).OnDelete(DeleteBehavior.Cascade);
       
        
   
    }



}