using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace IntegrationTests.Fixtures
{
    public class PostgresTestContainerFixture : IAsyncLifetime
    {
        public PostgreSqlContainer PostgreSqlContainer { get; private set; } = default!;
        public BillsContext Context { get; private set; } = default!;

        public async Task InitializeAsync()
        {
            PostgreSqlContainer = new PostgreSqlBuilder()
                .WithDatabase("TestDB")
                .WithPassword("Test1234")
                .WithImage("postgres:latest")
                .Build();

            await PostgreSqlContainer.StartAsync();

            var connectionString = PostgreSqlContainer.GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<BillsContext>();
            optionsBuilder.UseNpgsql(connectionString);

            Context = new BillsContext(optionsBuilder.Options);

            await Context.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await PostgreSqlContainer.DisposeAsync();

            await Context.DisposeAsync();
        }
    }
}
