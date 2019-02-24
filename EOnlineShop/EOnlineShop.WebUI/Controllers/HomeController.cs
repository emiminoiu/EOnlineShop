using EOnlineShop.core.Models;
using EOnlineShop.core.ViewModel;
using EOnlineShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOnlineShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }

        public ActionResult Index(string Category = null)
        {
            return View();
        }

        public ActionResult FindProductByCategory(string Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();

            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;
            return View(model);
        }
        
        public ActionResult FindProductByName(string Name)
        {
            var products = context.Collection().Where(p => p.Name == Name).ToList();
            
            return View(products);
        }
        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            var viewModel = new BasketItemViewModel()
            {
                Id = product.Id,
                Quanity = 1,
                ProductName = product.Name,
                Image = product.Image,
                Price = product.Price
            };
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(viewModel);
            }
        }

    }
}