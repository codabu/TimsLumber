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

        public static List<OrderItem> EditItems(int OId, int[] LIds, int[] Lengths, TimsLumberContext context)
        {
            //Delete the current order items
            List<OrderItem> toDelete = context.OrderItems.FromSqlRaw($"SELECT * FROM OrderItems WHERE OrderId = {OId}").ToList();
            foreach(OrderItem OI in toDelete)
            {
                context.OrderItems.Remove(OI);
            }
            context.SaveChanges();

            //Add the edited items to the order
            List<OrderItem> items = new List<OrderItem>();
            for (int i = 0; i < LIds.Length; i++)
            {
                if (Lengths[i] > 0)
                {
                    OrderItem OI = new OrderItem();
                    LumberItem li = context.Find<LumberItem>(LIds[i]);
                    OI.Length = Lengths[i];
                    OI.LumberItem = li;
                    OI.Cost = OI.LumberItem.PricePerFt * OI.Length;
                    items.Add(OI);
                }
            }
            return items;
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
