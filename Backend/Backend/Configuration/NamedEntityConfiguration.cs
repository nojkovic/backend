using Backend.Data_Access;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configuration
{
    public abstract class NamedEntityConfiguration<T> : EntityConfiguration<T>
        where T : NamedEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                   .IsUnique();    
        }
    }
}
