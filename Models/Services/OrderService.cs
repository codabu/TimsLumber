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
            OI.LumberItem = li;
            OI.Cost = OI.LumberItem.PricePerFt * OI.Length;
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
            foreach(OrderItem OI in thisOrder.OrderItems)
            {
                thisOrder.Subtotal += OI.Cost;
            }

            thisOrder.Tax = CalculateTax(thisOrder.Subtotal);
            thisOrder.Total = CalculateTotal(thisOrder.Subtotal, thisOrder.Tax);

            return thisOrder;
        }

        public static Order PopulateLIs(Order order, TimsLumberContext context)
        {
            foreach (OrderItem OI in order.OrderItems)
            {
                int id = OI.LumberItemId;
                OI.LumberItem = context.Find<LumberItem>(id);
            }

            return order;
        }
    }
}
