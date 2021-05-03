using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class PageSizeTests
    {
        #region Methods

        [Fact]
        public void FoodPageSize_ReturnsRedirectToActionResult()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);

            var result = controller.FoodPageSize(1) as RedirectToActionResult;

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void OrderPageSize_ReturnsRedirectToActionResult()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);

            var result = controller.OrderPageSize(1) as RedirectToActionResult;

            Assert.IsType<RedirectToActionResult>(result);
        }

        #endregion
    }
}