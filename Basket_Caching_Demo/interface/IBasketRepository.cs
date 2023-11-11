using Basket_Caching_Demo.Models;

namespace Basket_Caching_Demo.Interface;
public interface IBasketRepository
{
    Task<UserBasket> GetBasketAsync(string basketId);
    Task<UserBasket> UpdateOrCreateBasketAsync(UserBasket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}

