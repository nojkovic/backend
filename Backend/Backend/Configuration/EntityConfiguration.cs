using Backend.Data_Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configuration
{
    public abstract class EntityConfiguration<T> :IEntityTypeConfiguration<T> 
        where T : Entity
    {
        #region Methods
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            ConfigureEntity(builder);

        }
        protected abstract void ConfigureEntity(EntityTypeBuilder<T>builder);
        #endregion
    }
}
