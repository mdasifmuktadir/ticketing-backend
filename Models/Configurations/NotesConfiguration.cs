


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class NotesConfiguration : IEntityTypeConfiguration<Notes>
{
    public void Configure(EntityTypeBuilder<Notes> builder)
    {
       builder.HasMany(x => x.Files).WithOne(x => x.Note).HasForeignKey(x => x.NoteId).OnDelete(DeleteBehavior.NoAction);
       builder.Property(x => x.Mentions)
         .HasConversion(
              v => string.Join(',', v),
              v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
         );
       
       
        
   
    }



}