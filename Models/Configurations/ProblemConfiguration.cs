


using Microsoft.EntityFrameworkCore;
using Eapproval.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eapproval.Models.Configurations;

public class ProblemConfiguration : IEntityTypeConfiguration<ProblemTypesClass>
{
    public void Configure(EntityTypeBuilder<ProblemTypesClass> builder)
    {
       
       builder.Property(e => e.Subs)
                .HasConversion(
                    v => string.Join(',', v),  // Convert list of strings to a comma-separated string
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  // Convert the string back to a list of strings
                )
                .HasColumnType("varchar(max)");
        
   
    }



}