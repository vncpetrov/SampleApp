namespace SampleApp.Web.Models.Car
{
    using Domain.Models.Enums;
    using System.Collections.Generic;

    public class UserCarStatisticsModel
    {
        public int PercentageOfNewCars { get; set; }

        public int AverageCarPrice { get; set; }

        public Dictionary<CarColour, int> PercentagesByCarColour
            = new Dictionary<CarColour, int>();

    }
}
