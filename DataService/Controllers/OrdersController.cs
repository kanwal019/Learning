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
    }
}
