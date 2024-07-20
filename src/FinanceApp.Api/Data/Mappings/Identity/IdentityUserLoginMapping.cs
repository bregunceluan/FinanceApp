using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceApp.Api.Data.Mappings.Identity;

public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder.ToTable("IdentityUserLogin");
        builder.HasKey(k=> new {k.LoginProvider, k.ProviderKey });

        builder.Property(k => k.ProviderKey).HasMaxLength(128);
        builder.Property(k => k.LoginProvider).HasMaxLength(128);
        builder.Property(k => k.ProviderDisplayName).HasMaxLength(255);
    }
}
