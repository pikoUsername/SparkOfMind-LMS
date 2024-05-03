using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LMS.Domain.Payment.Entities;

namespace LMS.Infrastructure.Data.Configuration
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<PurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseEntity> builder)
        {
            builder
                .Property(e => e.Status)
                .HasConversion<string>();
            builder
                .Property(e => e.Currency)
                .HasConversion<string>();
        }
    }
}
