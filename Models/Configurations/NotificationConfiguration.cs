


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
      builder
      .Property(x => x.Mentions)
      .HasConversion(
                   v => string.Join(',', v),  
                   v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
               )
               .HasColumnType("varchar(max)");
       
        
   
    }



}