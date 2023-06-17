using Infra.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Model.Bills;
using Model.Tags;

namespace Infra.Persistence
{
    public class BillsContext : DbContext
    {
        public BillsContext(DbContextOptions<BillsContext> options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillConfiguration).Assembly);
        }
    }
}
