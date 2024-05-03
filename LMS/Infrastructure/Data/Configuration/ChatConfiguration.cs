using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LMS.Domain.Messaging.Entities;

namespace LMS.Infrastructure.Data.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder
                .Property(e => e.Type)
                .HasConversion<string>();
            builder
                .HasMany(e => e.Participants)
                .WithMany(e => e.Chats);
        }
    }
}
