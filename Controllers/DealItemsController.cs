using System;
using System.Threading.Tasks;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeSnow.Controllers
{
    public class DealItemsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await OrderManager.GetDeal_Items();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await OrderManager.GetDeal_Items(id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await OrderManager.RemoveDeal(id));
        }


        [HttpPost]
        public async Task<ActionResult> Create(Deal_Item item)
        {
            return HandleResult(await OrderManager.AddDeal(item));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Deal_Item item)
        {
            return HandleResult(await OrderManager.UpdateDeal(item));
        }
    }
}