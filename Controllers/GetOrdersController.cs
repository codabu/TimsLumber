using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimsLumber.Models;

namespace TimsLumber.Controllers
{
    [Route("api/")]
    public class GetOrdersController : Controller
    {

        private TimsLumberContext context;
        private readonly UserManager<User> _userManager;

        public GetOrdersController(TimsLumberContext ctx, UserManager<User> userManager)
        {
            context = ctx;
            _userManager = userManager;
        }

        [HttpGet("GetOrder/{id}", Name = "GetOrders")]
        public IActionResult GetById(int id)
        {
            Order order = context.Find<Order>(id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderItems = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {id}").ToList();
            foreach (OrderItem OI in order.OrderItems)
            {
                OI.LumberItem = context.LumberItems.Find(OI.LumberItemId);
            }

            string output = JsonConvert.SerializeObject(order, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return new ObjectResult(output);
        }
    }
}
