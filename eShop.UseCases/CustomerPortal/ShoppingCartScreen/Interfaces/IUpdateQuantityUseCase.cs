using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public interface IUpdateQuantityUseCase
    {
        Task<Order> Execute(int productId, int quanity);
    }
}