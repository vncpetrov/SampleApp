namespace SampleApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SampleApp.SqlDataAccess.Entities;
    using SampleApp.Web.Models.Identity;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class IdentityController : Controller
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly SignInManager<UserEntity> signInManager;

        public IdentityController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager)
        {
            if (userManager is null)
                throw new ArgumentNullException(nameof(userManager));

            if (signInManager is null)
                throw new ArgumentNullException(nameof(signInManager));

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
            => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpUserRequestModel model)
        {
            UserEntity user = await this.userManager
                .FindByEmailAsync(model.Email);

            if (user != null)
            {
                ModelState.AddModelError("Email", $"E-mail address '{model.Email}' is already taken.");

                return View(model);
            }

            user = new UserEntity
            {
                UserName = model.Email,
                Email = model.Email
            };

            IdentityResult createUserResult = await this.userManager
                .CreateAsync(user, model.Password);

            if (createUserResult.Succeeded)
            {
                return RedirectToAction(nameof(this.SignIn));
            }

            return View(createUserResult.Errors);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
            => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInUserRequestModel model)
        {
            UserEntity user = await this.userManager
                .FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized();

            bool isPasswordValid = await this.userManager
                .CheckPasswordAsync(user, model.Password);

            if (user is null)
                return Unauthorized();

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await this.HttpContext
                .SignInAsync(
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        IssuedUtc = DateTime.UtcNow
                    });

            return RedirectToAction(
                controllerName: nameof(HomeController),
                actionName: nameof(HomeController.Index));
        }
    }
}