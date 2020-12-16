using BikeStoresConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeStoresConnector.Classes
{
    public class Products
    {
        private readonly BikeStoresContext context;

        public Products(BikeStoresContext context)
        {
            this.context = context;
        }

        public List<Product> GetProducts(Dictionary<string, string> filters)
        {
            try
            {
                _ = filters;
                var products = from product in context.Products
                               join brand in context.Brands on product.BrandId equals brand.BrandId
                               join category in context.Categories on product.CategoryId equals category.CategoryId
                               orderby product.ProductName
                               select new Product()
                               {
                                     ProductId = product.ProductId,
                                     ProductName = product.ProductName,
                                     ListPrice = product.ListPrice,
                                     Brand = brand,
                                     Category = category,
                                     ModelYear = product.ModelYear
                               };

                return products.ToList();
            }
            catch (Exception ex)
            {
                _ = ex;
                return new List<Product>();
            }
        }
    }
}
