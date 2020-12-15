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

        public DbSet<CarEntity> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<CarEntity>()
                .HasOne(car => car.User)
                .WithMany()
                .HasForeignKey(car => car.UserId);

            builder
                .Entity<CarEntity>()
                .Property(car => car.Id)
                .ValueGeneratedNever();

            base.OnModelCreating(builder);
        }
    }
}
