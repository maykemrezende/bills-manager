using Application.Services.Bills;
using Application.Services.Emails;
using Application.Services.Tags;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Model.Users;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationDependencyInjection).Assembly;

            services.AddScoped<IBillService, BillService>();
            services.AddScoped<ITagService, TagService>();
            services.AddSingleton<IEmailSender<User>, EmailSender>();

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
