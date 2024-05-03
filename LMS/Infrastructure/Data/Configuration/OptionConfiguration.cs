using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LMS.Domain.Market.Entities;

namespace LMS.Infrastructure.Data.Configuration
{
    public class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
    {
        public void Configure(EntityTypeBuilder<OptionEntity> builder)
        {
            builder
                .Property(e => e.Type)
                .HasConversion<string>();
        }
    }
}
