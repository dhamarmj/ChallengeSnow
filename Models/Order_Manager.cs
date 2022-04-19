using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models.Core;

namespace ChallengeSnow.Models
{
    public class Order_Manager : Interfaces.IOrderManager
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Deal_Item> Deal_Items { get; set; } = new List<Deal_Item>();


        //ITEM METHODS
        #region  Items
        public Result<bool> AddItem(Item item)
        {
            if (item.Id == null || item.Id == Guid.Empty) item.Id = new Guid();
            else
            {
                var result = Items.Find(x => x.Id == item.Id);
                if (result != null) return Result<bool>.Failure("Item exists"); // "Item Already exists";
            }

            Items.Add(item);
            return Result<bool>.Success(true);
        }

        public Result<bool> RemoveItem(Guid id)
        {
            var result = Items.Find(x => x.Id == id);

            if (result == null) return Result<bool>.Failure("Item doesn't exist"); // "Item doesnt exist!";

            Orders.RemoveAll(x => x.Item_Number.Id == id);
            Items.Remove(result);
            return Result<bool>.Success(true);
        }

        public Result<Item> GetItem(Guid id)
        {
            var result = Items.Find(x => x.Id == id);

            if (result == null) return Result<Item>.Failure("Item not found");

            return Result<Item>.Success(result);
        }
        public Result<IEnumerable<Item>> GetItems()
        {
            return Result<IEnumerable<Item>>.Success(Items.ToList());
        }

        public Result<bool> UpdateItem(Item item)
        {
            var result = Items.FindIndex(x => x.Id == item.Id);

            if (result == -1) return Result<bool>.Failure("Item not found"); // "Item Already exists";

            Items[result] = item;
            return Result<bool>.Success(true);
        }

        #endregion

        // DEAL METHODS
        #region deal_items
        public Result<bool> AddDeal(Deal_Item item)
        {
            if (item.Start_Date > item.End_Date) return Result<bool>.Failure("Start and End Dates don't match");

            var result = Deal_Items.Find(x => x.Id == item.Id);

            if (result != null) return Result<bool>.Failure("Deal already exists"); // "Item Already exists";

            Deal_Items.Add(item);
            return Result<bool>.Success(true);
        }
        public Result<IEnumerable<Deal_Item>> GetDeal_Items()
        {
            return Result<IEnumerable<Deal_Item>>.Success(Deal_Items.ToList());
        }
        public Result<Deal_Item> GetDeal_Items(Guid id)
        {
            var result = Deal_Items.Find(x => x.Id == id);

            if (result == null) return Result<Deal_Item>.Failure("Deal not found");

            return Result<Deal_Item>.Success(result);
        }
        public Result<bool> UpdateDeal(Deal_Item item)
        {
            if (item.Start_Date > item.End_Date) return Result<bool>.Failure("Start and End Dates don't match");

            var result = Deal_Items.FindIndex(x => x.Id == item.Id);

            if (result == -1) return Result<bool>.Failure("Deal doesn't exist"); // "Item Already exists";

            Deal_Items[result] = item;
            return Result<bool>.Success(true);
        }
        public Result<bool> RemoveDeal(Guid id)
        {
            var result = Deal_Items.Find(x => x.Id == id);

            if (result == null) return Result<bool>.Failure("Deal doesn't exist"); // "Item Already exists";

            Orders.RemoveAll(x => x.Item_Number.Id == id);
            Deal_Items.Remove(result);
            return Result<bool>.Success(true);
        }

        #endregion

        #region orders
        public Result<bool> AddOrder(Order order)
        {
            if (order.Id == null || order.Id == Guid.Empty) order.Id = new Guid();
            else
            {
                var result = Orders.Find(x => x.Id == order.Id);

                if (result != null) return Result<bool>.Failure("Order already exists");
            }

            var dealItem = GetDeal_Items(order.Item_Number.Id).Value;
            var item = GetItem(order.Item_Number.Id).Value;

            if (dealItem == null && item == null) return Result<bool>.Failure("Item doesn't exist");
            else if (dealItem != null)
            {
                order.Item_Number = dealItem;
                dealItem.Available_Quantity -= order.Quantity;

                UpdateDeal(dealItem);
            }
            else
            {
                order.Item_Number = item;
                item.Available_Quantity -= order.Quantity;

                UpdateItem(item);
            }

            order.Date_Created = DateTime.Now;

            Orders.Add(order);

            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateOrder(Order order)
        {
            var result = Orders.FindIndex(x => x.Id == order.Id);

            if (result == -1) return Result<bool>.Failure("Order doesn't exist"); // "Item Already exists";

            Orders[result] = order;
            return Result<bool>.Success(true);
        }

        public Result<bool> RemoveOrder(Guid id)
        {
            var result = Orders.Find(x => x.Id == id);

            if (result == null) return Result<bool>.Failure("Order doesn't exist"); // "Item Already exists";

            Orders.Remove(result);
            return Result<bool>.Success(true);
        }

        public Result<IEnumerable<Order>> GetOrders()
        {
            return Result<IEnumerable<Order>>.Success(Orders.ToList());
        }

        public Result<Order> GetOrders(Guid id)
        {
            var result = Orders.Find(x => x.Id == id);

            if (result == null) return Result<Order>.Failure("Order not found");

            return Result<Order>.Success(result);
        }

        #endregion
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


    }
}
