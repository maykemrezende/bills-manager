using Infra.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Bills;
using Model.Tags;
using Model.Users;

namespace Infra.Persistence
{
    public class BillsContext(DbContextOptions<BillsContext> options) :
        IdentityDbContext<User>(options)
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
