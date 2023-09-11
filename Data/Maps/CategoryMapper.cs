using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps
{
    public class CategoryMapper : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName("Name");

            // Relacionamento um-para-muitos com Recipe
            builder.HasMany(x => x.Recipes).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
