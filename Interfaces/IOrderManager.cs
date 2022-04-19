using ChallengeSnow.Models;
using ChallengeSnow.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Interfaces
{
    public interface IOrderManager
    {

        //ITEM METHODS

        public Result<IEnumerable<Item>> GetItems();
        public Result<bool> RemoveItem(Guid item);
        public Result<bool> AddItem(Item item);
        public Result<Item> GetItem(Guid id);
        public Result<bool> UpdateItem(Item item);


        //  

        //DEAL ITEMS
        public Result<IEnumerable<Deal_Item>> GetDeal_Items();
        public Result<Deal_Item> GetDeal_Items(Guid id);
        public Result<bool> RemoveDeal(Guid id);
        public Result<bool> UpdateDeal(Deal_Item item);
        public Result<bool> AddDeal(Deal_Item item);

        //
        //ORDERS
        public Result<bool> AddOrder(Order order);
        public Result<bool> UpdateOrder(Order order);
        public Result<bool> RemoveOrder(Guid id);
        public Result<IEnumerable<Order>> GetOrders();
        public Result<Order> GetOrders(Guid id);


        public string PrintOrders();
        public bool ContainsOrders();
        public void InitialValues(List<Order> ordersList, List<Item> itemList, List<Deal_Item> dealItems);

    }
}
