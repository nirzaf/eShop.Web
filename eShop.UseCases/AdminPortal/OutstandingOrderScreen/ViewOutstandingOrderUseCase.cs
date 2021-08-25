using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.OutstandingOrderScreen
{
    public class ViewOutstandingOrderUseCase : IViewOutstandingOrderUseCase
    {
        private readonly IOrderRepository orderRepository;

        public ViewOutstandingOrderUseCase(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public IEnumerable<Order> Execute()
        {
            return orderRepository.GetOutstandingOrders();
        }
    }
}
