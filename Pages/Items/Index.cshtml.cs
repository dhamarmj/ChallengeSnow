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
        

        public async void OnGet()
        {
            var items = await OrderManager.GetItems();
            
            Items = items.Value;
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            var result = await OrderManager.RemoveItem(id);
            return HandleResult(result);    
        }
    }
}
