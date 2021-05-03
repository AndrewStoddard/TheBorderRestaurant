using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using TheBorderRestaurant.Models.Enums;
using TheBorderRestaurant.Models.ViewModels;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class FoodListTests
    {
        #region Methods

        [Fact]
        public void SortIsAscending_ByFoodName_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("food_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByFoodName);

            var result = controller.FoodList() as ViewResult;
            var viewModel = result.Model as FoodListViewModel;
            Assert.Equal(1, viewModel.Items[0].Id);
            Assert.Equal(3, viewModel.Items[1].Id);
            Assert.Equal(2, viewModel.Items[2].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsDescending_ByFoodName_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("food_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByFoodName);
            controller.FoodSort(SortTypes.SortByFoodName);

            var result = controller.FoodList() as ViewResult;
            var viewModel = result.Model as FoodListViewModel;
            Assert.Equal(1, viewModel.Items[2].Id);
            Assert.Equal(3, viewModel.Items[1].Id);
            Assert.Equal(2, viewModel.Items[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsAscending_ByFoodDesc_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("food_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByDescription);

            var result = controller.FoodList() as ViewResult;
            var viewModel = result.Model as FoodListViewModel;
            Assert.Equal(1, viewModel.Items[0].Id);
            Assert.Equal(2, viewModel.Items[1].Id);
            Assert.Equal(3, viewModel.Items[2].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsDescending_ByFoodDesc_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("food_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByDescription);
            controller.FoodSort(SortTypes.SortByDescription);

            var result = controller.FoodList() as ViewResult;
            var viewModel = result.Model as FoodListViewModel;
            Assert.Equal(1, viewModel.Items[2].Id);
            Assert.Equal(2, viewModel.Items[1].Id);
            Assert.Equal(3, viewModel.Items[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsAscending_ByFoodPrice_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("food_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByFoodPrice);

            var result = controller.FoodList() as ViewResult;
            var viewModel = result.Model as FoodListViewModel;
            Assert.Equal(2, viewModel.Items[0].Id);
            Assert.Equal(1, viewModel.Items[2].Id);
            Assert.Equal(3, viewModel.Items[1].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsDescending_ByFoodPrice_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByFoodPrice);
            controller.FoodSort(SortTypes.SortByFoodPrice);
            accessor.Object.HttpContext.Session.SetInt32("food_page_size", 3);

            var result = controller.FoodList() as ViewResult;
            var viewModel = result.Model as FoodListViewModel;
            Assert.Equal(2, viewModel.Items[2].Id);
            Assert.Equal(1, viewModel.Items[0].Id);
            Assert.Equal(3, viewModel.Items[1].Id);
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}