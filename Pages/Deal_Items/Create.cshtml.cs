using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChallengeSnow.Pages.Deal_Items
{
    public class CreateModel : BaseRazorPage
    {
        [BindProperty]
        public Deal_Item Deal { get; set; }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await OrderManager.AddDeal(Deal);
                return HandleResult(result);
            }
            else
            {
                return Page();
            }
        }

    }
}
