using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceApp.Api.Data.Mappings.Identity;

public class IdentityRoleMapping : IEntityTypeConfiguration<IdentityRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<long>> builder)
    {
        builder.ToTable("IdentityRole");

        builder.HasKey(r => r.Id);

        builder.HasIndex(r => r.NormalizedName).IsUnique();
        builder.Property(r => r.NormalizedName).HasMaxLength(255);  
        builder.Property(r => r.Name).HasMaxLength(255);
        builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
    }
}
