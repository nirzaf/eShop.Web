using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ProcessOrderScreen
{
    public interface IViewOrderDetailUseCase
    {
        Order Execute(int orderId);
    }
}