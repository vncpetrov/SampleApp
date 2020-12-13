namespace SampleApp.SqlDataAccess
{
    using Entities; 
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
