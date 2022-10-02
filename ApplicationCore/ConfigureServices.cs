using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using ApplicationCore.Exceptions;

namespace ApplicationCore
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());          

            return services;
        }
    }
}
