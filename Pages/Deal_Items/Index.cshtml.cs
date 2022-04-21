using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChallengeSnow.Pages.Deal_Items
{
    public class IndexModel : BaseRazorPage
    {
        public IEnumerable<Deal_Item> Deal_Items { get; set; }

        public async void OnGet()
        {   
            var deals = await OrderManager.GetDeal_Items();
            Deal_Items = deals.Value;
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            var result = await OrderManager.RemoveDeal(id);
            return HandleResult(result);
        }
    }
}
