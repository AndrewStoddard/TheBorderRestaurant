using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using TheBorderRestaurant.Models.Enums;
using TheBorderRestaurant.Models.ViewModels;
using Xunit;

namespace TheBorderRestaurantTests.AdminControllerTests
{
    public class OrderListTests
    {
        #region Methods

        [Fact]
        public void SortIsAscending_ByUserName_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("order_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByUserName);

            var result = controller.OrderList() as ViewResult;
            var viewModel = result.Model as OrderListViewModel;
            Assert.Equal(1, viewModel.Orders[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsDescending_ByUserName_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("order_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByUserName);
            controller.FoodSort(SortTypes.SortByUserName);

            var result = controller.OrderList() as ViewResult;
            var viewModel = result.Model as OrderListViewModel;
            Assert.Equal(1, viewModel.Orders[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsAscending_ByOrderDate_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("order_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByOrderDate);

            var result = controller.OrderList() as ViewResult;
            var viewModel = result.Model as OrderListViewModel;
            Assert.Equal(1, viewModel.Orders[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsDescending_ByOrderDate_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("order_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByOrderDate);
            controller.FoodSort(SortTypes.SortByOrderDate);

            var result = controller.OrderList() as ViewResult;
            var viewModel = result.Model as OrderListViewModel;
            Assert.Equal(1, viewModel.Orders[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsAscending_ByOrderTotal_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            accessor.Object.HttpContext.Session.SetInt32("order_page_size", 3);

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByOrderTotal);

            var result = controller.OrderList() as ViewResult;
            var viewModel = result.Model as OrderListViewModel;
            Assert.Equal(1, viewModel.Orders[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SortIsDescending_ByOrderTotal_ReturnsHomeView()
        {
            var accessor = TestLibrary.TestLibrary.SetUpHttpContextAccessor();
            var unitOfWork = TestLibrary.TestLibrary.SetUpIUnitOfWork();

            var controller = new AdminController(unitOfWork.Object, accessor.Object);
            controller.FoodSort(SortTypes.SortByOrderTotal);
            controller.FoodSort(SortTypes.SortByOrderTotal);
            accessor.Object.HttpContext.Session.SetInt32("order_page_size", 3);

            var result = controller.OrderList() as ViewResult;
            var viewModel = result.Model as OrderListViewModel;
            Assert.Equal(1, viewModel.Orders[0].Id);
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}