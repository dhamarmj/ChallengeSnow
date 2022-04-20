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
    public class IndexModel : BaseRazorPage
    {
        public IEnumerable<Order> Orders { get; set; }


        public async void OnGet()
        {
            var orders = await OrderManager.GetOrders();
            Orders = orders.Value;
        }
    }
}
