using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.HomeControllerTests
{
    public class TestIndex
    {
        #region Methods

        [Fact]
        public void TestReturnsView()
        {
            var uow = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var controller = new HomeController(uow.Object);
            var result = controller.Index() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}