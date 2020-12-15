namespace SampleApp.Web.Infrastructure.Extensions
{
    using Domain.Contracts;
    using Domain.Models;
    using DomainServices.CommandHandlers.Car.Create;
    using DomainServices.QueryHandlers.Car.GetByUser;
    using DomainServices.QueryHandlers.Car.GetStatisticsByUser;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SqlDataAccess;
    using SqlDataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public static class ServiceCollectionExtensoins
    {
        public static IServiceCollection RegisterCommandHandler<TCommandHandler, TCommand>(
            this IServiceCollection services)
            where TCommand : class
            where TCommandHandler : class, ICommandHandler<TCommand>
            => services.AddScoped<ICommandHandler<TCommand>, TCommandHandler>();

        public static IServiceCollection AddDatabase(
           this IServiceCollection services,
           IConfiguration configuration)
           => services
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<UserEntity, RoleEntity>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }

        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services) 
        {
            services
                .AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    config =>
                    {
                        config.Cookie.Name = WebConstants.IdentityCookieName;
                        config.LoginPath = "/identity/signin";
                        config.Cookie.HttpOnly = true;
                    });

            return services;
        }

        public static IServiceCollection AddMvcControllers(this IServiceCollection services)
        {
            services
                .AddControllersWithViews(config =>
                {
                    config.Filters.Add(new AuthorizeFilter());
                })
                .AddRazorRuntimeCompilation();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, AspNetUserContext>();

            AddDataWriters(services);
            AddDataReaders(services);
            AddCommandHandlers(services);
            AddQueryHandlers(services);

            return services;
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.RegisterCommandHandler<CreateCarCommandHandler, CreateCarCommand>();
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IQueryHandler<GetCarsByUser, IEnumerable<Car>>,
                               GetCarsByUserQueryHandler>();

            services.AddScoped<IQueryHandler<GetStatisticsByUser, GetStatisticsByUserOutputModel>,
                               GetStatisticsByUserQueryHandler>();
        }

        private static void AddDataWriters(IServiceCollection services)
        {
            var dataWriterTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IDataWriter)));

            foreach (Type @interface in dataWriterTypes.Where(a => a.IsInterface))
            {
                Type implementation = dataWriterTypes
                    .FirstOrDefault(x => !x.IsInterface
                                         && x.GetInterfaces().Contains(@interface));

                services.AddScoped(
                    serviceType: @interface,
                    implementationType: implementation);
            }
        }

        private static void AddDataReaders(IServiceCollection services)
        {
            var dataReaderTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IDataReader)));

            foreach (Type @interface in dataReaderTypes.Where(a => a.IsInterface))
            {
                Type implementation = dataReaderTypes
                    .FirstOrDefault(x => !x.IsInterface
                                         && x.GetInterfaces().Contains(@interface));

                services.AddScoped(
                    serviceType: @interface,
                    implementationType: implementation);
            }
        }   
    }
}