using EOnlineShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EOnlineShop.core.Contracts;
using EOnlineShop.core.Models;

namespace EOnlineShop.WebUI.Controllers
{
    public class OrderManagerController : Controller
    {
        private IOrderInterface orderService;
        public OrderManagerController(IOrderInterface orderService)
        {
            this.orderService = orderService;
        }
        
        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = orderService.getOrderList();
            return View(orders);
        }

        public ActionResult UpdateOrder(string id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };

            var order = orderService.getOrder(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order updatedOrder, string id)
        {
            Order order = orderService.getOrder(id);
            order.OrderStatus = updatedOrder.OrderStatus;
            orderService.UpdateOrder(order);
            return RedirectToAction("Index");

        }

    }
}