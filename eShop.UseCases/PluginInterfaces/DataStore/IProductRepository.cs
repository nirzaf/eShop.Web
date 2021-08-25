using eShop.CoreBusiness.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.PluginInterfaces.DataStore
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts(string filter = null);        
    }
}
