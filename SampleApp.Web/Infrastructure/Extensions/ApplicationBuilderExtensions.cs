namespace SampleApp.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SqlDataAccess;

    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabase(this IApplicationBuilder appBuilder)
        {
            using var services = appBuilder
                .ApplicationServices
                .CreateScope();

            var dbContext = services
                .ServiceProvider
                .GetService<AppDbContext>();

            dbContext.Database.Migrate();
        }
    }
} 