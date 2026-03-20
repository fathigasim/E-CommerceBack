using EcommerceDomain.Entities;
using MediaRTutorialDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Interfaces
{
    public interface IBasketRepository :IRepository<Basket>
    {
        Task<Basket> GetByIdAsync(string basketId, bool includeItems = false);
        Task<Basket> CreateAsync(Basket basket);
        Task UpdateAsync(Basket basket);
        Task DeleteAsync(Basket basket);
        Task<BasketItem?> GetBasketItemAsync(string basketId, Guid productId);
        Task AddItemAsync(BasketItem item);
        Task RemoveItemAsync(BasketItem item);
        Task<List<BasketItem>> GetBasketItemsAsync(string basketId,CancellationToken cancellationToken);
    }
}
