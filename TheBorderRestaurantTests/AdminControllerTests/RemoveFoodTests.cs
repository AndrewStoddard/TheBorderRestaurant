using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class RemoveFoodTests
    {
        #region Methods

        [Fact]
        public void TestRemoveFoodReturnsRedirectToActionResult()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            var result = controller.RemoveFood(1) as RedirectToActionResult;
            Assert.IsType<RedirectToActionResult>(result);
        }

        #endregion
    }
}