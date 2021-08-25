using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public interface IViewShoppingCartUseCase
    {
        Task<Order> Execute();
    }
}