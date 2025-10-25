using Domain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //get basket by id
        Task<CustomerBasket?> GetBasketAsync(string id);

        //Create or update basket
        Task<CustomerBasket?> CreateOrUpdateBaskect(CustomerBasket basket,TimeSpan? TimeToLive=null);
    
        //Delete Basket
        Task<bool> DeleteBasketAsync(string id);
    }
}
