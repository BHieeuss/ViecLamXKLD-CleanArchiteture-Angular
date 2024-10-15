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

            builder.Property(x => x.ProductImage)
                .HasColumnName("ProductImage");

            builder.Property(x => x.Heading)
                .HasColumnName("Heading");

            builder.Property(x => x.SubHeading)
                .HasColumnName("SubHeading");

            builder.Property(x => x.Poster)
                .HasColumnName("Poster");

            builder.Property(x => x.BlogDate)
                .HasColumnName("BlogDate");

            builder.Property(x => x.BlogDetail)
                .HasColumnName("BlogDetail");

            builder.Property(x => x.ProductName)
                .HasColumnName("ProductName")
                .IsRequired();

            builder.ToTable("Blog");
        }
    }
}
