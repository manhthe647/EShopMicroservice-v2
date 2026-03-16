using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.API.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _repository;

    public BasketsController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet(template: "{username}")]
    public async Task<IActionResult> GetBasketByUsername([Required] string username)
    {
        var result = await _repository.GetBasketByUserName(username);
        return Ok(result == null ? new Cart() : result);
    }

    [HttpPost(Name = "UpdateBasket")]
    [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Cart>> UpdateBasket([FromBody] Cart cart)
    {
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        var result = await _repository.UpdateBasket(cart, options);
        return Ok(result);
    }

    [HttpDelete(template: "{username}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteBasket([Required] string username)
    {
        var result = await _repository.DeleteBasketFromUserName(username);
        return Ok(result);
    }
}