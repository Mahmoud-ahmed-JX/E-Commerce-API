using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager _serviceManager):ApiController
    {
        //get
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string Id)
        =>Ok(await _serviceManager.BasketService.GetBasketAsync(Id));

        //post
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var basket=await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto);
            return Ok(basket);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBasketAsync(string Id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(Id);
            return NoContent();
        }
    }
}
