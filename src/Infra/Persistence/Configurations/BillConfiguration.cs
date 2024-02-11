using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Bills;
using Model.Tags;

namespace Infra.Persistence.Configurations
{
    internal class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public const string BillTagEntityName = "BillTag";

        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired();

            builder.OwnsOne(b => b.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Currency).HasMaxLength(3);
            });

            builder.OwnsOne(b => b.Period, priceBuilder =>
            {
                priceBuilder.Property(m => m.Month).IsRequired();
                priceBuilder.Property(m => m.Year).IsRequired();
                priceBuilder.Property(m => m.MonthName).IsRequired();
            });

            builder
                .HasOne(b => b.User)
                .WithMany(u => u.Bills)
                .HasForeignKey(b => b.User.Id);

            builder
                .HasMany(e => e.Tags)
                .WithMany(e => e.Bills)
                .UsingEntity(
                    BillTagEntityName,
                    l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                    r => r.HasOne(typeof(Bill)).WithMany().HasForeignKey("BillsId").HasPrincipalKey(nameof(Bill.Id)),
                    j => j.HasKey("BillsId", "TagsId"));
        }
    }
}
