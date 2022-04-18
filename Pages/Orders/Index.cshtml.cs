using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Interfaces;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChallengeSnow.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private IOrderManager orderManager;
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Deal_Item> Deal_Items { get; set; }

        public IndexModel(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        public void OnGet()
        {
            Console.WriteLine(orderManager.PrintOrders());
            Orders = orderManager.GetOrders();
            Items = orderManager.GetItems();
            Deal_Items = orderManager.GetDeal_Items();
        }
    }
}
