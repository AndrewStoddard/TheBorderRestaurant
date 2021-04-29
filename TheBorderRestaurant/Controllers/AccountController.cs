using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Models.DataLayer;
using TheBorderRestaurant.Models.DomainModels;
using TheBorderRestaurant.Models.ViewModels;

namespace TheBorderRestaurant.Controllers
{
    public class AccountController : Controller
    {
        #region Data members

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor contextAccessor;

        #endregion

        #region Constructors

        public AccountController(UserManager<User> userMngr,
            SignInManager<User> signInMngr, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            this.userManager = userMngr;
            this.signInManager = signInMngr;
            this.unitOfWork = unitOfWork;
            this.contextAccessor = contextAccessor;
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
                var newUser = new User {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip
                };
                var result = await this.userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(newUser, false);
                    var user = await this.userManager.FindByNameAsync(model.Username);
                    var order = this.unitOfWork.FoodOrders.Get()
                                    .FirstOrDefault(o => o.UserId == user.Id && o.IsComplete == false);
                    if (order == null)
                    {
                        order = new FoodOrder {
                            UserId = user.Id,
                            User = user,
                            IsComplete = false
                        };
                        this.unitOfWork.FoodOrders.Insert(order);
                        this.unitOfWork.Save();
                    }

                    this.contextAccessor.HttpContext.Session.SetInt32("orderid",
                        this.unitOfWork.FoodOrders.Get()
                            .First(o => o.UserId == user.Id && o.IsComplete == false).Id);
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
                    var user = await this.userManager.FindByNameAsync(model.Username);

                    var order = this.unitOfWork.FoodOrders.Get()
                                    .FirstOrDefault(o => o.UserId == user.Id && o.IsComplete == false);
                    if (order == null)
                    {
                        order = new FoodOrder {
                            UserId = user.Id,
                            User = user,
                            IsComplete = false
                        };
                        this.unitOfWork.FoodOrders.Insert(order);
                        this.unitOfWork.Save();
                    }

                    this.contextAccessor.HttpContext.Session.SetInt32("orderid",
                        this.unitOfWork.FoodOrders.Get()
                            .First(o => o.UserId == user.Id && o.IsComplete == false).Id);

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