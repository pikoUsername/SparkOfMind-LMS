using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LMS.Domain.Payment.Entities;

namespace LMS.Infrastructure.Data.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder
                .Property(e => e.Status)
                .HasConversion<string>();
            builder
                .Property(e => e.Direction)
                .HasConversion<string>();
            builder
                .Property(e => e.Operation)
                .HasConversion<string>();
        }
    }
}
