using ChallengeSnow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Interfaces
{
    public interface IOrderManager
    {
       
        public void AddItem(Item item);
        public void AddOrder(Order order);
        public string PrintOrders();
        public bool ContainsOrders();
        public void InitialValues(List<Order> ordersList, List<Item> itemList, List<Deal_Item> dealItems);
        public IEnumerable<Item> GetItems();
        public IEnumerable<Deal_Item> GetDeal_Items();
        public IEnumerable<Order> GetOrders();
    }
}
