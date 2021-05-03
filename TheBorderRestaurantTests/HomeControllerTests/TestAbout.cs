using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.HomeControllerTests
{
    public class TestAbout
    {
        #region Methods

        [Fact]
        public void TestReturnsView()
        {
            var uow = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var controller = new HomeController(uow.Object);
            var result = controller.About() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}