using FinanceApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceApp.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("varchar(80)")
            .HasMaxLength(80); 

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.PaidOrReceivedAt)
            .IsRequired(false);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnType("smallint");

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("money");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("varchar(160)")
            .HasMaxLength(160); ;
    }
}
