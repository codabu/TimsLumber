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
        public IActionResult Orders()
        {
            AddViewModel model = new AddViewModel();
            model.LumberItems = context.LumberItems.ToList();
            Order order = new Order();
            context.Add(order);
            context.SaveChanges();
            HttpContext.Session.SetString("OrderId", order.OrderId.ToString());
            int id = int.Parse(HttpContext.Session.GetString("OrderId"));
            return View(model);
        }

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
            order.OrderItems.Add(OrderService.AddItemToOrder(model, context));
            context.Update(order);
            context.SaveChanges();
            model.ThisOrder = order;
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewOrder()
        {
            OrderService orderService = new OrderService();
            OrderViewModel model = new OrderViewModel();
            int id = int.Parse(HttpContext.Session.GetString("OrderId"));
            Order order = context.Find<Order>(id);
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {id}").ToList();
            order = OrderService.PopulateLIs(order, context);
            order = OrderService.FinalizeOrder(order);
            order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Update(order);
            context.SaveChanges();
            model.Order = order;
            return View("Summary", model);
        }
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

        [HttpPost]
        public IActionResult EditPage(OrderViewModel model)
        {
            int thisId = model.OrderID;
            Order order = context.Find<Order>(thisId);
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {thisId}").ToList();
            order = OrderService.PopulateLIs(order, context);
            model.Order = order;
            return View(model);
        }
    }
}
