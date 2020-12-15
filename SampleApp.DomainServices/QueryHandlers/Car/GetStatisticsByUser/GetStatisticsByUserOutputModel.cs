namespace SampleApp.DomainServices.QueryHandlers.Car.GetStatisticsByUser
{
    using Domain.Models.Enums;
    using System.Collections.Generic;

    public class GetStatisticsByUserOutputModel
    {
        public int PercentageOfNewCars { get; set; }

        public int AverageCarPrice { get; set; }

        public Dictionary<CarColour, int> PercentagesByCarColour
            = new Dictionary<CarColour, int>();
    }
}
