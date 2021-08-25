using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.ProcessedOrderScreen
{
    public class ViewProcessedOrdersUseCase : IViewProcessedOrdersUseCase
    {
        private readonly IOrderRepository orderRepository;

        public ViewProcessedOrdersUseCase(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public IEnumerable<Order> Execute()
        {
            return orderRepository.GetProcessedOrders();
        }
    }
}
