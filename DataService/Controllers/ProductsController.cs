using BikeStoresConnector.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController
    {
        [HttpGet("{sequence}")]
        public ActionResult<List<Product>> FetchAllProducts(int sequence)
        {
            try
            {
                var filters = new Dictionary<string, string>();
                var context = new BikeStoresContext();
                var products = new BikeStoresConnector.Classes.Products(context);
                return products.GetProducts(filters).Skip((sequence - 1) * 20).Take(20).ToList();
            }
            catch (Exception ex)
            {
                _ = ex;
                return new List<Product>();
            }
        }
    }
}
