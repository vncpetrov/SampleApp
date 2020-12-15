namespace SampleApp.Web.Models.Car
{
    using SampleApp.Domain.Models.Enums;

    public class CarListingModel
    { 
        public CarColour Colour { get; set; }
         
        public CarCondition Condition { get; set; }
         
        public double Price { get; set; } 
    }
}