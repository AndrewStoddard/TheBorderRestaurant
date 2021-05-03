using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class ViewOrderTests
    {
        #region Methods

        [Fact]
        public void TestViewOrderReturnsView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var tempData = new TempDataDictionary(accessor.Object.HttpContext, Mock.Of<ITempDataProvider>());

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.TempData = tempData;
            var result = controller.ViewOrder(1) as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}