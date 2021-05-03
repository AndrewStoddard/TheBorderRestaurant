using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using TheBorderRestaurant.Controllers;
using TheBorderRestaurant.Models.DomainModels;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class AddEditFoodTests
    {
        #region Methods

        [Fact]
        public void TestAddFoodSuccess()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var tempData = new TempDataDictionary(accessor.Object.HttpContext, Mock.Of<ITempDataProvider>());

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.TempData = tempData;

            var foodItem = new FoodItem {
                Id = 0,
                Name = "Test",
                Description = "Test",
                ImageName = "Test",
                Price = 2.0
            };
            var result = controller.AddEditFood(foodItem) as RedirectToActionResult;
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void TestEditFoodSuccess()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var tempData = new TempDataDictionary(accessor.Object.HttpContext, Mock.Of<ITempDataProvider>());

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.TempData = tempData;
            var foodItem = new FoodItem {
                Id = 1,
                Name = "Test",
                Description = "Test",
                ImageName = "Test",
                Price = 2.0
            };
            var result = controller.AddEditFood(foodItem) as RedirectToActionResult;
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void TestEditStateInvalid()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            var foodItem = new FoodItem {
                Id = 0
            };
            controller.ModelState.AddModelError("test", "test");
            var result = controller.AddEditFood(foodItem) as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestAddStateInvalid()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var tempData = new TempDataDictionary(accessor.Object.HttpContext, Mock.Of<ITempDataProvider>());

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.TempData = tempData;
            var foodItem = new FoodItem {
                Id = 1
            };
            controller.ModelState.AddModelError("test", "test");

            var result = controller.AddEditFood(foodItem) as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}