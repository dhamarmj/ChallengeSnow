using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Models
{
    public class Order_Manager : Interfaces.IOrderManager
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Deal_Item> Deal_Items { get; set; } = new List<Deal_Item>();


        public void AddItem(Item item)
        {
            var result = Items.Where(x => x.Id == item.Id);
            if (result != null) return; // "Item Already exists";

            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            var result = Items.Where(x => x.Id == item.Id);
            if (result == null) return; // "Item doesnt exist!";

            Items.Remove(item);
        }


        public void AddDeal(Deal_Item item)
        {
            var result = Deal_Items.Where(x => x.Id == item.Id);
            if (result != null) return; // "Item Already exists";

            Deal_Items.Add(item);
        }

        public void AddOrder(Order order)
        {
            var result = Orders.Where(x => x.Id == order.Id);
            if (result != null) return; // "Item Already exists";

            Orders.Add(order);
        }

        public string PrintItems()
        {
            string print = "";
            Items.ForEach(x =>
            {
                print += String.Format("{0}-{1}-{2} \n", x.Id, x.Available_Quantity, x.Price);
                Console.WriteLine(String.Format("{0}-{1}-{2}", x.Id, x.Available_Quantity, x.Price));
            });

            return print;
        }

        public string PrintOrders()
        {
            string print = "";
            Orders.ForEach(x =>
            {
                print += String.Format("{0}-{1}-{2}-{3} \n", x.Id, x.Date_Created.ToString("MMMM dd yyyy"), x.Item_Number, x.Quantity);
                Console.WriteLine(String.Format("{0}-{1}-{2}-{3} \n", x.Id, x.Date_Created.ToString("MMMM dd yyyy"), x.Item_Number, x.Quantity));
            });

            return print;
        }

        public bool ContainsOrders()
        {
            return Orders.Any();
        }

        public void InitialValues(List<Order> ordersList, List<Item> itemList, List<Deal_Item> dealItems)
        {
            ordersList.ForEach(x => Orders.Add(x));
            itemList.ForEach(x => Items.Add(x));
            dealItems.ForEach(x => Deal_Items.Add(x));
        }

        public IEnumerable<Order> GetOrders()
        {
            return Orders.ToList();
        }
        public IEnumerable<Item> GetItems()
        {
            return Items.ToList();
        }
        public IEnumerable<Deal_Item> GetDeal_Items()
        {
            return Deal_Items.ToList();
        }

    }
}
