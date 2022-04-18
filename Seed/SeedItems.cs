using ChallengeSnow.Interfaces;
using ChallengeSnow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Seed
{
    public class SeedItems
    {
        public static void SeedData(IOrderManager orderManager)
        {
            if (orderManager.ContainsOrders()) return;

            Random rnd = new Random();
            List<Item> items = new List<Item>();
            List<Deal_Item> deals = new List<Deal_Item>();


            for (int i = 1; i <= 5; i++)
            {
                var o = new Item
                {
                    Id = Guid.NewGuid(),
                    Available_Quantity = rnd.Next(10, 50),
                    Price = Convert.ToDecimal(rnd.NextDouble() * 500)

                };

                items.Add(o);

                var d = new Deal_Item
                {
                    Id = Guid.NewGuid(),
                    Available_Quantity = rnd.Next(10, 50),
                    Price = Convert.ToDecimal(rnd.NextDouble() * 500),
                    Discount = Convert.ToDecimal(rnd.Next(1, 100)),
                    Start_Date = DateTime.Now.AddDays(i * -1),
                    End_Date = DateTime.Now.AddDays(i),
                };

                deals.Add(d);
            }


            List<Order> orders = new List<Order>();
            for (int i = 1; i <= 10; i++)
            {
                var o = new Order
                {
                    Id = Guid.NewGuid(),
                    Date_Created = DateTime.Now.AddDays(i * -1),
                    Quantity = rnd.Next(1, 10),
                    Item_Number = (rnd.Next(2) == 1) ? items[rnd.Next(0, 5)] : deals[rnd.Next(0, 5)]
                };

                orders.Add(o);
            }

            orderManager.InitialValues(orders, items, deals);
        }
    }
}
