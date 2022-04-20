using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChallengeSnow.Pages.Items
{
    public class CreateModel : BaseRazorPage
    {
        [BindProperty] // to bind on post to this bookd
        public Item Item { get; set; }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
               var result = await OrderManager.AddItem(Item);
                return HandleResult(result);
            }
            else
            {
                return Page();
            }
        }

    }
}
