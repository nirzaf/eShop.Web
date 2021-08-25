using eShop.CoreBusiness.Models;

namespace eShop.UseCases.OrderConfirmationScreen
{
    public interface IViewOrderConfirmationUseCase
    {
        Order Execute(string uniqueId);
    }
}