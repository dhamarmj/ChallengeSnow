using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeSnow.Pages.Orders
{
    public class CreateModel : BaseRazorPage
    {
        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public List<SelectListItem> Options { get; set; }


        public void OnGet()
        {
            StartOptions();
        }

        public async void StartOptions()
        {
            var items = await OrderManager.GetAllItems();
            Options = items.Value.Select(a =>
                                new SelectListItem
                                {
                                    Value = a.Id.ToString(),
                                    Text = a.Description
                                }).ToList();

            Options.Insert(0, new SelectListItem
            {
                Value = Guid.Empty.ToString(),
                Text = "Select"
            });
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid && Order.Item_NumberId != Guid.Empty)
            {
                var result = await OrderManager.AddOrder(Order);

                if (result == null) return NotFound();
                if (result.IsSuccess && result.Value != null)
                    return RedirectToPage("Index");
                if (result.IsSuccess && !result.Value == null)
                    return NotFound();

                ViewData["Message"] = result.Error;
            }

            StartOptions();
            return Page();
        }
    }
}
