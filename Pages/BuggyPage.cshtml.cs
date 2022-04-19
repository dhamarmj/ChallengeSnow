using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChallengeSnow.Pages
{
    public class BuggyPageModel : BaseRazorPage
    {

        [BindProperty] // to bind on post to this bookd
        public Item Item { get; set; }

        public void OnGet()
        {

        }
    }
}
