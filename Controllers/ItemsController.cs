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
        public IActionResult GetAll()
        {
            var result = OrderManager.GetItems();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return HandleResult(OrderManager.GetItem(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return HandleResult(OrderManager.RemoveItem(id));
        }


        [HttpPost]
        public IActionResult Create(Item item)
        {
            return HandleResult(OrderManager.AddItem(item));
        }

        [HttpPut]
        public IActionResult Update(Item item)
        {
            return HandleResult(OrderManager.UpdateItem(item));
        }
    }
}
