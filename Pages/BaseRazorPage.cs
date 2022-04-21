using ChallengeSnow.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using ChallengeSnow.Models.Core;


namespace ChallengeSnow.Pages
{
    // Base Razor Page -> for all the Pages to inherit
    // base references and general returns
    public class BaseRazorPage : PageModel
    {
        private IOrderManager _orderManager;
        protected IOrderManager OrderManager => _orderManager ??= HttpContext.RequestServices.GetService<IOrderManager>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
                return RedirectToPage("Index");
            if (result.IsSuccess && result.Value == null)
                return NotFound();


            ViewData["Message"] = result.Error;
            return Page();
        }


    }
}
