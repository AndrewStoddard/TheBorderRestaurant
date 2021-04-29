using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Models.DomainModels;
using TheBorderRestaurant.Models.ViewModels;

namespace TheBorderRestaurant.Controllers
{
    public class AccountController : Controller
    {
        #region Data members

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        #endregion

        #region Constructors

        public AccountController(UserManager<User> userMngr,
            SignInManager<User> signInMngr)
        {
            this.userManager = userMngr;
            this.signInManager = signInMngr;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip
                };
                var result = await this.userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel {ReturnUrl = returnURL};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(
                    model.Username, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                        Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }

        public ViewResult AccessDenied()
        {
            return View();
        }

        #endregion
    }
}