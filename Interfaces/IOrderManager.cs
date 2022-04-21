using ChallengeSnow.Models;
using ChallengeSnow.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Interfaces
{
    // Interface for Order Manager 
    public interface IOrderManager
    {
        public Task<Result<IEnumerable<ItemBase>>> GetAllItems();

        //ITEM METHODS
        public Task<Result<bool>> AddItem(Item item);
        public Task<Result<bool>> RemoveItem(Guid item);
        public Task<Result<Item>> GetItem(Guid id);
        public Task<Result<IEnumerable<Item>>> GetItems();
        public Task<Result<bool>> UpdateItem(Item item);

        //DEAL ITEMS
        public Task<Result<bool>> AddDeal(Deal_Item item);
        public Task<Result<bool>> RemoveDeal(Guid id);
        public Task<Result<Deal_Item>> GetDeal_Items(Guid id);
        public Task<Result<IEnumerable<Deal_Item>>> GetDeal_Items();
        public Task<Result<bool>> UpdateDeal(Deal_Item item);

        //
        //ORDERS
        public Task<Result<bool>> AddOrder(Order order);
        public Task<Result<bool>> RemoveOrder(Guid id);
        public Task<Result<bool>> UpdateOrder(Order order);
        public Task<Result<Order>> GetOrders(Guid id);
        public Task<Result<IEnumerable<Order>>> GetOrders();
        public Task<string> PrintOrders();
        public Task<string> PrintItems();
    }
}
