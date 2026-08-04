using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using StackExchange.Redis;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection )
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var data = JsonSerializer.Serialize(Basket);
            var result = await _database.StringSetAsync(Basket.Id , data , timeToLive?? TimeSpan.FromDays(7));

            return result ? Basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string BasketId, CancellationToken ct = default)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId, CancellationToken ct = default)
        {
            var basket = await _database.StringGetAsync(BasketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
