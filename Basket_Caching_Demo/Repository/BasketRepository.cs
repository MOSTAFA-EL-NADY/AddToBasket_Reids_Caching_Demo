using Basket_Caching_Demo.Interface;
using Basket_Caching_Demo.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Basket_Caching_Demo.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redisDb;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redisDb.KeyDeleteAsync(basketId);
        }

        public async Task<UserBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _redisDb.StringGetAsync(basketId);
            return basket.IsNull ? null : JsonSerializer.Deserialize<UserBasket>(basket);
        }

        public async Task<UserBasket?> UpdateOrCreateBasketAsync(UserBasket basket)
        {
            // if the basketId not exists create new basket else update existing basket
            // the last parameter  expiration time for  the basket 
            var updateOrCreateBasket = await _redisDb.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (updateOrCreateBasket)
                return await GetBasketAsync(basket.Id);
            return null;
        }
    }
}
