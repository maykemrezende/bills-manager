using Infra.Persistence;
using Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Bills;
using Model.Tags;

namespace Infra
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BillsContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("BillsDatabase")));

            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }
    }
}
