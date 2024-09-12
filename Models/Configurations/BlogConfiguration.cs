


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class BlogsConfiguration : IEntityTypeConfiguration<Blogs>
{
    public void Configure(EntityTypeBuilder<Blogs> builder)
    {
       
       builder.HasOne(x => x.Author)
       .WithMany(x => x.Authored)
       .HasForeignKey(x => x.AuthorId)
       .OnDelete(DeleteBehavior.NoAction);
        
   
    }



}