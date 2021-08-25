using eShop.CoreBusiness.Models;
using System.Collections.Generic;

namespace eShop.UseCases.ProcessedOrderScreen
{
    public interface IViewProcessedOrdersUseCase
    {
        IEnumerable<Order> Execute();
    }
}