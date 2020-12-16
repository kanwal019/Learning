using BikeStoresConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeStoresConnector.Classes
{
    public class Orders
    {
        private readonly BikeStoresContext context;

        public Orders(BikeStoresContext context)
        {
            this.context = context;
        }

        public List<Order> GetOrders(Dictionary<string,string> filters)
        {
            try
            {
                _ = filters;
                var orders = from order in context.Orders join
                             customer in context.Customers on order.CustomerId equals customer.CustomerId join
                             staff in context.Staffs on order.StaffId equals staff.StaffId join
                             store in context.Stores on order.StoreId equals store.StoreId
                             orderby order.OrderDate descending
                             select new Order() { 
                                OrderId = order.OrderId,
                                OrderDate = order.OrderDate,
                                OrderStatus = order.OrderStatus,
                                RequiredDate = order.RequiredDate,
                                ShippedDate = order.ShippedDate,
                                Customer = customer,
                                Staff = staff,
                                Store = store,
                                OrderItems = context.OrderItems
                                .Where(orderItems => orderItems.OrderId == order.OrderId).ToList()
                             };

                return orders.ToList();
            }
            catch(Exception ex)
            {
                _ = ex;
                return new List<Order>();
            }
        }

        public Order GetOrder(int orderId)
        {
            try
            {
                return (from order in context.Orders
                       where order.OrderId == orderId
                       select new Order()
                       {
                           OrderId = order.OrderId,
                           OrderDate = order.OrderDate,
                           OrderStatus = order.OrderStatus,
                           StoreId = order.StoreId,
                           OrderItems = context.OrderItems
                            .Where(orderItems => orderItems.OrderId == order.OrderId).ToList()
                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _ = ex;
                return new Order();
            }
        }

        public Order AddItems(Product product, int orderId, int quantity, decimal discount)
        {
            try
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = orderId,
                    ItemId = (from o in context.OrderItems where o.OrderId == orderId select o).Count() + 1,
                    ProductId = product.ProductId,
                    Quantity = quantity,
                    ListPrice = product.ListPrice,
                    Discount = discount
                };

                context.OrderItems.Add(orderItem);
                context.SaveChanges();
           
                return GetOrder(orderId);
            }
            catch (Exception ex)
            {
                _ = ex;
                return new Order();
            }
        }
    }
}
