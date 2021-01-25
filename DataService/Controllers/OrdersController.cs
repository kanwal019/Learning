using BikeStoresConnector.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{orderSequence}")]
        public ActionResult<List<Order>> FetchAllOrders(int orderSequence)
        {
            try
            {
                var filters = new Dictionary<string, string>();
                var context = new BikeStoresContext();
                var orders = new BikeStoresConnector.Classes.Orders(context);
                return orders.GetOrders(filters).Skip((orderSequence - 1) * 20).Take(20).ToList();
            }
            catch(Exception ex)
            {
                _ = ex;
                return new List<Order>();
            }
        }

        [Authorize]
        [HttpGet("getOrder/{orderId}")]
        public ActionResult<Order> GetOrder(int orderId)
        {
            try
            {
                var filters = new Dictionary<string, string>();
                var context = new BikeStoresContext();
                var orders = new BikeStoresConnector.Classes.Orders(context);
                return orders.GetOrder(orderId);
            }
            catch (Exception ex)
            {
                _ = ex;
                return new Order();
            }
        }

        [Authorize]
        [HttpPatch("addItem")]
        public ActionResult<Order> AddItem(Product product, int orderId, int quantity, decimal discount)
        {
            try
            {
                var filters = new Dictionary<string, string>();
                var context = new BikeStoresContext();
                var orders = new BikeStoresConnector.Classes.Orders(context);
                return orders.AddItems(product, orderId, quantity, discount);
            }
            catch (Exception ex)
            {
                _ = ex;
                return new Order();
            }
        }
    }
}
