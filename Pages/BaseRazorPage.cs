using ChallengeSnow.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeSnow.Pages
{
    public class BaseRazorPage : PageModel
    {
        private IOrderManager _orderManager;

        protected IOrderManager OrderManager => _orderManager ??= HttpContext.RequestServices.GetService<IOrderManager>();

    }
}
