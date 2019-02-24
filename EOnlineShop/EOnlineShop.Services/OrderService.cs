using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOnlineShop.core.Contracts;
using EOnlineShop.core.Models;
using EOnlineShop.core.ViewModel;
using EOnlineShop.Core.Contracts;

namespace EOnlineShop.Services
{
    public class OrderService : IOrderInterface
    {
       
        IRepository<Order> orderContext;

        public OrderService(IRepository<Order> orderContext)
        {
            this.orderContext = orderContext;
        }
        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductName = item.ProductName,
                    ProductId = item.Id,
                    Price = item.Price,
                    Quantity = item.Quanity,
                    Image = item.Image

                });
                orderContext.Insert(baseOrder);
                orderContext.Commit();
            }
        }
    }
}
