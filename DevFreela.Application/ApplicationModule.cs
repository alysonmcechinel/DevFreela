using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddServices()
                .AddHandlers();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // services.AddScoped<IProjectService, ProjectService>(); não esta sendo mais usado
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());

            services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateProjectCommandBehavior>();

            return services;
        }

        // comments
        // builder.Services.AddSingleton<IConfigService, ConfigService>(); config injeção de depedencia
        // builder.Services.AddScoped<IConfigService, ConfigService>(); config injeção de depedencia
        // se não for uma aplicação muito grande deixar na Program.cs / Start.cs
    }
}
