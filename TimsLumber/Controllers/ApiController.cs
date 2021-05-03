using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TimsLumber.Models;

namespace TimsLumber.Controllers
{
    [Route("api/")]
    public class ApiController : Controller
    {

        private TimsLumberContext context;

        public ApiController(TimsLumberContext ctx)
        {
            context = ctx;
        }


        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        [Produces("application/json")]
        public IActionResult GetOrder(int id)
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

            OrderOutput output = new OrderOutput(order);
            return new ObjectResult(output);
        }

        public class OrderOutput
        {
            public int OrderID{ get; set; }
            public string Subtotal { get; set; }
            public string Tax { get; set; }
            public string Total { get; set; }
            public string [] OrderItems { get; set; }

            public OrderOutput()
            {

            }
            public OrderOutput (Order order)
            {
                OrderID = order.OrderId;
                Subtotal = order.Subtotal.ToString("C2");
                Tax = order.Tax.ToString("C2");
                Total = order.Total.ToString("C2");

                List<OrderItem> OItems = order.OrderItems.ToList();
                OrderItems = new string[OItems.Count];
                for (int i = 0; i < OItems.Count; i++)
                {
                    OrderItems[i] =
                        $@"Nominal Size : {OItems[i].LumberItem.NominalSize}     Actual Size : {OItems[i].LumberItem.ActualSize}     Price per ft : {OItems[i].LumberItem.PricePerFt.ToString("C2")}     Length : {OItems[i].Length}     Cost : {OItems[i].Cost.ToString("C2")}";
                }
            }
            
        }

    }
}
