using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOnlineShop.core.Models;
using EOnlineShop.core.ViewModel;

namespace EOnlineShop.core.Contracts
{
    public interface IOrderInterface
    {
        void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems);
        List<Order> getOrderList();
        Order getOrder(string Id);
        void UpdateOrder(Order updatedOrder);
    }
}
