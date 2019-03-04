using EOnlineShop.core.Contracts;
using System;
using System.Collections.Generic;
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
        public BasketController(IBasketService BasketService, IOrderInterface orderService, IRepository<Customer> customers)
        {
            this.basketService = BasketService;
            this.orderService = orderService;
            this.customers = customers;
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

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }
        [Authorize]
        public ActionResult Checkout1()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
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
        [Authorize]
        [HttpPost]
        public ActionResult Checkout1(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
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
            return View(basketItems);
        }
    }
}