using ChallengeSnow.Interfaces;
using ChallengeSnow.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Controllers
{
    public class OrdersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await OrderManager.GetOrders();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await OrderManager.GetOrders(id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await OrderManager.RemoveOrder(id));
        }


        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            return HandleResult(await OrderManager.AddOrder(order));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            return HandleResult(await OrderManager.UpdateOrder(order));
        }
    }
}
