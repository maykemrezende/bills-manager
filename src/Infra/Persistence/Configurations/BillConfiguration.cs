using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Bills;

namespace Infra.Persistence.Configurations
{
    internal class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
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
        }
    }
}
