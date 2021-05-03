using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class AddFoodTests
    {
        #region Methods

        [Fact]
        public void TestAddFoodReturnsView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            var result = controller.AddFood() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}