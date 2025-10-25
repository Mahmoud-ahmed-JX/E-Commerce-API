using Domain.Contracts;
using Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBaskect(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var jsonBasket=JsonSerializer.Serialize(basket);
            var res=await _database.StringSetAsync(basket.Id,jsonBasket,TimeToLive??TimeSpan.FromDays(30));

            return res ? await GetBasketAsync(basket.Id): null;
        }

        public Task<bool> DeleteBasketAsync(string id)
        =>_database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var res =await _database.StringGetAsync(id);
            if(res.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(res!);
        }
    }
}
