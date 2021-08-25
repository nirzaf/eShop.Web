using eShop.CoreBusiness.Models;
using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class PlaceOrderUseCase : IPlaceOrderUseCase
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderService orderService;
        private readonly IShoppingCartStateStore shoppingCartStateStore;

        public PlaceOrderUseCase(IShoppingCart shoppingCart, 
            IOrderRepository orderRepository,
            IOrderService orderService,
            IShoppingCartStateStore shoppingCartStateStore)
        {
            this.shoppingCart = shoppingCart;
            this.orderRepository = orderRepository;
            this.orderService = orderService;
            this.shoppingCartStateStore = shoppingCartStateStore;
        }

        public async Task<string> Execute(Order order)
        {            
            await shoppingCart.UpdateOrderAsync(order);
            if (orderService.ValidateCreateOrder(order))
            {
                order.DatePlaced = DateTime.Now;
                order.UniqueId = Guid.NewGuid().ToString();
                int orderId = orderRepository.CreateOrder(order);
                order = orderRepository.GetOrder(orderId);

                await shoppingCart.EmptyAsync();
                this.shoppingCartStateStore.LineItemsCountUpdated();

                return order.UniqueId;
            }            

            return null;
        }
    }
}
