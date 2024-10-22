using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ViecLam.Domain.Entities;

namespace ViecLam.Infrastructure.Configurations
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SenderName)
                .HasColumnName("SenderName");

            builder.Property(x => x.Content)
                .HasColumnName("Content");

            builder.Property(x => x.SentAt)
                .HasColumnName("SentAt");

            builder.ToTable("Messages");
        }
    }
}
