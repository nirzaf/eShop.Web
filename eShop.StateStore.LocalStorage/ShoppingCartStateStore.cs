using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Threading.Tasks;

namespace eShop.StateStore.LocalStorage
{
    public class ShoppingCartStateStore : StateStoreBase, IShoppingCartStateStore
    {
        private readonly IShoppingCart shoppingCart;

        public ShoppingCartStateStore(IShoppingCart shoppingCart)
        {            
            this.shoppingCart = shoppingCart;            
        }

        public void LineItemsCountUpdated()
        {
            base.BroadCastStateChange();
        }

        public async Task<int> GetLineItemsCount()
        {
            var order = await shoppingCart.GetOrderAsync();
            if (order != null && order.LineItems != null && order.LineItems.Count > 0)            
                return order.LineItems.Count;                            

            return 0;
        }

        void IShoppingCartStateStore.ProductQuantityUpdated()
        {
            base.BroadCastStateChange();
        }
    }
}
