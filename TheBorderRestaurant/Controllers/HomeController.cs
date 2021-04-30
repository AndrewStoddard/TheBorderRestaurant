using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Models.DataLayer;

namespace TheBorderRestaurant.Controllers
{
    public class HomeController : Controller
    {
        #region Data members

        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            ViewBag.FoodItems = this.unitOfWork.FoodItems.Get().ToList();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Specials()
        {
            return View();
        }

        public IActionResult OrderDelivery()
        {
            return RedirectToAction("Menu");
        }

        #endregion
    }
}