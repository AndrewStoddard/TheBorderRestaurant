using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBorderRestaurant.Models.DataLayer;
using TheBorderRestaurant.Models.DomainModels;
using TheBorderRestaurant.Models.Enums;
using TheBorderRestaurant.Models.ViewModels;

namespace TheBorderRestaurant.Controllers
{
    public class AdminController : Controller
    {
        #region Data members

        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor contextAccessor;

        #endregion

        #region Constructors

        public AdminController(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.contextAccessor = contextAccessor;
        }

        #endregion

        #region Methods

        public IActionResult FoodList(int pageNumber = 0)
        {
            var pageSize = this.contextAccessor.HttpContext.Session.GetInt32("food_page_size") ?? 3;
            var food = this.unitOfWork.FoodItems.Get().ToList();
            var totalPages = food.Count / pageSize;

            if (this.contextAccessor.HttpContext.Session.GetInt32("food_sort_direction") ==
                (int) SortDirection.Ascending)
            {
                food = this.orderAscending(food);
            }
            else if (this.contextAccessor.HttpContext.Session.GetInt32("food_sort_direction") ==
                     (int) SortDirection.Descending)
            {
                food = this.orderDescending(food);
            }

            totalPages += food.Count % pageSize == 0 ? 0 : 1;
            food = food.Skip((pageNumber - 1) * pageSize).Take(pageSize > food.Count ? food.Count : pageSize)
                       .ToList();

            var viewModel = new FoodListViewModel {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                Items = food
            };
            return View(viewModel);
        }

        private List<FoodItem> orderDescending(List<FoodItem> food)
        {
            food = this.contextAccessor.HttpContext.Session.GetInt32("food_sort_type") switch {
                0 => food.OrderBy(f => f.Name).ToList(),
                1 => food.OrderBy(f => f.Description).ToList(),
                2 => food.OrderBy(f => f.Price).ToList()
            };
            return food;
        }

        private List<FoodItem> orderAscending(List<FoodItem> food)
        {
            food = this.contextAccessor.HttpContext.Session.GetInt32("food_sort_type") switch {
                0 => food.OrderByDescending(f => f.Name).ToList(),
                1 => food.OrderByDescending(f => f.Description).ToList(),
                2 => food.OrderByDescending(f => f.Price).ToList()
            };
            return food;
        }

        public IActionResult FoodSort(SortTypes sort)
        {
            this.contextAccessor.HttpContext.Session.SetInt32("food_sort_type", (int) sort);
            if (this.contextAccessor.HttpContext.Session.GetInt32("food_sort_direction") ==
                (int) SortDirection.Ascending)
            {
                this.contextAccessor.HttpContext.Session.SetInt32("food_sort_direction",
                    (int) SortDirection.Descending);
            }
            else
            {
                this.contextAccessor.HttpContext.Session.SetInt32("food_sort_direction", (int) SortDirection.Ascending);
            }

            return RedirectToAction("FoodList");
        }

        [HttpPost]
        public IActionResult FoodPageSize(int size)
        {
            this.contextAccessor.HttpContext.Session.SetInt32("food_page_size", size);
            return RedirectToAction("FoodList");
        }

        public IActionResult OrderList(int pageNumber = 0)
        {
            var pageSize = this.contextAccessor.HttpContext.Session.GetInt32("order_page_size") ?? 3;
            var order = this.unitOfWork.FoodOrders.Get().Include(o => o.FoodOrderItems).Include(o => o.User).ToList();
            var totalPages = order.Count / pageSize;

            if (this.contextAccessor.HttpContext.Session.GetInt32("order_sort_direction") ==
                (int) SortDirection.Ascending)
            {
                order = this.orderAscending(order);
            }
            else if (this.contextAccessor.HttpContext.Session.GetInt32("order_sort_direction") ==
                     (int) SortDirection.Descending)
            {
                order = this.orderDescending(order);
            }

            totalPages += order.Count % pageSize == 0 ? 0 : 1;
            order = order.Skip((pageNumber - 1) * pageSize).Take(pageSize > order.Count ? order.Count : pageSize)
                         .ToList();

            var viewModel = new OrderListViewModel {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                Orders = order
            };
            return View(viewModel);
        }

        private List<FoodOrder> orderDescending(List<FoodOrder> order)
        {
            order = this.contextAccessor.HttpContext.Session.GetInt32("order_sort_type") switch {
                3 => order.OrderBy(f => f.Total()).ToList(),
                4 => order.OrderBy(f => f.User.FirstName).ToList(),
                5 => order.OrderBy(f => f.OrderDateTime).ToList()
            };
            return order;
        }

        private List<FoodOrder> orderAscending(List<FoodOrder> order)
        {
            order = this.contextAccessor.HttpContext.Session.GetInt32("order_sort_type") switch {
                3 => order.OrderByDescending(f => f.Total()).ToList(),
                4 => order.OrderByDescending(f => f.User.FirstName).ToList(),
                5 => order.OrderByDescending(f => f.OrderDateTime).ToList()
            };
            return order;
        }

        public IActionResult OrderSort(SortTypes sort)
        {
            this.contextAccessor.HttpContext.Session.SetInt32("order_sort_type", (int) sort);
            if (this.contextAccessor.HttpContext.Session.GetInt32("order_sort_direction") ==
                (int) SortDirection.Ascending)
            {
                this.contextAccessor.HttpContext.Session.SetInt32("order_sort_direction",
                    (int) SortDirection.Descending);
            }
            else
            {
                this.contextAccessor.HttpContext.Session.SetInt32("order_sort_direction",
                    (int) SortDirection.Ascending);
            }

            return RedirectToAction("OrderList");
        }

        [HttpPost]
        public IActionResult OrderPageSize(int size)
        {
            this.contextAccessor.HttpContext.Session.SetInt32("order_page_size", size);
            return RedirectToAction("OrderList");
        }

        public IActionResult ViewOrder(int orderId)
        {
            var order = this.unitOfWork.FoodOrders.Get().Include(o => o.FoodOrderItems)
                            .FirstOrDefault(o => o.Id == orderId);
            foreach (var orderItem in order.FoodOrderItems)
            {
                orderItem.FoodItem = this.unitOfWork.FoodItems.Get().FirstOrDefault(f => f.Id == orderItem.FoodItemId);
            }

            var vm = new OrderViewModel {OrderId = order.Id, Items = order.FoodOrderItems.ToList()};

            return View(vm);
        }

        [HttpGet]
        public IActionResult EditFood(int id)
        {
            var foodItem = this.unitOfWork.FoodItems.Get().FirstOrDefault(f => f.Id == id);
            ViewBag.Action = "Edit";
            return View("AddEditFood", foodItem);
        }

        [HttpGet]
        public IActionResult AddFood()
        {
            ViewBag.Action = "Add";
            return View("AddEditFood");
        }

        [HttpPost]
        public IActionResult AddEditFood(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                foodItem.ImageType = "jpeg";
                if (foodItem.Id == 0)
                {
                    this.unitOfWork.FoodItems.Insert(foodItem);
                }
                else
                {
                    this.unitOfWork.FoodItems.Update(foodItem);
                }
            }
            else
            {
                if (foodItem.Id == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }

                return View("AddEditFood", foodItem);
            }

            this.unitOfWork.Save();
            return RedirectToAction("FoodList");
        }

        #endregion
    }
}