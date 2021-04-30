using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using TheBorderRestaurant.Models.DataLayer;
using TheBorderRestaurant.Models.DomainModels;

namespace TheBorderRestaurantTests.TestLibrary
{
    public class TestLibrary
    {
        #region Methods

        public static Mock<IHttpContextAccessor> SetUpHttpContextAccessor()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new Mock<HttpContext>();
            context.Setup(c => c.Session).Returns(new FakeSession());

            httpContextAccessor.Setup(a => a.HttpContext).Returns(context.Object);
            return httpContextAccessor;
        }

        public static Mock<UserManager<User>> MockUserManager<TUser>(List<User> ls)
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<User>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<User>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success)
               .Callback<User, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            return mgr;
        }

        public static Mock<IUnitOfWork> SetUpIUnitOfWork()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var food1 = new FoodItem {
                Id = 1,
                Name = "Taco",
                Description =
                    "Traditional Mexican dish consisting of a small hand-sized corn or wheat tortilla topped with a filling.",
                Price = 3.00,
                ImageName = "Taco.jpg"
            };
            var food2 = new FoodItem {
                Id = 2,
                Name = "Quesadilla",
                Description =
                    "Mexican cuisine dish, consisting of a tortilla that is filled primarily with cheese, and sometimes meats, spices, and other fillings, and then cooked on a griddle or stove.",
                Price = 6.00,
                ImageName = "Quesadilla.jpg"
            };
            var food3 = new FoodItem {
                Id = 3,
                Name = "Queso and Chips",
                Description =
                    "A blend of queso cheese, peppers, and spices that's a little smoky, a little spicy, and very cheesy.",
                Price = 5.00,
                ImageName = "Queso.jpg"
            };
            var food4 = new FoodItem {
                Id = 4,
                Name = "Enchilada",
                Description =
                    "A corn tortilla rolled around a filling and covered with a savory sauce.",
                Price = 8.00,
                ImageName = "Enchiladas.jpg"
            };

            var order1 = new FoodOrder {
                Id = 1,
                UserId = "admin",
                OrderDateTime = DateTime.Now.AddDays(-2),
                IsComplete = true
            };
            var order2 = new FoodOrder {
                Id = 2,
                UserId = "admin",
                OrderDateTime = DateTime.Now,
                IsComplete = false
            };

            var orderItem1 = new FoodOrderItem {
                Id = 1,
                FoodItemId = 1,
                Quantity = 2,
                FoodOrders = new List<FoodOrder> {order1}
            };

            var orderItem2 = new FoodOrderItem {
                Id = 1,
                FoodItemId = 1,
                Quantity = 2,
                FoodOrders = new List<FoodOrder> {order2}
            };
            order1.FoodOrderItems.Add(orderItem1);
            order2.FoodOrderItems.Add(orderItem2);
            var food = new List<FoodItem> {food1, food2, food3, food4};
            var orders = new List<FoodOrder> {order1, order2};
            var orderItems = new List<FoodOrderItem> {orderItem1, orderItem2};
            var orderRepo = new Mock<Repository<FoodOrder>>();
            var orderItemRepo = new Mock<Repository<FoodOrderItem>>();
            var foodRepo = new Mock<Repository<FoodItem>>();
            orderRepo.Setup(repo => repo.Get(null)).Returns(orders.AsQueryable());
            orderItemRepo.Setup(repo => repo.Get(null)).Returns(orderItems.AsQueryable());
            foodRepo.Setup(repo => repo.Get(null)).Returns(food.AsQueryable());

            unitOfWork.Setup(w => w.FoodOrders).Returns(orderRepo.Object);
            unitOfWork.Setup(w => w.FoodOrderItems).Returns(orderItemRepo.Object);
            unitOfWork.Setup(w => w.FoodItems).Returns(foodRepo.Object);

            return unitOfWork;
        }

        #endregion
    }
}