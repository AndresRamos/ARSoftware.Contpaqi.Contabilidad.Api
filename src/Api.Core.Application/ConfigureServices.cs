using System.Reflection;
using Api.Core.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Core.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return serviceCollection;
    }
}
