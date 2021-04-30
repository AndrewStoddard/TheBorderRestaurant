using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBorderRestaurant.Models.DataLayer;
using TheBorderRestaurant.Models.DomainModels;
using TheBorderRestaurant.Models.ViewModels;

namespace TheBorderRestaurant.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        #region Data members

        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<User> userManager;

        #endregion

        #region Constructors

        public OrderController(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, UserManager<User> userMngr)
        {
            this.unitOfWork = unitOfWork;
            this.contextAccessor = contextAccessor;
            this.userManager = userMngr;
        }

        #endregion

        #region Methods

        public IActionResult Order()
        {
            var orderId = this.contextAccessor.HttpContext.Session.GetInt32("orderid");
            if (orderId == null)
            {
                return RedirectToAction("LogOut", "Account");
            }

            var order = this.unitOfWork.FoodOrders.Get().Include(o => o.FoodOrderItems)
                            .FirstOrDefault(o => o.Id == orderId);
            foreach (var orderItem in order.FoodOrderItems)
            {
                orderItem.FoodItem = this.unitOfWork.FoodItems.Get().FirstOrDefault(f => f.Id == orderItem.FoodItemId);
            }

            var vm = new OrderViewModel {OrderId = order.Id, Items = order.FoodOrderItems.ToList()};

            return View(vm);
        }

        public IActionResult AddToOrder(int id)
        {
            var orderId = this.contextAccessor.HttpContext.Session.GetInt32("orderid");
            if (orderId == null)
            {
                return RedirectToAction("LogOut", "Account");
            }

            var order = this.unitOfWork.FoodOrders.Get().Include(o => o.FoodOrderItems)
                            .FirstOrDefault(o => o.Id == orderId);
            var foodItem = this.unitOfWork.FoodItems.Get().FirstOrDefault(f => f.Id == id);
            var foodOrderItem = order.FoodOrderItems.FirstOrDefault(foi => foi.FoodItemId == id);
            if (foodOrderItem == null)
            {
                foodOrderItem = new FoodOrderItem {FoodItem = foodItem, FoodItemId = foodItem.Id, Quantity = 1};

                this.unitOfWork.FoodOrderItems.Insert(foodOrderItem);
            }
            else
            {
                foodOrderItem.Quantity++;
                this.unitOfWork.FoodOrderItems.Update(foodOrderItem);
            }

            order.FoodOrderItems.Add(foodOrderItem);
            this.unitOfWork.FoodOrders.Update(order);
            this.unitOfWork.Save();
            TempData["message"] = $"Added {foodItem.Name} to order";
            return RedirectToAction("Menu", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var foodOrderItem = this.unitOfWork.FoodOrderItems.Get().Include(f => f.FoodItem)
                                    .FirstOrDefault(f => f.Id == id);
            return View(foodOrderItem);
        }

        [HttpPost]
        public IActionResult Edit(FoodOrderItem foodOrderItem)
        {
            var quantity = foodOrderItem.Quantity;
            foodOrderItem = this.unitOfWork.FoodOrderItems.Get().Include(f => f.FoodItem)
                                .FirstOrDefault(f => f.Id == foodOrderItem.Id);
            foodOrderItem.Quantity = quantity;
            this.unitOfWork.FoodOrderItems.Update(foodOrderItem);
            this.unitOfWork.Save();
            TempData["message"] = $"Updated Quantity of {foodOrderItem.FoodItem.Name} to {quantity}";
            return RedirectToAction("Order");
        }

        public IActionResult Remove(int id)
        {
            var foodOrderItem = this.unitOfWork.FoodOrderItems.Get().Include(f => f.FoodItem)
                                    .FirstOrDefault(f => f.Id == id);
            var orderId = this.contextAccessor.HttpContext.Session.GetInt32("orderid");
            var order = this.unitOfWork.FoodOrders.Get().Include(o => o.FoodOrderItems)
                            .FirstOrDefault(o => o.Id == orderId);
            order.FoodOrderItems.Remove(foodOrderItem);
            this.unitOfWork.Save();
            TempData["message"] = $"Removed {foodOrderItem.FoodItem.Name} from order.";
            return RedirectToAction("Order");
        }

        public async Task<IActionResult> CheckOut(int id)
        {
            var order = this.unitOfWork.FoodOrders.Get().FirstOrDefault(o => o.Id == id);
            order.IsComplete = true;
            order.OrderDateTime = DateTime.Now;
            var user = await this.userManager.GetUserAsync(User);
            var newOrder = new FoodOrder {
                UserId = user.Id,
                User = user,
                IsComplete = false
            };
            this.unitOfWork.FoodOrders.Insert(newOrder);
            this.unitOfWork.FoodOrders.Update(order);
            this.unitOfWork.Save();
            this.contextAccessor.HttpContext.Session.SetInt32("orderid",
                this.unitOfWork.FoodOrders.Get().First(o => o.IsComplete == false && o.UserId == user.Id).Id);
            TempData["message"] = "Order Placed!";
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}