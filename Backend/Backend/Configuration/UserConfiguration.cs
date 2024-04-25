using Backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configuration
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email)
                   .HasMaxLength(60)
                   .IsRequired();
            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.Property(x => x.Name)
                   .HasMaxLength(20)
                   .IsRequired();
           

            builder.Property(x => x.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

            

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(150);
        }
    }
}
