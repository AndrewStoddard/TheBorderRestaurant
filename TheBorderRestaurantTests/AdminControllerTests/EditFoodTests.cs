using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class EditFoodTests
    {
        #region Methods

        [Fact]
        public void TestEditFoodReturnsView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            var result = controller.EditFood(1) as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}