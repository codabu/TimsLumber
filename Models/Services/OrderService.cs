using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimsLumber.Models
{
    public class OrderService
    {
        private const double TaxRate = 0.06;
        public static OrderItem AddItemToOrder(AddViewModel model, TimsLumberContext context)
        {
            OrderItem OI = new OrderItem();
            LumberItem li = context.Find<LumberItem>(model.LumberItemId);
            OI.Length = model.Length;
            OI.LItem = li;
            OI.Cost = OI.LItem.PricePerFt * OI.Length;
            return OI;
        }

        private static double CalculateTax(double subtotal)
        {
            return subtotal * TaxRate;
        }

        private static double CalculateTotal(double subtotal, double tax)
        {
            return subtotal + tax;
        }

        public static Order FinalizeOrder (Order order)
        {
            Order thisOrder = order;
            thisOrder.Subtotal = 0;
            foreach(OrderItem OI in thisOrder.Items)
            {
                thisOrder.Subtotal += OI.Cost;
            }

            thisOrder.Tax = CalculateTax(thisOrder.Subtotal);
            thisOrder.Total = CalculateTotal(thisOrder.Subtotal, thisOrder.Tax);

            return thisOrder;
        }

        public static Order PopulateLIs(Order order, TimsLumberContext context)
        {
            foreach (OrderItem OI in order.Items)
            {
                int id = OI.OrderItemId;
                int LId = int.Parse(context.OrderItems.FromSqlRaw($"SELECT LItemLumberItemId FROM OrderItems WHERE OrderItemId = {id}").ToString());
                Console.WriteLine(LId);
                //OI.LItem = context.Find<LumberItem>(LId);
            }

            return order;
        }
    }
}
