namespace SampleApp.SqlDataAccess.DataWriters
{
    using Domain.Contracts;
    using Domain.Models;
    using Entities;
    using System;
    using System.Threading.Tasks;

    public class SqlCarDataWriter : ICarDataWriter
    {
        private AppDbContext dbContext;

        public SqlCarDataWriter(AppDbContext dbContext)
        {
            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Guid id, Car car)
        {
            if (car is null)
                throw new ArgumentNullException(nameof(car));

            CarEntity carEntity = new CarEntity(id)
            {
                 Colour = car.Colour,
                 Condition = car.Condition,
                 Price = car.Price,
                 UserId = car.UserId
            };

            await this.dbContext
                .Cars
                .AddAsync(carEntity);

            await this.dbContext
                .SaveChangesAsync();
        }
    }
}
