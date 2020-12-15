namespace SampleApp.DomainServices.QueryHandlers.Car.GetStatisticsByUser
{
    using Domain.Contracts;
    using SampleApp.Domain.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Car = Domain.Models.Car;

    public class GetStatisticsByUserQueryHandler
        : IQueryHandler<GetStatisticsByUser, GetStatisticsByUserOutputModel>
    {
        private readonly ICarDataReader carDataReader;

        public GetStatisticsByUserQueryHandler(ICarDataReader carDataReader)
        {
            if (carDataReader is null)
                throw new ArgumentNullException(nameof(carDataReader));

            this.carDataReader = carDataReader;
        }

        public async Task<GetStatisticsByUserOutputModel> HandleAsync(GetStatisticsByUser query)
        {
            IEnumerable<Car> userCars = await this.carDataReader
                .GetByUserAsync(query.UserId);

            GetStatisticsByUserOutputModel outputModel = new GetStatisticsByUserOutputModel();

            if (userCars.Any())
            {
                outputModel.AverageCarPrice =
                                (int)(((double)userCars.Sum(c => c.Price)) / userCars.Count());

                outputModel.PercentageOfNewCars =
                    (int)((double)userCars.Count(c => c.Condition == CarCondition.New) / userCars.Count() * 100);
            }

            foreach (var colour in Enum.GetValues<CarColour>())
            {
                int carsCountOfColor = userCars.Count(c => c.Colour == colour);

                if (carsCountOfColor > 0)
                {
                    int percentage = (int)((double)carsCountOfColor / userCars.Count() * 100);
                    outputModel.PercentagesByCarColour.Add(colour, percentage);
                }
            }

            return outputModel;
        }
    }
}
