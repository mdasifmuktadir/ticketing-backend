


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class ActionConfiguration : IEntityTypeConfiguration<ActionObject>
{
    public void Configure(EntityTypeBuilder<ActionObject> builder)
    {
       
       builder.HasMany(x => x.Files).WithOne(x => x.Action).HasForeignKey(x => x.ActionId).OnDelete(DeleteBehavior.NoAction);
       
        
   
    }



}