using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EOnlineShop.core.Models;
using EOnlineShop.core.ViewModel;
using EOnlineShop.Core.Contracts;

namespace EOnlineShop.WebUI.Controllers
{
    public class BrandController : Controller
    {
        IRepository<Brand> context;
        IRepository<ProductCategory> categories;


        public BrandController(IRepository<Brand> categoryContext, IRepository<ProductCategory> categories)
        {
            context = categoryContext;
            this.categories = categories;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Brand> brands = context.Collection().ToList();
            return View(brands);
        }

        public ActionResult Create()
        {
            Brand brand = new Brand();                     
            ViewBag.CategoryList = new List<string>()
            {
                "Laptops",
                "SmartPhones",
                "Cameras",
                "Toys",
                "Audio",
                "Books"
            };
            
            return View(brand);
        }

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
            else
            {
                context.Insert(brand);
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {
            Brand brand = context.Find(Id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(brand);
            }
        }

        [HttpPost]
        public ActionResult Edit(Brand brand, string Id)
        {
            Brand brandToEdit = context.Find(Id);

            if (brandToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(brand);
                }

                //brandToEdit.Category = brand.Category;
                brandToEdit.Name = brand.Name;
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Brand brandToDelete = context.Find(Id);

            if (brandToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(brandToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Brand brandToDelete = context.Find(Id);

            if (brandToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}