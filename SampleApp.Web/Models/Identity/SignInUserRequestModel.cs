﻿namespace SampleApp.Web.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class SignInUserRequestModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail",
                 Prompt = "E-mail")]
        public string Email { get; set; }
         
        [Required] 
        [DataType(DataType.Password)]
        [Display(Prompt = "Password")]
        public string Password { get; set; }
    }
} 