using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChallengeSnow.Pages.Items
{
    public class IndexModel : BaseRazorPage
    {
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Deal_Item> Deal_Items { get; set; }

        public void OnGet()
        {
            Items = OrderManager.GetItems().Value;
            Deal_Items = OrderManager.GetDeal_Items().Value;
        }
    }
}
