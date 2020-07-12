using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RIPE.Application.HandlersValidators;
using System;

namespace RIPE.IoC
{
    public static class HandlersExtension
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            const string ASSEMBLY_NAME = "RIPE.Application";
            var assembly = AppDomain.CurrentDomain.Load(ASSEMBLY_NAME);
            AssemblyScanner.FindValidatorsInAssembly(assembly)
                           .ForEach(v => services.AddScoped(v.InterfaceType, v.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            services.AddMediatR(assembly);

            return services;
        }
    }
}
