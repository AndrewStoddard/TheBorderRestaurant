using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.HomeControllerTests
{
    public class TestSpecials
    {
        #region Methods

        [Fact]
        public void TestReturnsView()
        {
            var uow = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var controller = new HomeController(uow.Object);
            var result = controller.Specials() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}