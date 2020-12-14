namespace SampleApp.Web
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using SqlDataAccess;
    using SqlDataAccess.Entities;
    using System.Text;
    using Utils;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
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


            //var appSettingsSection = this.Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            //AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            //byte[] secret = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(config =>
                {
                    config.LoginPath = "/identity/signin";
                });


            //services
            //    .AddAuthentication(config =>
            //    {
            //        config.DefaultAuthenticateScheme = Cookie.AuthenticationScheme;
            //        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(config =>
            //    {
            //        config.RequireHttpsMetadata = false;
            //        config.SaveToken = true;
            //        config.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(secret),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });

            services.AddAuthorization();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews(cfg =>
            {
                cfg.Filters.Add(new AuthorizeFilter());
            });
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

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
