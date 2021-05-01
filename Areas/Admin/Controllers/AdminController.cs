using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimsLumber.Areas.Admin.Models;
using TimsLumber.Models;

namespace TimsLumber.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private TimsLumberContext context;
        public AdminController(TimsLumberContext ctx)
        {
            context = ctx;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Orders()
        {
            AdminOrderViewModel model = new AdminOrderViewModel();

            //clean the orders DB
            List<Order> orders = context.Orders.FromSqlRaw($"SELECT * FROM Orders").ToList();            foreach(Order order in orders)
            {
                if (order.UserId == null)
                {
                    context.Remove(order);
                    context.SaveChanges();
                }
            }

            //get the fresh list of orders
            orders = context.Orders.FromSqlRaw($"SELECT * FROM Orders").ToList();

            model.Orders = orders;
            model.UserNames = new List<String>();
            model.Emails = new List<String>();
            foreach (Order order in orders)
            {
                User user = context.Find<User>(order.UserId);
                model.UserNames.Add(user.Firstname + " " + user.Lastname);
                model.Emails.Add(user.Email);
            }
            return View("Orders", model);
        }

        public IActionResult ViewOrder (AdminOrderViewModel model)
        {
            OrderService orderService = new OrderService();
            int id = model.OrderID;
            Order order = context.Find<Order>(id);
            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {id}").ToList();
            order = OrderService.PopulateLIs(order, context);
            order = OrderService.FinalizeOrder(order);
            model.Order = order;

            //get the users name and email to display
            User user = context.Find<User>(order.UserId);
            model.UserNames = new List<String>();
            model.Emails = new List<String>();
            model.UserNames.Add(user.Firstname + " " + user.Lastname);
            model.Emails.Add(user.Email);

            return View(model);
        }

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

        [HttpPost]
        public IActionResult Edit(int OrderId, int[] LIds, int[] Lengths)
        {
            AdminOrderViewModel model = new AdminOrderViewModel();
            Order order = context.Find<Order>(OrderId);
            order.OrderItems = OrderService.EditItems(OrderId, LIds, Lengths, context);
            order = OrderService.FinalizeOrder(order);
            context.Orders.Update(order);
            context.SaveChanges();
            model.Order = order;
            model.OrderID = order.OrderId;

            //get the users name and email to display
            User user = context.Find<User>(order.UserId);
            model.UserNames = new List<String>();
            model.Emails = new List<String>();
            model.UserNames.Add(user.Firstname + " " + user.Lastname);
            model.Emails.Add(user.Email);

            return View("ViewOrder", model);
        }

        [HttpPost]
        public IActionResult Delete(AdminOrderViewModel model)
        {
            int id = model.OrderID;
            Order order = context.Find<Order>(id);
            context.Remove(order);
            context.SaveChanges();
            return RedirectToAction("Orders");
        }
    }
}
