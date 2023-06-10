using Application.Services.Bills;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationDependencyInjection).Assembly;

            services.AddScoped<IBillService, BillService>();

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
