using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBorderRestaurant.Models.DataLayer;
using TheBorderRestaurant.Models.DomainModels;
using TheBorderRestaurant.Models.ViewModels;

namespace TheBorderRestaurant.Controllers
{
    public class OrderController : Controller
    {
        #region Data members

        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor contextAccessor;

        #endregion

        #region Constructors

        public OrderController(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.contextAccessor = contextAccessor;
        }

        #endregion

        #region Methods

        public IActionResult Order()
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                return RedirectToAction("LogIn", "Account", routeValues: "/Home/Order");
            }

            var orderId = this.contextAccessor.HttpContext.Session.GetInt32("orderid");
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
            if (User?.Identity?.IsAuthenticated == false)
            {
                return RedirectToAction("LogIn", "Account", routeValues: "/Home/Menu");
            }

            var orderId = this.contextAccessor.HttpContext.Session.GetInt32("orderid");
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

        public IActionResult Edit(int foodOrderItemId)
        {
            var foodOrderItem = this.unitOfWork.FoodOrderItems.Get().Include(f => f.FoodItem)
                                    .FirstOrDefault(f => f.Id == foodOrderItemId);
            return View(foodOrderItem);
        }

        public IActionResult Edit(int foodOrderItemId, int quantity)
        {
            var foodOrderItem = this.unitOfWork.FoodOrderItems.Get().Include(f => f.FoodItem)
                                    .FirstOrDefault(f => f.Id == foodOrderItemId);
            foodOrderItem.Quantity = quantity;
            this.unitOfWork.Save();
            TempData["message"] = $"Updated Quantity of {foodOrderItem.FoodItem.Name} to {quantity}";
            return RedirectToAction("Order");
        }

        public IActionResult Remove(int foodOrderItemId)
        {
            var foodOrderItem = this.unitOfWork.FoodOrderItems.Get().Include(f => f.FoodItem)
                                    .FirstOrDefault(f => f.Id == foodOrderItemId);
            var orderId = this.contextAccessor.HttpContext.Session.GetInt32("orderid");
            var order = this.unitOfWork.FoodOrders.Get().Include(o => o.FoodOrderItems)
                            .FirstOrDefault(o => o.Id == orderId);
            order.FoodOrderItems.Remove(foodOrderItem);
            this.unitOfWork.Save();
            TempData["message"] = $"Removed {foodOrderItem.FoodItem.Name} from order.";
            return RedirectToAction("Order");
        }

        #endregion
    }
}