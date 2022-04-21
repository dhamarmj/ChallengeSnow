using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Models
{
    public static class Calculating
    {
        public static decimal GetProfit(Order order, Item item)
        {
            decimal total = order.Quantity * (item.Price * 20 / 100);
            return Math.Round(total, 2);
        }

        public static decimal GetProfit(Order order, Deal_Item item)
        {
            decimal total = order.Quantity * (item.Price * 20 / 100) - (item.Price - item.Reduced_price);
            return Math.Round(total, 2);
        }

        // changed the reference of OrderManager to receive a List of orders from OrderManager 
        public static decimal GetProfitLoss(List<Order> orders)
        {
            decimal total = 0;
            foreach (var item in orders)
            {
                var dealitem = item.Item_Number as Deal_Item;
                if (dealitem != null)
                {
                    total += item.Quantity * (item.Item_Number.Price * 20 / 100) - (item.Item_Number.Price - dealitem.Reduced_price);
                }
            }

            return Math.Round(total, 2);
        }

        public static decimal GetProfit(List<Order> orders)
        {
            decimal total = 0;
            foreach (var item in orders)
            {
                var dealitem = item.Item_Number as Deal_Item;
                if (dealitem != null)
                    total += item.Quantity * (dealitem.Reduced_price * 20 / 100);
                else
                    total += item.Quantity * (item.Item_Number.Price * 20 / 100);
            }

            return Math.Round(total, 2);
        }

    }
}
