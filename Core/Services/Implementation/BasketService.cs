using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<BasketDto,CustomerBasket>(basketDto);
            var createdOrUpdatedBasket=await _basketRepository.CreateOrUpdateBaskect(basket);
            return createdOrUpdatedBasket is null ? throw new Exception("Can not create or update"):
                _mapper.Map<BasketDto>(createdOrUpdatedBasket);
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            var result= _basketRepository.DeleteBasketAsync(id);
            return result;
        }

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket =await _basketRepository.GetBasketAsync(id);
            if(basket is null)
            {
                throw new BasketNotFoundException(id);
            }
            return  _mapper.Map<BasketDto>(basket);
        }
    }
}
