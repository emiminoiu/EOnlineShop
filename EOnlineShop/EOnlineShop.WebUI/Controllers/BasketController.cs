using EOnlineShop.core.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EOnlineShop.core.Models;
using EOnlineShop.Core.Contracts;
using EOnlineShop.Services;

namespace EOnlineShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IOrderInterface orderService;
        private IRepository<Customer> customers;
        private IRepository<Order> orders;
       
        public BasketController(IBasketService BasketService, IOrderInterface orderService, IRepository<Customer> customers, IRepository<Order> orders)
        {
            this.basketService = BasketService;
            this.orderService = orderService;
            this.customers = customers;
            this.orders = orders;
          
        }
        // GET: Basket2
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }
        
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            if (basketItems.Count < 1)
            {
                return View("Index1");
            }
            return RedirectToAction("Index");
        }

        public ActionResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            if (basketSummary.BasketCount == 0)
            {
                return HttpNotFound();
            }
            return PartialView(basketSummary);
        }
        [Authorize]
        public ActionResult Checkout1()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            if (basketItems == null)
            {
                RedirectToAction("Index");
            }
            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    Street = customer.Street


                };
                return View(order);
            }
            else
            {
                return View();
            }
        }

        public ActionResult ViewOrders()
        {
            List<Order> ordersList = orders.Collection().ToList();
            return View(ordersList);
        }
       
        //public ActionResult OrderedItems(string orderId)
        //{
        //    List<OrderItem> orderItems = new List<OrderItem>();
        //    var theOrderItems = this.orderItems.Find(orderId);
        //    orderItems.Add(theOrderItems);
        //    return View(orderItems);

        //}

        [Authorize]
        [HttpPost]
        public ActionResult Checkout1(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            if (basketItems == null)
            {
                RedirectToAction("Index");
            }

            order.OrderStatus = "Order Created";
            order.OrderStatus = "Order Processed";
            order.Email = User.Identity.Name;
            orderService.CreateOrder(order, basketItems);
            //basketService.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            if (basketItems == null)
            {
                RedirectToAction("Index");
            }
            return View(basketItems);
        }
    }
}