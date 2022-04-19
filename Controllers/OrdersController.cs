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
        public IActionResult GetAll()
        {
            var result = OrderManager.GetOrders();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return HandleResult(OrderManager.GetOrders(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return HandleResult(OrderManager.RemoveOrder(id));
        }


        [HttpPost]
        public IActionResult Create(Order order)
        {
            return HandleResult(OrderManager.AddOrder(order));
        }

        [HttpPut]
        public IActionResult Update(Order order)
        {
            return HandleResult(OrderManager.UpdateOrder(order));
        }
    }
}
