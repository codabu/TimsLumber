using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimsLumber.Models;

namespace TimsLumber.Controllers
{
    public class HomeController : Controller
    {

        private TimsLumberContext context;



        public HomeController(TimsLumberContext ctx)
        {
            context = ctx;

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
            order.Items = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {id}").ToList();
            if (order.Items == null)
            {
                order.Items = new List<OrderItem>();
            }
            order.Items.Add(OrderService.AddItemToOrder(model, context));
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
            order.Items = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {id}").ToList();
            order = OrderService.PopulateLIs(order, context);
            order = OrderService.FinalizeOrder(order);
            context.Update(order);
            context.SaveChanges();
            model.Order = order;
            return View("Summary", model);
        }
    }
}
