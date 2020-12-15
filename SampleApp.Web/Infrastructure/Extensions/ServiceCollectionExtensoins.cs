namespace SampleApp.Web.Infrastructure.Extensions
{
    using Domain.Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensoins
    {
        public static IServiceCollection RegisterCommandHandler<TCommandHandler, TCommand>(
            this IServiceCollection services)
            where TCommand : class
            where TCommandHandler : class, ICommandHandler<TCommand>
        {  
            services.AddScoped<ICommandHandler<TCommand>, TCommandHandler>();

            return services;
        }
    }
}
