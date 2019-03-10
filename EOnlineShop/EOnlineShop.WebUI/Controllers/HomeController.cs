using EOnlineShop.core.Models;
using EOnlineShop.core.ViewModel;
using EOnlineShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOnlineShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IRepository<Brand> brands;
        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext, IRepository<Brand> brands)
        {
            context = productContext;
            productCategories = productCategoryContext;
            this.brands = brands;
        }

        public ActionResult Index(string Category = null)
        {
            return View();
        }

        public ActionResult FindProductByCategory(string Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            List<Brand> brands = this.brands.Collection().ToList();
            ProductCategory productCategory = new ProductCategory();
           

            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                brands = this.brands.Collection().Where(b => b.CategoryName.Equals(Category)).ToList();
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;
            model.Brands = brands;
           
            return View(model);
        }
        
        public ActionResult FindProductByName(string Name)
        {
            ProductListViewModel model = new ProductListViewModel();
            var products = context.Collection().Where(p => p.Name == Name).ToList();
            var categories = productCategories.Collection().ToList();
            var brands = this.brands.Collection().ToList();
            model.Products = products;
            model.ProductCategories = categories;
            model.Brands = brands;
            return View(model);
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