namespace SampleApp.Web.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static Utils.ModelConstants;

    public class SignUpUserRequestModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail",
                 Prompt = "E-mail")]
        public string Email { get; set; }


        [Required]
        [MinLength(PasswordMinLength,
                   ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(PasswordMaxLength,
                   ErrorMessage = MaxLengthErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Prompt = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password",
                 Prompt = "Password")]
        [Compare(nameof(SignUpUserRequestModel.Password),
                 ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}