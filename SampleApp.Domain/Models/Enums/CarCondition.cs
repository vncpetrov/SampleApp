namespace SampleApp.Domain.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CarCondition
    {
        New = 1,

        [Display(Name = "Second hand")]
        SecondHand = 2
    }
}
