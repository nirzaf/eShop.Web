using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.UI;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace eShop.ShoppingCart.LocalStorage
{
    public class ShoppingCart : IShoppingCart
    {
        private const string cstrShoppingCart = "eShop.ShoppingCart";
        private readonly IJSRuntime jSRuntime;
        private readonly IProductRepository productRepository;        

        public ShoppingCart(IJSRuntime jSRuntime, IProductRepository productRepository)
        {
            this.jSRuntime = jSRuntime;
            this.productRepository = productRepository;            
        }

        public async Task<Order> GetOrderAsync()
        {
            var order = await GetOrder();
            return order;
        }

        public async Task<Order> AddProductAsync(Product product)
        {
            var order = await GetOrder();
            order.AddProduct(product.ProductId, 1, product.Price);
            await SetOrder(order);

            return order;
        }

        public async Task<Order> DeleteProductAsync(int productId)
        {
            var order = await GetOrder();
            order.RemoveProduct(productId);
            await SetOrder(order);

            return order;
        }

        public Task EmptyAsync()
        {
            return this.SetOrder(null);
        }

        public Task<Order> PlaceOrderAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> UpdateQuantityAsync(int productId, int quanity)
        {
            var order = await GetOrder();
            if (quanity < 0)
                return order;
            else if (quanity == 0)
                return await DeleteProductAsync(productId);

            var lineItem = order.LineItems.SingleOrDefault(x => x.ProductId == productId);
            if (lineItem != null) lineItem.Quantity = quanity;
            await SetOrder(order);

            return order;
        }

        public Task UpdateOrderAsync(Order order)
        {
            return this.SetOrder(order);
        }

        private async Task<Order> GetOrder()
        {
            Order order = null;

            var strOrder = await jSRuntime.InvokeAsync<string>("localStorage.getItem", cstrShoppingCart);
            if (!string.IsNullOrWhiteSpace(strOrder) && strOrder.ToLower() != "null")
                order = JsonConvert.DeserializeObject<Order>(strOrder);
            else
            {
                order = new Order();
                await SetOrder(order);
            }

            foreach (var item in order.LineItems)
            {
                item.Product = productRepository.GetProduct(item.ProductId);
            }

            return order;
        }        

        private async Task SetOrder(Order order)
        {
            await jSRuntime.InvokeVoidAsync("localStorage.setItem", cstrShoppingCart, JsonConvert.SerializeObject(order));            
        }
        
    }
}
