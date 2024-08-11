
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.config
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.PublicationDate).IsRequired();

            builder
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Articles)
            .HasForeignKey(a => a.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Status).WithMany(s => s.Articles).HasForeignKey(a => a.StatusId);
            builder.HasOne(s => s.Category).WithMany(s => s.Articles).HasForeignKey(a => a.CategoryId);
            builder.HasMany(c => c.Comments).WithOne(c => c.Article).HasForeignKey(a => a.ArticleId);
        }
    }
}