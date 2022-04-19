using System;
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
        public IActionResult GetAll()
        {
            var result = OrderManager.GetDeal_Items();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return HandleResult(OrderManager.GetDeal_Items(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return HandleResult(OrderManager.RemoveDeal(id));
        }


        [HttpPost]
        public IActionResult Create(Deal_Item item)
        {
            return HandleResult(OrderManager.AddDeal(item));
        }

        [HttpPut]
        public IActionResult Update(Deal_Item item)
        {
            return HandleResult(OrderManager.UpdateDeal(item));
        }
    }
}