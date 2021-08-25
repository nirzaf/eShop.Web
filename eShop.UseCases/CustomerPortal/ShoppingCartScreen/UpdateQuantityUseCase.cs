using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class UpdateQuantityUseCase : IUpdateQuantityUseCase
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IShoppingCartStateStore shoppingCartStateStore;

        public UpdateQuantityUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore shoppingCartStateStore )
        {
            this.shoppingCart = shoppingCart;
            this.shoppingCartStateStore = shoppingCartStateStore;
        }

        public async Task<Order> Execute(int productId, int quanity)
        {
            var order = await shoppingCart.UpdateQuantityAsync(productId, quanity);
            shoppingCartStateStore.ProductQuantityUpdated();
            return order;
        }
    }
}
