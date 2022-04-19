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


        public void OnGet()
        {
            Console.WriteLine(OrderManager.PrintOrders());
            Orders = OrderManager.GetOrders().Value;
        }
    }
}
