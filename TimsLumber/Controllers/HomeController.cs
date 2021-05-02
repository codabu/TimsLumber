using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimsLumber.Models;

namespace TimsLumber.Controllers
{
    public class HomeController : Controller
    {

        private TimsLumberContext context;
        private readonly UserManager<User> _userManager;


        public HomeController(TimsLumberContext ctx, UserManager<User> userManager)
        {
            context = ctx;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewOrder()
        {
            AddViewModel model = new AddViewModel();
            model.LumberItems = context.LumberItems.ToList();
            Order order = new Order();
            context.Add(order);
            context.SaveChanges();
            HttpContext.Session.SetString("OrderId", order.OrderId.ToString());
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            OrderService orderService = new OrderService();
            model.LumberItems = context.LumberItems.ToList();
            int id = int.Parse(HttpContext.Session.GetString("OrderId"));
            Order order = context.Find<Order>(id);
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {id}").ToList();
            if (order.OrderItems == null)
            {
                order.OrderItems = new List<OrderItem>();
            }

            if (ModelState.IsValid)
            {
                order.OrderItems.Add(OrderService.AddItemToOrder(model, context));
                context.Update(order);
                context.SaveChanges();
            }
            model.ThisOrder = order;
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewOrder()
        {
            OrderService orderService = new OrderService();
            OrderViewModel model = new OrderViewModel();
            int id = int.Parse(HttpContext.Session.GetString("OrderId"));
            Order order = context.Find<Order>(id);
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {id}").ToList();
            //fix to prevent submitting an empty order
            if (order.OrderItems.Count == 0)
            {
                return RedirectToAction("NewOrder");
            }
            order = OrderService.PopulateLIs(order, context);
            order = OrderService.FinalizeOrder(order);
            order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Update(order);
            context.SaveChanges();
            model.Order = order;
            return View("Summary", model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult MyOrders()
        {
            OrderViewModel model = new OrderViewModel();
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Order> orders = context.Orders.FromSqlRaw($"SELECT * FROM Orders").ToList();
            List<Order> myOrders = new List<Order>();
            foreach (Order o in orders)
            {
                if (o.UserId == id)
                {
                    myOrders.Add(o);
                }
            }
            model.Orders = myOrders;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditPage(OrderViewModel model)
        {
            int thisId = model.OrderID;
            Order order = context.Find<Order>(thisId);
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {thisId}").ToList();
            order = OrderService.PopulateLIs(order, context);
            model.LumberItems = context.LumberItems.ToList();
            model.Order = order;
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditPage()
        {
            OrderViewModel model = new OrderViewModel();

            int thisId = (int)TempData["OrderId"];
            Order order = context.Find<Order>(thisId);
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {thisId}").ToList();
            order = OrderService.PopulateLIs(order, context);
            model.LumberItems = context.LumberItems.ToList();
            model.Order = order;
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit(int OrderId, int[] LIds, int[] Lengths)
        {
            OrderViewModel model = new OrderViewModel();
            Order order = context.Find<Order>(OrderId);
            order.OrderItems = OrderService.EditItems(OrderId, LIds, Lengths, context);
            order = OrderService.FinalizeOrder(order);
            context.Orders.Update(order);
            context.SaveChanges();
            model.Order = order;
            model.OrderID = order.OrderId;
            return View("Summary", model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditAdd(int OrderId)
        {
            //add a new item to the order
            OrderItem OI = new OrderItem();
            OI.Length = 0;
            OI.Cost = 0;
            OI.LumberItemId = 1;
            OI.OrderId = OrderId;

            context.Add<OrderItem>(OI);
            context.SaveChanges();

            TempData["OrderId"] = OrderId;
            return RedirectToAction("EditPage", "Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Summary (int orderId)
        {
            OrderViewModel model = new OrderViewModel();
            Order order = context.Find<Order>(orderId);
            model.Order = order;
            model.OrderID = order.OrderId;
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {orderId}").ToList();
            order = OrderService.PopulateLIs(order, context);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(OrderViewModel model)
        {
            int id = model.OrderID;
            Order order = context.Find<Order>(id);
            context.Remove(order);
            context.SaveChanges();
            return RedirectToAction("MyOrders");
        }
    }
}
