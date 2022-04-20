using ChallengeSnow.Interfaces;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Controllers
{
    public class ItemsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await OrderManager.GetItems();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await OrderManager.GetItem(id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await OrderManager.RemoveItem(id));
        }


        [HttpPost]
        public async Task<ActionResult> Create(Item item)
        {
            return HandleResult(await OrderManager.AddItem(item));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Item item)
        {
            return HandleResult(await OrderManager.UpdateItem(item));
        }
    }
}
