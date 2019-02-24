using EOnlineShop.core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EOnlineShop.core.Models;
using EOnlineShop.Services;

namespace EOnlineShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IOrderInterface orderService;

        public BasketController(IBasketService BasketService, IOrderInterface orderService)
        {
            this.basketService = BasketService;
            this.orderService = orderService;
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

        public ActionResult Checkout1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Checkout1(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.OrderStatus = "Order Processed";
            orderService.CreateOrder(order,basketItems);
            //basketService.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou", new{ OrderId = order.Id } );
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            return View(basketItems);
        }
    }
}