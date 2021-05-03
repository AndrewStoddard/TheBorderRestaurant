using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.HomeControllerTests
{
    public class TestMenu
    {
        #region Methods

        [Fact]
        public void TestReturnsView()
        {
            var uow = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var controller = new HomeController(uow.Object);
            var result = controller.Menu() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}