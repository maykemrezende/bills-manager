using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Tags;

namespace Infra.Persistence.Configurations
{
    internal class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Code).IsRequired();
        }
    }
}
