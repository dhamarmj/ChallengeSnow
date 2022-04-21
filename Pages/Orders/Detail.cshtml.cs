using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChallengeSnow.Pages.Orders
{
    public class DetailModel : BaseRazorPage
    {
        [BindProperty]
        public Order Order { get; set; }
        public decimal Profit { get; set; }
        public decimal Reduced_price { get; set; } = 0;


        public async Task<IActionResult> OnGet(Guid id)
        {
            var result = await OrderManager.GetOrders(id);

            if (!result.IsSuccess) return NotFound();

            Order = result.Value;

            var dealitem = Order.Item_Number as Deal_Item;
            if (dealitem != null)
            {
                Profit = Calculating.GetProfit(Order, dealitem); // Is it a deal item?
                Reduced_price = dealitem.Reduced_price;
            }
            else Profit = Calculating.GetProfit(Order, (Item)Order.Item_Number);

            return Page();
        }
    }
}
