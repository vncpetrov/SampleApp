namespace SampleApp.DomainServices.QueryHandlers.Car.GetByUser
{
    using Domain.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Car = Domain.Models.Car;

    public class GetCarsByUserQueryHandler : IQueryHandler<GetCarsByUser, IEnumerable<Car>>
    {
        private readonly ICarDataReader carDataReader;

        public GetCarsByUserQueryHandler(ICarDataReader carDataReader)
        {
            if (carDataReader is null)
                throw new ArgumentNullException(nameof(carDataReader));

            this.carDataReader = carDataReader;
        }

        public async Task<IEnumerable<Car>> HandleAsync(GetCarsByUser query)
        {
            IEnumerable<Car> userCars = await this.carDataReader
                .GetByUserAsync(query.UserId);

            return userCars;
        }
    }
}
