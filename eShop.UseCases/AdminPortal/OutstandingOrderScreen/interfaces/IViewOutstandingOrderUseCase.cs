using eShop.CoreBusiness.Models;
using System.Collections.Generic;

namespace eShop.UseCases.OutstandingOrderScreen
{
    public interface IViewOutstandingOrderUseCase
    {
        IEnumerable<Order> Execute();
    }
}