namespace SampleApp.Web.Models.Car
{
    using SampleApp.Domain.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateCarRequestModel
    {
        [Required] 
        public CarColour Colour { get; set; }

        [Required] 
        public CarCondition Condition { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Price { get; set; } 
    }
}
