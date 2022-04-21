using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Pages
{
    public class IndexModel : BaseRazorPage
    {
        public string PrintOrders { get; set; }
        public string PrintItems { get; set; }
        public decimal ProfitLoss { get; set; }
        public decimal Profit { get; set; }


        public async void OnGet()
        {
            var orders = await OrderManager.PrintOrders();
            PrintOrders = orders;
            var items = await OrderManager.PrintItems();
            PrintItems = items;

            var orderList = await OrderManager.GetOrders();
            ProfitLoss = Calculating.GetProfitLoss(orderList.Value.ToList());
            Profit = Calculating.GetProfit(orderList.Value.ToList());

        }
    }
}
