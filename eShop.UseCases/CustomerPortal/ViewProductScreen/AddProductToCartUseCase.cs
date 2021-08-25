using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.ViewProductScreen
{
    public class AddProductToCartUseCase : IAddProductToCartUseCase
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IProductRepository productRepository;
        private readonly IShoppingCartStateStore shoppingCartStateStore;

        public AddProductToCartUseCase(
            IShoppingCart shoppingCart, 
            IProductRepository productRepository,
            IShoppingCartStateStore shoppingCartStateStore)
        {
            this.shoppingCart = shoppingCart;
            this.productRepository = productRepository;
            this.shoppingCartStateStore = shoppingCartStateStore;
        }

        public async void Execute(int productId)
        {
            var product = productRepository.GetProduct(productId);
            await shoppingCart.AddProductAsync(product);

            shoppingCartStateStore.LineItemsCountUpdated();
        }
    }
}
