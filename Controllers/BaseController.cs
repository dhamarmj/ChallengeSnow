using ChallengeSnow.Interfaces;
using ChallengeSnow.Models.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeSnow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Base Controller -> for all the Controllers to inherit
    // base references and general returns
    public class BaseController : Controller
    {
        private IOrderManager _orderManager;

        protected IOrderManager OrderManager => _orderManager ??= HttpContext.RequestServices.GetService<IOrderManager>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }

    }
}