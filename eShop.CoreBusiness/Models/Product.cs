using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.CoreBusiness.Models
{
    public class Product
    {   
        public int ProductId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
    }
}
