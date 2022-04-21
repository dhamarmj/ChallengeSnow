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
            return total;
        }

        public static decimal GetProfit(Order order, Deal_Item item)
        {
            decimal total = order.Quantity * (item.Price - item.Reduced_price);
            return total;
        }

        public static decimal GetProfitLoss(List<Order> orders)
        {
            decimal total = 0;
            foreach (var item in orders)
            {
                var dealitem = (Deal_Item)item.Item_Number;
                if (dealitem.Discount > 0)
                {
                    total += item.Quantity * (item.Item_Number.Price - dealitem.Reduced_price);
                }
            }

            return total;
        }

    }
}
