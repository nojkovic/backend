using Backend.Data_Access;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configuration
{
    public class RoleConfiguration : NamedEntityConfiguration<Role>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
        {
            
        }
    }
}
