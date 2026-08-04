using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;

namespace E_Commerce.Application.Services
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAync(BasketDto basket, TimeSpan? TLV = null, CancellationToken ct = default)
        {
            var customarBasket = _mapper.Map<CustomerBasket>(basket);
            var basketResult = await _basketRepository.CreateOrUpdateBasketAsync(customarBasket, TLV ,ct);

            return basketResult == null ? Result<BasketDto>.Fail(Error.Failuer("BasketCreate.Failuer" , "Can not create or update basket"))
                : Result<BasketDto>.Ok(basket);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            var result = await _basketRepository.DeleteBasketAsync(basketId, ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failuer("BasketDelete.Failuer" , "Can not delete basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string BasketId, CancellationToken ct = default)
        {
           var basket= await _basketRepository.GetBasketAsync(BasketId, ct);

            return basket == null ? Result<BasketDto>.Fail(Error.NotFound("Basket not found")) :
                Result<BasketDto>.Ok(_mapper.Map<BasketDto>(basket));
        }
    }
}
