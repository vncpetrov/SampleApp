namespace SampleApp.Web
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SampleApp.Domain.Contracts;
    using SampleApp.DomainServices.CommandHandlers.Car.Create;
    using SampleApp.Web.Infrastructure;
    using SqlDataAccess;
    using SqlDataAccess.Entities;
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
    using Utils;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

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

            services.AddAuthorization();

            services.AddTransient<IUserContext, AspNetUserContext>();
            AddDataWriters(services);
            AddCommandHandlers(services);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddControllersWithViews(cfg =>
                {
                    cfg.Filters.Add(new AuthorizeFilter());
                })
                .AddRazorRuntimeCompilation(); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.MigrateDatabase();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.RegisterCommandHandler<CreateCarCommandHandler, CreateCarCommand>();
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
                                         && x.IsAssignableFrom(@interface));

                services.AddScoped(
                    serviceType: @interface,
                    implementationType: implementation);
            }
        }
    }
}
