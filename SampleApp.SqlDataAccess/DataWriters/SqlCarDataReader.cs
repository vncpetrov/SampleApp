namespace SampleApp.SqlDataAccess.DataWriters
{
    using Domain.Contracts;
    using Domain.Models;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SqlCarDataReader : ICarDataReader
    {
        private AppDbContext dbContext;

        public SqlCarDataReader(AppDbContext dbContext)
        {
            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Car>> GetByUserAsync(Guid userId)
        {
            List<CarEntity> carEntities = await this.dbContext
                .Cars
                .Where(car => car.UserId == userId)
                .ToListAsync();

            List<Car> userCars = new List<Car>();

            foreach (var carEntity in carEntities)
            {
                Car car = new Car(
                    colour: carEntity.Colour,
                    condition: carEntity.Condition,
                    price: carEntity.Price,
                    userId: carEntity.UserId);

                userCars.Add(car);
            }

            return userCars;
        }
    }
}
