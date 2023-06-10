using Infra.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Model.Bills;

namespace Infra.Persistence
{
    public class BillsContext : DbContext
    {
        public BillsContext(DbContextOptions<BillsContext> options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillConfiguration).Assembly);
        }
    }
}
