using Microsoft.AspNetCore.Mvc;

namespace TheBorderRestaurant.Controllers
{
    public class HomeController : Controller
    {
        #region Methods

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}