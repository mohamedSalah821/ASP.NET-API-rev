using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Baskets;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string BasketId, CancellationToken ct =default);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan? timeToLive = default, CancellationToken ct = default);
        Task<bool> DeleteBasketAsync(string BasketId, CancellationToken ct = default);
    }
}
