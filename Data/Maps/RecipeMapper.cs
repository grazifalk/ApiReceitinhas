using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps
{
    public class RecipeMapper : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipe");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").UseIdentityColumn();
            builder.Property(x => x.Title).HasColumnName("Title");
            builder.Property(x => x.Photo).HasColumnName("Photo");
            builder.Property(x => x.PreparationMethod).HasColumnName("PreparationMethod");
            builder.Property(x => x.PreparationTime).HasColumnName("PreparationTime");
            builder.Property(x => x.Cost).HasColumnName("Cost");
            builder.Property(x => x.Difficulty).HasColumnName("Difficulty");

            builder.HasOne(x => x.Category).WithMany(x => x.Recipes).HasForeignKey(x => x.CategoryId);

        }
    }
}