using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViecLam.Domain.Entities;

namespace ViecLam.Infrastructure.Configurations
{
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(x => x.Image)
                .HasColumnName("image");

            builder.Property(x => x.Heading)
                .HasColumnName("heading");

            builder.Property(x => x.SubHeading)
                .HasColumnName("subHeading");

            builder.Property(x => x.BlogDate)
                .HasColumnName("blogDate");

            builder.Property(x => x.BlogDetail)
                .HasColumnName("blogDetail");

            builder.ToTable("blogs");
        }
    }
}
