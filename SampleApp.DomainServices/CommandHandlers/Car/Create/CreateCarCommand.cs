namespace SampleApp.DomainServices.CommandHandlers.Car.Create
{
    using Domain.Models.Enums;

    public class CreateCarCommand
    {
        public CarCondition Condition { get; set; }

        public CarColour Colour { get; set; }

        public double Price { get; set; }
    }
} 